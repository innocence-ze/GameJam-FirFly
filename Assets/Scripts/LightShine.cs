using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShine : MonoBehaviour {

    private int flag = 0;//标志，用于恢复原位
    private float timer = 0f;//计时
    public Transform trans;//记录原位

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            timer -= 0.2f;
            flag = flag + 1;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.06f, transform.position.z);
            if (flag >= 4)
            {
                flag = 0;
                this.gameObject.transform.position = trans.position;
            }
        }

    }
}
