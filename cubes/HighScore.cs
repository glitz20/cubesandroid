using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    Text selfText;

	// Use this for initialization
	void Start () {
        selfText = GetComponent<Text>();
        selfText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CheckHighScore(int score)
    {
        if (PlayerPrefs.GetInt("HighScore", 0) < score)
            PlayerPrefs.SetInt("HighScore", score);
    }
}
