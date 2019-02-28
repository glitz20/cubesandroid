using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareInformation : MonoBehaviour {

    //this one goes with 2DGameController

    public CubeMovement cubeMovement;
    public GameObject square;
    public GameVariables gameVariables;
    List<GameObject> squareList = new List<GameObject>();
    CubeData data = new CubeData();
    public CameraScript cameraScript;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        square.transform.localPosition = new Vector3(0, 4.5f * cameraScript.getMultiplier(), 0);
        data = cubeMovement.data;
        //updating the number of squares
        int sizeDifference = data.cubePosition.Count - squareList.Count;
        if (sizeDifference!=0)
        {
            if (sizeDifference > 0)
                for (int i = 0; i < sizeDifference; i++)
                    squareList.Add(Instantiate(square));
            else
                for (int i = 0; i < (-1) * sizeDifference; i++)
                {
                    //since these squares don't interact with the gameplay, any of the squares can be deleted
                    Destroy(squareList[0]);
                    squareList.RemoveAt(0);
                }
        }
        //now that the number of squares is the same,
        //updating the position and color
        for(int i=0;i<squareList.Count;i++)
        {
            squareList[i].transform.position = data.cubePosition[i] + new Vector3(10, 0, 0);
            squareList[i].GetComponent<SpriteRenderer>().color = gameVariables.cubeColors[data.cubeIndex[i]];
        }
	}

    public void Blink(int listIndex)
    {
        squareList[listIndex].GetComponent<Animator>().SetTrigger("Dead");
    }
}
