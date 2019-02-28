using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to be used as script in the gameController

public class TimeController : MonoBehaviour {

    public static float timeScale = 1;
    static float gameTime = 0;

    private void Awake()
    {
        timeScale = 1;
        gameTime = 0;
    }

    private void Update()
    {
        gameTime += timeScale * Time.deltaTime;
    }

    public static float getGameTime() { return gameTime; }
}
