using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShine : MonoBehaviour {

    private int flag=0;
    private float timer = 0f;
    public Transform trans;

    // Update is called once per frame
    void Update () {

        timer += Time.deltaTime;

        if (timer > 0.2f)
        {
            timer -= 0.2f;
            flag = flag + 1;
            transform.position = new Vector3(transform.position.x+0.06f,transform.position.y,transform.position.z);
            if (flag >= 4)
            {
                flag = 0;
                this.gameObject.transform.position = trans.position;
            }
        }

	}
}
