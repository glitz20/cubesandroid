using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSquare : MonoBehaviour {

    public GameVariables gameVariables;
    public GameObject parentCube;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int colorIndex = (int)(Random.value * 3) % 3;
        GetComponent<SpriteRenderer>().color = gameVariables.cubeColors[colorIndex];
        transform.position = new Vector3(10 / parentCube.transform.position.x, transform.position.y);
    }
}
