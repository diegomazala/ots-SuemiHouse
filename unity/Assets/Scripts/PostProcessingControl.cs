using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingControl : MonoBehaviour
{
    public UnityEngine.PostProcessing.PostProcessingBehaviour postProcessing;

	
	void Start ()
    {
		
	}
	
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.I))
            postProcessing.enabled = !postProcessing.enabled;

    }
}
