using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class After999999 : MonoBehaviour {

    Text scoreText;
    int score;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        score = int.Parse(scoreText.text);
	}
	
	// Update is called once per frame
	void Update () {
        score = int.Parse(scoreText.text);
        if (score >= 999999)
            GetComponent<Animator>().SetTrigger("Raise");
	}
}
