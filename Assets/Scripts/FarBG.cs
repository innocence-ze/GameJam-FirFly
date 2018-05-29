using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarBG : MonoBehaviour
{
    public float speed = 0.01f;
    private float timer = 0f;
    public Transform left;
    public Transform right;

    //获取刷新和消失位置
    void Awake()
    {
        left = GameObject.Find("LeftPoint").transform;
        right = GameObject.Find("RightPoint").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 0.05f)
        {
            this.transform.Translate(new Vector3(-speed, 0, 0));
            timer -= 0.05f;
        }

        if (this.transform.position.x <= left.position.x + 2f)
        {
            this.transform.position = right.position;
            ChangePicture();
        }
    }

    //换图片移动位置
    private void ChangePicture()
    {
        Sprite spr = Resources.Load<Sprite>("Sprites/far" + Random.Range(1,4));
        this.gameObject.GetComponent<SpriteRenderer>().sprite = spr;
    }
}
