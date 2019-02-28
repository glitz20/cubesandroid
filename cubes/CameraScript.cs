using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    Camera selfCamera;
    float multiplier;
    Vector3 position = new Vector3();
    bool dead = new bool();
    float size;
    float animationLifeTime = new float();

	// Use this for initialization
	void Start () {
        selfCamera = GetComponent<Camera>();
        multiplier = (10f / 16f) / selfCamera.aspect;
        if (selfCamera.orthographic)
        {
            selfCamera.orthographicSize *= multiplier;
            size = selfCamera.orthographicSize;
        }
        else
        {
            selfCamera.fieldOfView = 2 * Mathf.Atan(multiplier * 0.4f) * Mathf.Rad2Deg;
            size = selfCamera.fieldOfView;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (dead)
        {
            if (animationLifeTime <= 1)
            {
                transform.position += new Vector3(position.x * Time.deltaTime, position.y * Time.deltaTime);
                if (selfCamera.orthographic)
                    selfCamera.orthographicSize -= (size / 2) * Time.deltaTime;
                else
                    selfCamera.fieldOfView -= (size / 2) * Time.deltaTime;
                animationLifeTime += Time.deltaTime;
            }
            else
            {
                animationLifeTime = 0;
                dead = false;
            }
        }
	}

    public float getMultiplier() { return multiplier; }

    public void DeathFocus(Vector3 deadPosition)
    {
        position = deadPosition;
        dead = true;
    }
}
