using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;



[RequireComponent(typeof(UIController))]
public class ColorSwitcherUIPanel : MonoBehaviour
{
    public UnityEngine.UI.Button buttonTemplate;
    public UnityEngine.UI.LayoutGroup layoutGroup;
    public List<UnityEngine.UI.Button> colorButtons;

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

    private void OnDisable()
    {
        if (uiController.isShow)
            uiController.Hide();
    }

    public void ShowHide(ColorSwitcher colorSwitcher)
    {
        // Destroy buttons
        foreach (var button in colorButtons)
            Destroy(button.gameObject);
        colorButtons.Clear();


        if (uiController.isShow)
        {
            // Hide popup
            uiController.Hide();
                        
        }
        else
        {
            rectTransform.position = Input.mousePosition;

            int color_count = colorSwitcher.colors.Count;
            for (int i = 0; i < color_count; ++i)
            {
                colorButtons.Add(Instantiate<UnityEngine.UI.Button>(buttonTemplate));
                UnityEngine.UI.Button button = colorButtons[colorButtons.Count - 1];
                button.image.color = colorSwitcher.colors[i];
                int color_index = i;    // it must have this copy, otherwise, the listener will not work properly
                button.onClick.AddListener(() => colorSwitcher.SwitchColor(color_index));
                button.transform.SetParent(layoutGroup.transform);
                button.transform.localScale = Vector3.one;
            }

            uiController.Show();
        }




#if false
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
#endif
    }

}
