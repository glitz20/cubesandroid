using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActive : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("gameStyle", 0) == 1)
        {
            gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("gameStyle", 0) == 0)
        {
            gameObject.SetActive(true);
            /*Camera thisCamera = GetComponent<Camera>();
            if (thisCamera != null)
                thisCamera.depth = 1;*/
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
