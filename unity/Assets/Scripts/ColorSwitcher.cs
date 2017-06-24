using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable]
public class ColorSwitcherEvent :  UnityEvent<ColorSwitcher>
{
}

public class ColorSwitcher : MonoBehaviour
{
    public int index = 0;
    public List<Color> colors;
    public Material[] targetMaterials;
    public Collider[] colliderList;
    public ReflectionProbe[] reflectionProbe;


    [SerializeField]
    private ColorSwitcherEvent m_OnHitSelection = new ColorSwitcherEvent();
    public ColorSwitcherEvent onHitSelection
    {
        get { return this.m_OnHitSelection; }
        private set { this.m_OnHitSelection = value; }
    }


    void Start()
    {
        if (colliderList.Length < 1)
            colliderList = GetComponentsInChildren<Collider>();
    }
	
    void OnDisable()
    {
        RenderSettings.skybox = null;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 32.0f))
            {
                OnRaycastHit(hit);
            }
        }
    }

    void OnRaycastHit(RaycastHit hit)
    {
        if (hit.transform.tag == "ColorSwitchable")
        {
            bool hit_this = false;
            int j = 0;
            while (j < colliderList.Length && !hit_this)
            {
                hit_this = (colliderList[j] == hit.collider);
                ++j;
            }
            if (hit_this)
            {
                onHitSelection.Invoke(this);
                
                //SwitchColor(++index); // used for debug 
            }

        }
    }

  

    public void SwitchColor(int index)
    {
        foreach (Material m in targetMaterials)
            m.color = colors[index % colors.Count];

        foreach (ReflectionProbe r in reflectionProbe)
            r.RenderProbe();
    }

}
