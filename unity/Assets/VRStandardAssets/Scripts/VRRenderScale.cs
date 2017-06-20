using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class VRRenderScale : MonoBehaviour
{
	[SerializeField]
    private float m_RenderScale = 1f;              //The render scale. Higher numbers = better quality, but trades performance
    [SerializeField]
    private float m_RenderScaleMin = 0.5f;              //The render scale. Higher numbers = better quality, but trades performance
    [SerializeField]
    private float m_RenderScaleMax = 4f;              //The render scale. Higher numbers = better quality, but trades performance

    void Start ()
	{
		VRSettings.renderScale = m_RenderScale;
	}

    void Update()
    {
        bool isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isCtrlKeyDown = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isShiftKeyDown)
                m_RenderScale += 0.5f;
            else if (isCtrlKeyDown)
                m_RenderScale = 1.0f;
            else
                m_RenderScale -= 0.5f;

            if (m_RenderScale > m_RenderScaleMax)
                m_RenderScale = m_RenderScaleMax;

            if (m_RenderScale < m_RenderScaleMin)
                m_RenderScale = m_RenderScaleMin;

            VRSettings.renderScale = m_RenderScale;
        }

#if UNITY_EDITOR
        VRSettings.renderScale = m_RenderScale;
#endif

    }
}
