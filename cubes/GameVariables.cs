using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to be used as a script in the gameController

//instead of just storing the game-global variables, it changes the index

/// <summary>
/// create a distribution for velocityIncreament and timeBetweenTwoCubes
/// </summary>
/// 
public class GameVariables : MonoBehaviour {

    public static float cubeVerticalMovement = 1;
    //public float velocityIncreament;
    float frequency = new float();
    public float timeBetweenTwoCubes;
    public Color[] cubeColors = new Color[3];
    public static int currentIndex = 0;
    public Animator circleAnimator;
    public float timeUntilChange = new float();
    public static bool tutorial = new bool();

	// Use this for initialization
	void Start () {
        timeUntilChange = (1 / (TimeController.getGameTime() + 0.03f)) + Random.value * 30;
        cubeVerticalMovement = 1;
        currentIndex = 0;
        Debug.Log("time is: " + TimeController.getGameTime());
    }
	
	// Update is called once per frame
	void Update () {
        //cubeVerticalMovement += velocityIncreament * Time.deltaTime;
        //with the code below, the cube speed will reach 5.97 units after 120 seconds
        cubeVerticalMovement = 2 * (1 - Mathf.Pow(2.718f, TimeController.getGameTime() * (-0.05f))) / (1 + Mathf.Pow(2.718f, TimeController.getGameTime() * (-0.05f))) + 1;
        frequency = 3*(1 - Mathf.Pow(2.718f, TimeController.getGameTime() * (-0.05f))) / (1 + Mathf.Pow(2.718f, TimeController.getGameTime() * (-0.05f))) + 0.5f;
        timeBetweenTwoCubes = 1 / frequency;
        timeUntilChange -= TimeController.timeScale * Time.deltaTime;
    }
}
