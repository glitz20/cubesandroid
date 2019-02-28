using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialCubeMovement : MonoBehaviour {

    public GameObject circle;
    public GameVariables gameVariables;
    bool mistake = new bool();
    float age = new float();
    Material cubeMaterial;
    public Text specialCubePriority;
    public Text specialCubeSignificance;

	// Use this for initialization
	void Start () {
        cubeMaterial = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        int changeCubeColor = (int)(Random.value * 3) % 3;
        cubeMaterial.color = gameVariables.cubeColors[changeCubeColor];
        cubeMaterial.SetColor("_EmissionColor", gameVariables.cubeColors[changeCubeColor]);
        transform.position -= new Vector3(0, GameVariables.cubeVerticalMovement * Time.deltaTime * TimeController.timeScale, 0);
        if (mistake)
        {
            age += Time.deltaTime;
            cubeMaterial.color -= new Color(0, 0, 0, Mathf.Sin(age * Mathf.PI));
        }

    }

    public void touched()
    {
        GetComponent<Animator>().SetTrigger("Pop");
        StartCoroutine(delayedPop());
        circle.GetComponent<Animator>().SetTrigger("IndexChange");
        specialCubeSignificance.GetComponent<Animator>().SetTrigger("Show");
        specialCubePriority.GetComponent<Animator>().SetTrigger("Close");
        StartCoroutine(delayedIndexChange());
    }

    IEnumerator delayedPop()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    IEnumerator delayedIndexChange()
    {
        yield return new WaitForSeconds(0.5f);
        GameVariables.currentIndex = (int)(Random.value * 3) % 3;
        gameVariables.timeUntilChange = 1 + 1 / (TimeController.getGameTime() + 0.03f) + Random.value * 30;       //added 1 sec to the time so that the animation can always run fully

    }

    public void Death() { mistake = true; }
}
