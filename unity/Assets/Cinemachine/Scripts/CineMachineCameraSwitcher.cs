using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineMachineCameraSwitcher : MonoBehaviour
{
    public int Current = 0;
    public KeyCode[] keyForDisableCineMachine;
    public Cinemachine.CinemachineVirtualCamera[] cineCameras;


    void Start()
    {
        if (cineCameras.Length < 1)
            cineCameras = FindObjectsOfType<Cinemachine.CinemachineVirtualCamera>();


        DisableCineMachine();
        Current = 0;
        cineCameras[Current % cineCameras.Length].enabled = true;
    }


    void Update()
    {
        int c = 0;
        foreach (KeyCode k in keyForDisableCineMachine)
        {
            if (Input.GetKeyDown(k))
            {
                cineCameras[Current % cineCameras.Length].enabled = false;
                Debug.Log("Off/On: " + Current.ToString() + " -> " + c.ToString());
                Current = c;
                cineCameras[Current % cineCameras.Length].enabled = true;
            }
            c++;
        }
    }

    

    public void EnableCamera(int index)
    {
        cineCameras[Current % cineCameras.Length].enabled = false;
        Current = index;
        cineCameras[Current % cineCameras.Length].enabled = true;
    }

    public void DisableCineMachine()
    {
        foreach (Cinemachine.CinemachineVirtualCamera c in cineCameras)
        {
            c.enabled = false;
        }
    }

}
