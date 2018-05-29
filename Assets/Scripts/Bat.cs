using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

    //敌人的速度，伤害
    [SerializeField]private float speed;
    [SerializeField]private float hit;
    [SerializeField] private int hitScore;

    SpriteRenderer sr;
    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
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

    //判断敌人是否到玩家身后，出了屏幕
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "LeftEdge")
        {
            if(GameManager.Instance.GetScore() >= 0)
            {
                GameManager.Instance.AddScore(-hitScore);
            }
            PlayerController.Institute.ChangeMaxRadius(-hit);
            Destroy(gameObject);
        }
    }

    Vector3 Move(Vector3 targetPosition)
    {
        Vector3 dir = targetPosition - transform.position;
        dir.Normalize();
        dir = AvoidObstacle(dir);
        return dir;
    }

    Vector3 AvoidObstacle(Vector3 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 3f, 1 << LayerMask.NameToLayer("Obstacle"));
        if(hit)
        {
            Vector3 hitNormal = hit.normal;
            hitNormal.z = 0;
            dir += hitNormal;
        }
        return dir;
    }

    // Update is called once per frame
    protected void FixedUpdate () {
        Vector3 targetPosition = PlayerController.Institute.GetComponent<Transform>().position;
        Vector3 dir = new Vector3(-1, 0, 0);
        if (Vector3.Distance(transform.position, targetPosition) > 2f && targetPosition.x < transform.position.x)
        {
            dir = Move(targetPosition);
        }
        transform.position += new Vector3(-1 * Time.deltaTime * speed, dir.y * Time.deltaTime * speed, 0) ;
        Visuable();
	}
}
