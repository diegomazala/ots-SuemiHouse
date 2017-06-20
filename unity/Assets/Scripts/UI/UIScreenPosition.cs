using UnityEngine;

/// <summary>
/// Place an UI element to a world position
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class UIScreenPosition : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        // Get the rect transform
        this.rectTransform = GetComponent<RectTransform>();
    }

    public void SetPosition(Vector3 position)
    {
        this.rectTransform.position = position;
    }

    public void SetPositionFromMouse()
    {
        this.rectTransform.position = Input.mousePosition;
    }

#if false
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
            SetPositionFromMouse();
        }
    }
#endif

}