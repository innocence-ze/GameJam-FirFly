using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour {

    public float speed = 0.1f;
    private float timer = 0f;
    public static float time = 0.05f;
    //两变量用于获取生成与消失位置
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
        //水平移动
        timer += Time.deltaTime;
        if (timer > time)
        {
            this.transform.Translate(new Vector3(-speed, 0, 0));
            timer -= time;
        }

        //重新生成
        if (this.transform.position.x <= left.position.x+2f)
        {
            this.transform.position = right.position;
            if(BGMove.time>0.02f)
                BGMove.time-=0.002f;
            ChangePrefab();
            Destroy(this.gameObject);
        }
    }

    //创建新的预制体
    private void ChangePrefab()
    {
        Object bg = Resources.Load("Prefabs/BG" + Random.Range(1, 10), typeof(GameObject));
        GameObject aa = Instantiate(bg) as GameObject;
        aa.transform.position = right.position;
    }
}
