using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour {

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

    // Update is called once per frame
    void FixedUpdate () {
        Visuable();
	}
}
