using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroyer : MonoBehaviour {

    public HighScore highScore;
    public Text scoreText;
    public Canvas canvas;
    public CameraScript cameraScript3D;
    public CameraScript cameraScript2D;
    public SquareInformation squares;
    public CubeMovement cubeMovement;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidingGameObject = other.gameObject;
        if (collidingGameObject.tag == "cube")
        {
            if (collidingGameObject.GetComponent<SetUpColor>().getIndex() == GameVariables.currentIndex)
            {
                TimeController.timeScale = 0;
                Debug.Log("Dead");
                highScore.CheckHighScore(int.Parse(scoreText.text));
                collidingGameObject.GetComponent<SetUpColor>().Death();
                cameraScript3D.DeathFocus(collidingGameObject.transform.position);
                cameraScript2D.DeathFocus(collidingGameObject.transform.position);
                if (squares.gameObject.activeSelf)
                    squares.Blink(collidingGameObject.GetComponent<SetUpColor>().getListIndex());
                canvas.GetComponent<Animator>().SetTrigger("Dead");
            }
            else
                cubeMovement.DestroyCubeAt(collidingGameObject.GetComponent<SetUpColor>().getListIndex(), 1);
        }
        else if(collidingGameObject.tag=="specialCube")
        {
            TimeController.timeScale = 0;
            Debug.Log("Dead");
            highScore.CheckHighScore(int.Parse(scoreText.text));
            collidingGameObject.GetComponent<SpecialCubeMovement>().Death();
            cameraScript3D.DeathFocus(collidingGameObject.transform.position);
            cameraScript2D.DeathFocus(collidingGameObject.transform.position);
            if (squares.gameObject.activeSelf)
                collidingGameObject.GetComponent<SpecialCubeMovement>().Death();
            canvas.GetComponent<Animator>().SetTrigger("Dead");
        }
    }
}
