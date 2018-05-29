using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour {

    private Slider slider;

    // Use this for initialization
    void Start () {
        slider = GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        //逐帧更新血条长度
        slider.value =( PlayerController.Institute.currentRadius- PlayerController.Institute.minRadius) / (PlayerController.Institute.maxRadius - PlayerController.Institute.minRadius);
	}
}
