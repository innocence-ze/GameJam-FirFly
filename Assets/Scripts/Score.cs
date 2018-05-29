using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private Text score;

	// Use this for initialization
	void Start () {

        score = this.GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {

        score.text = GameManager.Instance.GetScore().ToString();
       

	}
}
