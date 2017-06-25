using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineMachineCameraSwitcher : MonoBehaviour
{
    public KeyCode[] keyForDisableCineMachine;
    public Cinemachine.CinemachineVirtualCamera[] cineCameras;


    void Start()
    {
        if (cineCameras.Length < 1)
            cineCameras = FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();

        DisableCineMachine();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DisableCineMachine();

		foreach(KeyCode k in keyForDisableCineMachine)
        {
            if (Input.GetKeyDown(k))
            {
                DisableCineMachine();
            }
        }

        for (int i = 0; i < cineCameras.Length; ++i)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                EnableCamera(i);
            }
        }

	}


    public void DisableCineMachine()
    {
        foreach (Cinemachine.CinemachineVirtualCamera c in cineCameras)
        {
            c.enabled = false;
        }
    }


    public void EnableCamera(int index)
    {
        DisableCineMachine();
        cineCameras[index % cineCameras.Length].enabled = true;
    }

}
