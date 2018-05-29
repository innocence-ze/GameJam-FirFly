using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOS;
using UnityEngine.UI;

enum PlayerState
{

    light,
    shot,

}


public class PlayerController : MonoBehaviour {

    public static PlayerController Institute;
    public GameManager gameManager;
    public Animator anim;

    //用于改变光圈动画
    public GameObject lightCircle;
    public GameObject fireShine;

    new public GameObject light;
    public SpriteRenderer spr;//获取主角图片，用于隐藏动画

    //用于改变UI图像
    public GameObject ui_fire;
    public GameObject ui_begin;
    public GameObject left_circle;
    public GameObject right_circle;

    //用于改变主角碰撞体
    [SerializeField]private Collider2D lightCollider;
    [SerializeField]private Collider2D shotCollider;

    //player的移动速度
    public float speed;

    //player光圈的半径
    //光圈的对象，最大、当前、最小光圈
    //光状态与火状态光圈的增加减少量
    public LOSRadialLight radiusLight;
    public float maxRadius;
    public float currentRadius;
    public float minRadius;
    public float addRadius;
    public float decRadius;

    //player发射子弹的速率、位置
    //及发射子弹减小的光圈半径
    public float shotRadius;
    public Transform shotPosition;
    public float shotRate;

    //记录主角状态
    private bool isAlive;
    private PlayerState playerState;

	// Use this for initialization
	void Start () {
        Institute = this;
        currentRadius = maxRadius;
        radiusLight.radius = currentRadius;

        lightCircle.transform.localScale = new Vector3(currentRadius, currentRadius, 1);

        isAlive = true;
        playerState = PlayerState.light;
        anim = gameObject.GetComponentInChildren<Animator>();

        anim.SetBool("light", true);
        anim.SetBool("shot", false);
	}

    //改变最大光圈值
    public void ChangeMaxRadius(float change)
    {
        maxRadius += change;
        if(currentRadius>=maxRadius)
        {
            currentRadius = maxRadius;
            lightCircle.transform.localScale = new Vector3(currentRadius, currentRadius, 1);
        }
        radiusLight.radius = currentRadius;
        if(maxRadius <= minRadius)
        {
            isAlive = false;
        }
    }
	
    //player移动上下左右
    //平移，没有加速度
    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        Vector3 directionVector = new Vector3(moveHorizontal, moveVertical, 0);
        Vector3 movePosition = transform.rotation * directionVector;
        transform.position += movePosition;
    }

    //物体碰撞后死亡
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.tag == "Obstacle")
        {
            isAlive = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle")
        {
            isAlive = false;
        }
    }

    //是否存活，逐帧判断
    public bool IsAlive()
    {
        if (isAlive == true && currentRadius > minRadius && gameManager.GetScore() >= 0)
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
        }
            return isAlive;
    }

    void ChangeState()
    {
        if (playerState == PlayerState.light)
        {
            playerState = PlayerState.shot;
            ui_fire.SetActive(true);
            right_circle.SetActive(true);
            left_circle.SetActive(false);
            ui_begin.SetActive(false);
            anim.SetBool("shot", true);
            anim.SetBool("light", false);  
            Invoke("ShotShine", 0.25f);
            light.SetActive(false);
        }
        else
        {
            playerState = PlayerState.light;
            ui_fire.SetActive(false);
            right_circle.SetActive(false);
            left_circle.SetActive(true);
            ui_begin.SetActive(true);
            anim.SetBool("light", true);
            anim.SetBool("shot", false);
            Invoke("LightShine", 0.25f);
            fireShine.SetActive(false);
        }
    }

    float timer = 0;
    void OnShotState()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.J) && timer >= shotRate && currentRadius - shotRadius > minRadius)
        {
            timer = 0;
            Shot();
        }
        if(currentRadius >= minRadius)
        {
            currentRadius -= decRadius * Time.deltaTime;
            lightCircle.transform.localScale = new Vector3(currentRadius, currentRadius, 1);
        }
    }

    public AudioClip shotAudio;

    //发射子弹
    void Shot()
    {
        AudioSource.PlayClipAtPoint(shotAudio, transform.position);
        GameObject bullet = Resources.Load<GameObject>("Bullet");
        Instantiate(bullet, shotPosition.position, new Quaternion(0, 0, 0, 0));
        currentRadius -= shotRadius;
        lightCircle.transform.localScale = new Vector3(currentRadius, currentRadius, 1);
    }

    void OnLightState()
    {
        if(currentRadius < maxRadius)
        {
            currentRadius += addRadius * Time.deltaTime;
            lightCircle.transform.localScale = new Vector3(currentRadius, currentRadius, 1);
        }
    }

	// Update is called once per frame
	void Update () {

        if (IsAlive())
        {
            Move();
            if(Input.GetKeyDown(KeyCode.K))
            {
                ChangeState();
            }      
            else
            {
                switch (playerState)
                {
                    case PlayerState.light:
                        lightCollider.enabled = true;
                        shotCollider.enabled = false;
                        OnLightState();
                        break;
                    case PlayerState.shot:
                        if(anim.GetCurrentAnimatorStateInfo(0).IsName("shot"))
                        {
                            lightCollider.enabled = false;
                            shotCollider.enabled = true;
                            OnShotState();
                        }
                        break;
                }
            }        
        }
        else
        {
            //游戏结束
            anim.SetBool("die", true);
            light.SetActive(false);
            fireShine.SetActive(false);
            StartCoroutine(die());
            gameManager.Over();
        }
        radiusLight.radius = currentRadius;
    }

    //开启射击状态的光管动画
    void ShotShine()
    {
        fireShine.SetActive(true);
    }
    //开启发光状态的光管动画
    void LightShine()
    {
        light.SetActive(true);
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(1.0f);
        lightCollider.enabled = false;
        shotCollider.enabled = false;
        spr.color = new Color(1, 1, 1, 0);
    }
}
