using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpider : MonoBehaviour {

    //敌人的速度，伤害
    [SerializeField] private float upSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float hit;
    [SerializeField] private int hitScore;

    SpriteRenderer sr;
    SpriteRenderer net;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        net = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    //是否可见，要求物体上有meshrender
    //当物体在玩家光圈外不可见
    void Visuable()
    {
        if (Vector3.Distance(transform.position, PlayerController.Institute.transform.position) >= PlayerController.Institute.currentRadius)
        {
            sr.enabled = false;
            net.enabled = false;
        }
        else
        {
            sr.enabled = true;
            net.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "LeftEdge")
        {
            if (GameManager.Instance.GetScore() >= 0)
            {
                GameManager.Instance.AddScore(-hitScore);
            }
            PlayerController.Institute.ChangeMaxRadius(-hit);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {


        if(transform.position.y <= -1.5f)
        {
            transform.position += new Vector3(-speed * Time.deltaTime, upSpeed * Time.deltaTime, 0);
        }
        else if(transform.position.y >= 1f)
        {
            transform.position += new Vector3(-speed * Time.deltaTime, -upSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
        Visuable();
    }

}
