using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{

    public bool down = false;
    public bool up = false;
    public Transform downPoint;
    public Transform upPoint;

    /*用于计时*/
    private float timer = 0;
    private float curTime;
    private float deltaTime;
    private float lastFrameTime;

    /*用于恢复timescale*/
    private float timer1 = 0;
    private float curTime1;
    private float deltaTime1;
    private float lastFrameTime1;

    void Update()
    {
        curTime = Time.realtimeSinceStartup; // 当前真实时间
        deltaTime = curTime - lastFrameTime; // 此帧与上一帧的时间间隔
        lastFrameTime = curTime; // 记录此帧时间，下一帧用
        timer += deltaTime; // 动画已播放时间

        if(timer >= 0.01f)
        {
            timer -= 0.01f;
            if (down == true && this.transform.position.y > downPoint.position.y)
            {
                transform.Translate(new Vector3(0, -10f, 0));
            }
            if (up == true && this.transform.position.y < upPoint.position.y)
            {
                transform.Translate(new Vector3(0, 10f, 0));
            }
            if (up == true && this.transform.position.y > upPoint.position.y-0.1f)
            {
                Time.timeScale = 1;
            }
        }

        if (up == true)
        {
            curTime1 = Time.realtimeSinceStartup; // 当前真实时间
            deltaTime1 = curTime1 - lastFrameTime1; // 此帧与上一帧的时间间隔
            lastFrameTime1 = curTime1; // 记录此帧时间，下一帧用
            timer1 += deltaTime1; // 动画已播放时间

            if (timer1 > 0.6f)
            {
                Time.timeScale = 1;
                timer1 -= 0.6f;
            }

        }
    }

    public void Up()
    {
        up = true;
        down = false;
    }
    public void Down()
    {
        down = true;
        up = false;
    }

}
