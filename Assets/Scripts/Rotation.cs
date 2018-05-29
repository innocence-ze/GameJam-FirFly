using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour {

    private float timer = 0f;
    private int count = 0;
    private Sprite spr;
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if(timer>0.2f)
        {
            timer -= 0.2f;
            count = (count + 1) % 3;
            spr = Resources.Load<Sprite>("Sprites/"+count.ToString());
            this.gameObject.GetComponent<Image>().sprite = spr;
        }

	}
}
