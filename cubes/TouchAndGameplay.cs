using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//every user interaction starts from this script

public class TouchAndGameplay : MonoBehaviour
{

    public GameObject mainCamera;
    public CubeMovement cubeMovement;
    public GameObject secondCamera;
    public Destroyer destroyer;
    int threeD;
    bool paused = new bool();
    public Animator pausePanelAnimator;
    bool escape = new bool();
    public List<Vector2> destructionFeeder = new List<Vector2>();

    // Use this for initialization
    void Start()
    {
        threeD = PlayerPrefs.GetInt("gameStyle", 1);
    }

    // Update is called once per frame
    void Update()
    {
        RegisterBackCommand();
        //the following code is only for detection
        if (!paused)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch firstTouch = Input.GetTouch(i);
                if (firstTouch.phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = new Vector3(firstTouch.position.x, firstTouch.position.y, -mainCamera.transform.position.z);    //the z compontent should be 'the z component of the plane containing the game objects' - z component of the main camera
                    Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition) - ((threeD + 1) % 2) * new Vector3(10, 0, 0);
                    Collider[] colliders = Physics.OverlapSphere(worldTouchPosition, 0.3f);
                    if (colliders.Length > 0)
                    {
                        GameObject touchedObject = colliders[0].gameObject;
                        //this for-loop makes sure that if there are more than 1 cubes, the color changing cube is selected
                        if (touchedObject.tag == "cube")
                        {
                            //change the following code as there will never be two cubes in overalapping, but there can be a cube and a special cube overlapping - DONE
                            for (int j = 0; j < colliders.Length; j++)
                            {
                                GameObject registeredGameObject = colliders[j].gameObject;
                                if (registeredGameObject.GetComponent<SetUpColor>() == null)
                                {
                                    touchedObject = colliders[j].gameObject;
                                    break;
                                }
                            }
                            //have to do the same loop twice so that it seached for the 'special cube s'in all the colliders and then proceeds to 'cubes'
                            for (int j = 0; j < colliders.Length; j++)
                            {
                                GameObject registeredGameObject = colliders[j].gameObject;
                                if (registeredGameObject.GetComponent<SetUpColor>().getIndex() == GameVariables.currentIndex)
                                {
                                    touchedObject = colliders[j].gameObject;
                                    break;
                                }
                            }
                            if (touchedObject.GetComponent<SetUpColor>().checkIndex())
                            {
                                int a = touchedObject.GetComponent<SetUpColor>().getListIndex();
                                Debug.Log(a);
                                cubeMovement.DestroyCubeAt(a);
                            }
                        }
                        else if (touchedObject.tag == "specialCube")
                            touchedObject.GetComponent<SpecialCubeMovement>().touched();
                    }
                }
            }
        }
    }

    public void PauseUnpause(float time)
    {
        StartCoroutine(DelayedAction(time));
    }

    IEnumerator DelayedAction(float time)
    {
        yield return new WaitForSeconds(time);
        if (escape)
            TimeController.timeScale = 0;
        else
            TimeController.timeScale = (TimeController.timeScale + 1) % 2;
        paused = !paused;
        escape = false;
    }

    void RegisterBackCommand()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanelAnimator.GetBool("Pause"))
                SceneManager.LoadScene(0);
            pausePanelAnimator.SetBool("Pause", !pausePanelAnimator.GetBool("Pause"));
            escape = true;
            PauseUnpause(0);
        }
    }
}
