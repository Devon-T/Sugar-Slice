using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraSelector : MonoBehaviour {

    GameObject firstPersonCamera;
    GameObject thirdPersonCamera;
    bool updateCamera;

	// Use this for initialization
	void Start () {
        firstPersonCamera = GameObject.FindGameObjectWithTag("FirstPerson");
        thirdPersonCamera = GameObject.FindGameObjectWithTag("ThirdPerson");
        firstPersonCamera.SetActive(false);
        updateCamera = true;
	}
	
	// Update is called once per frame
	void Update () {
            ChangeCamera();
	}

    void ChangeCamera()
    {
        
        if (CrossPlatformInputManager.GetButton("Fire1") && updateCamera)
        {
            if (thirdPersonCamera.activeSelf)
            {
                updateCamera = false;
                thirdPersonCamera.SetActive(false);
                firstPersonCamera.SetActive(true);
                print("updated");
            } else
            {
                updateCamera = false;
                firstPersonCamera.SetActive(false);
                thirdPersonCamera.SetActive(true);
            }
            updateCamera = true;
        }
    }
}
