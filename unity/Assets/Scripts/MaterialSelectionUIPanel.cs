using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;



[RequireComponent(typeof(UIController))]
public class MaterialSelectionUIPanel : MonoBehaviour
{

    public UnityEngine.UI.Button[] materialButtons;

    private RectTransform rectTransform = null;

    private UIController uiController = null;

    [SerializeField]
    private UnityEvent m_OnHitSelection = new UnityEvent();
    public UnityEvent onHitSelection
    {
        get { return this.m_OnHitSelection; }
        private set { this.m_OnHitSelection = value; }
    }

    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        uiController = this.GetComponent<UIController>();
    }


    public void ShowHide(MaterialDownloaderManager matdownlMngr)
    {
        rectTransform.position = Input.mousePosition;

        int smaller_list = Mathf.Min(materialButtons.Length, matdownlMngr.materials.Count);
        for (int i = 0; i < smaller_list; ++i)
        {
            materialButtons[i].onClick.RemoveAllListeners();

            UnityEngine.UI.RawImage rawImage = materialButtons[i].GetComponent<UnityEngine.UI.RawImage>();
            if (rawImage != null)
            {
                //rawImage.material = matdownlMngr.materials[i];
                rawImage.texture = matdownlMngr.materials[i].mainTexture;
            }
            int mat_index = i;
            materialButtons[i].onClick.AddListener(() => matdownlMngr.SwitchMaterial(mat_index));
        }

        if (uiController.isShow)
        {
            uiController.Hide();
        }
        else
        {
            uiController.Show();
        }
    }

}
