using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indexColor : MonoBehaviour {

    public GameVariables gameVariables;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<SpriteRenderer>().color = gameVariables.cubeColors[GameVariables.currentIndex];
	}
}
