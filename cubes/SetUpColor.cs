using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script does more than just setting up the color

    /// <summary>
    /// The listIndex of the cubes is not in alignment to the real index, after the cubes are destroyed
    /// make UMLs before procceding any further
    /// </summary>

public class SetUpColor : MonoBehaviour {

    public GameVariables gameVariables;
    public Text scoreText;
    public HighScore highScore;
    public Canvas canvas;
    public CameraScript cameraScript3D;
    public CameraScript cameraScript2D;
    public SquareInformation squares;
    Material cubeMaterial;
    int index;
    public int listIndex = new int();
    bool mistake = new bool();
    float age = new float();

	// Use this for initialization
	void Start () {
        cubeMaterial = GetComponent<Renderer>().material;
        cubeMaterial.color = gameVariables.cubeColors[index];
    }

    // Update is called once per frame
    void Update () {
        if (mistake)
        {
            age += Time.deltaTime;
            Color change = new Color(0, 0, 0, Mathf.Sin(2.5f * age));
            cubeMaterial.color -= Time.deltaTime * change;
        }
    }

    public bool checkIndex() {
        if (index == GameVariables.currentIndex)
            return true;      //the cube also has to be removed from the list in CubeMovement
        else
        {
            TimeController.timeScale = 0;
            highScore.CheckHighScore(int.Parse(scoreText.text));
            mistake = true;
            canvas.GetComponent<Animator>().SetTrigger("Dead");
            if (squares.gameObject.activeSelf)
                squares.Blink(listIndex);
            cameraScript3D.DeathFocus(transform.position);
            cameraScript2D.DeathFocus(transform.position);
            return false;
        }
    }

    public void setListIndex(int i) { listIndex = i; }
    public int getListIndex() { return listIndex; }

    public void setIndex(int i) { index = i; }
    public int getIndex() { return index; }

    public void Death(){ mistake = true; }
}
