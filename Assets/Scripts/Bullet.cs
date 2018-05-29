using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOS;

public class Bullet : MonoBehaviour {

    public float speed;
    public float maxDistance = 30f;
    public int minAddScore;
    public int maxAddScore;
    int addScore;

    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        speed = 15f;
        StartCoroutine(Destroy());
	}

    //是否可见，要求物体上有meshrender
    //当物体在玩家光圈外不可见
    void Visuable()
    {
        if (Vector3.Distance(transform.position, PlayerController.Institute.transform.position) >= PlayerController.Institute.currentRadius)
        {
            sr.enabled = false;
        }
        else
        {
            sr.enabled = true;
        }
    }

    //3s后删除子弹
    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle" || collision.tag == "Enemy")
        {
            if(collision.tag == "Enemy")
            {
                addScore = Random.Range(minAddScore, maxAddScore);
                GameManager.Instance.AddScore(addScore);
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        transform.position += transform.rotation * new Vector3(speed * Time.deltaTime, 0, 0);
        Visuable();
    }
}
