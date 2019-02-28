using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorParameters : MonoBehaviour {

    Animator selfAnimator;

	// Use this for initialization
	void Start () {
        selfAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleBool(string n)
    {
        selfAnimator.SetBool(n, !selfAnimator.GetBool(n));
    }
}
