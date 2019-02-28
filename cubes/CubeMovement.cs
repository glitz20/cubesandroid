using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script to be applied to the gameController

public class CubeData
{
    public List<Vector3> cubePosition;
    public List<int> cubeIndex;
    public bool deathAnimation;

    public CubeData(List<Vector3> position, List<int> index, bool animation = false)
    {
        cubePosition = position;
        cubeIndex = index;
        deathAnimation = false;
    }

    public CubeData()
    {
        cubePosition = new List<Vector3>();
        cubeIndex = new List<int>();
        deathAnimation = false;
    }
}

public class CubeMovement : MonoBehaviour {

    public GameObject cube;
    public GameVariables gameVariables;
    float enterTime = new float();
    List<GameObject> cubeList = new List<GameObject>();
    public int indexDeleted = 0;
    public int listSize = 0;
    public GameObject colorChangingCube;
    public GameObject circle;
    public Text scoreText;
    public CubeData data = new CubeData();
    public Text specialCubePriority;
    public Text specialCubeSignificance;
    public CameraScript cameraScript;
    int currentListIndex = new int();

	// Use this for initialization
	void Start () {
        enterTime = gameVariables.timeBetweenTwoCubes;              //this is set to be equal to the spawn timer so that 1 cube is produced as soon as the game starts
        gameVariables = GetComponent<GameVariables>();
    }

    // Update is called once per frame
    void Update () {
        cube.transform.position = new Vector3(0, 4.5f * cameraScript.getMultiplier(), 0);
        listSize = cubeList.Count;
        enterTime += Time.deltaTime * TimeController.timeScale;
        if (enterTime >= gameVariables.timeBetweenTwoCubes)
        {
            enterTime = 0;
            cubeList.Add(Instantiate(cube));
            cubeList[cubeList.Count - 1].GetComponent<SetUpColor>().setListIndex(currentListIndex);
            cubeList[cubeList.Count - 1].transform.position = new Vector3(Random.value * 4f - 2, 4 * cameraScript.getMultiplier(), 0);
            cubeList[cubeList.Count - 1].transform.eulerAngles = new Vector3(Random.value * 360, Random.value * 360, Random.value * 360);
            //adding the information to CubeData data
            int index = (int)(Random.value * 3) % 3;
            cubeList[cubeList.Count - 1].GetComponent<SetUpColor>().setIndex(index);
            data.cubePosition.Add(cubeList[cubeList.Count - 1].transform.position);
            data.cubeIndex.Add(index);
            //data.cubeIndex.Add(cubeList[cubeList.Count - 1].GetComponent<SetUpColor>().getIndex());

            currentListIndex++;
        }
        for (int i = 0; i < cubeList.Count; i++)
        {
            Vector3 change = new Vector3(0, GameVariables.cubeVerticalMovement * Time.deltaTime * TimeController.timeScale, 0);
            cubeList[i].transform.position -= change;
            data.cubePosition[i] -= change;
        }
        if (gameVariables.timeUntilChange <= 0)
        {
            SpawnColorChangingCube();
        }
    }



    public void DestroyCubeAt(int a, float timer=0)
    {
        StartCoroutine(delayedDestruction(a, timer));
    }

    IEnumerator delayedDestruction(int a, float timer)
    {
        yield return new WaitForSeconds(timer);
        int requiredListIndex = BinarySearch(a, 0, cubeList.Count);
        Debug.Log("PASSED");
        Destroy(cubeList[requiredListIndex]);
        cubeList.RemoveAt(requiredListIndex);
        Destroy2D(requiredListIndex);
        Debug.Log("Destroyed at: " + requiredListIndex);
        int score = int.Parse(scoreText.text);
        score++;
        scoreText.text = score.ToString();
    }

    void SpawnColorChangingCube()
    {
        GameObject specialCube = Instantiate(colorChangingCube);
        specialCube.transform.position = new Vector3(Random.value * 4f - 2, 4 * cameraScript.getMultiplier(), 0);
        //specialCube.transform.eulerAngles = new Vector3(Random.value * 360, Random.value * 360, Random.value * 360);
        specialCube.AddComponent<SpecialCubeMovement>();
        SpecialCubeMovement special = specialCube.GetComponent<SpecialCubeMovement>();
        special.circle = circle;
        special.gameVariables = gameVariables;
        gameVariables.timeUntilChange = 100;
        special.specialCubePriority = specialCubePriority;
        special.specialCubeSignificance = specialCubeSignificance;
        specialCubePriority.GetComponent<Animator>().SetTrigger("Show");
    }

    void Destroy2D(int listIndex)
    {
        data.cubePosition.RemoveAt(listIndex);
        data.cubeIndex.RemoveAt(listIndex);
    }

    int BinarySearch(int a, int initial, int final)
    {
        int midpoint = (initial + final) / 2;
        int midValue = cubeList[midpoint].GetComponent<SetUpColor>().getListIndex();
        if (a == midValue)
            return midpoint;
        else if (a < midValue)
            return BinarySearch(a, initial, midpoint);
        else
            return BinarySearch(a, midpoint, final);
    }
}
