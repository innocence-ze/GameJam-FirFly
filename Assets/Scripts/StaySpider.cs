using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaySpider : MonoBehaviour {

    //敌人的速度，伤害
    [SerializeField] private float speed;
    [SerializeField] private float hit;
    private float timer = 0f;

    SpriteRenderer sr;
    SpriteRenderer net;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        net = gameObject.GetComponentInChildren<SpriteRenderer>();
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
            PlayerController.Institute.ChangeMaxRadius(-hit);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        timer += Time.deltaTime;

        if(timer>BGMove.time)
        {
            this.transform.Translate(new Vector3(-speed, 0, 0));
            timer -= BGMove.time;
        }
        Visuable();
    }
}
