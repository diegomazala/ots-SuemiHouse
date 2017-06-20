using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable]
public class MaterialSwitcherEvent :  UnityEvent<MaterialDownloaderManager>
{
}

public class MaterialDownloaderManager : MonoBehaviour
{

    public bool useLocalFolder = true;
    public string AssetBundleFolderUrl;
    public string AssetBundleName;

    public int index = 0;
    public List<Material> materials;

    public string[] assetNames;

    private AssetBundleManifest assetBundleManifest;

    

    public Collider[] colliderList;
    public MeshRenderer[] rendererList;
    public ReflectionProbe reflectProbe;

    public UnityEngine.UI.Text screenText;

    [SerializeField]
    private MaterialSwitcherEvent m_OnHitSelection = new MaterialSwitcherEvent();
    public MaterialSwitcherEvent onHitSelection
    {
        get { return this.m_OnHitSelection; }
        private set { this.m_OnHitSelection = value; }
    }


    void Start()
    {
        materials.Clear();
        if (screenText != null)
            screenText.text = "Loading...";


        if (useLocalFolder)
            StartCoroutine(DownloadMaterialsAssetBundle(@"file:///" + Application.streamingAssetsPath + @"/" + AssetBundleName, AssetBundleName));
        else
            StartCoroutine(DownloadMaterialsAssetBundle(AssetBundleFolderUrl, AssetBundleName));
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
        if (hit.transform.tag == "Decorable")
        {
            //    index = ++index % materials.Count;
            //    for (int j = 0; j < colliderList.Length; ++j)
            //    {
            //        //ShowUIGroup(colliderList[j] == hit.collider);
            //        if (hit.transform.gameObject == colliderList[j].gameObject)
            //            SwitchMaterial(index);
            //    }

            bool hit_this = false;
            int j = 0;
            while (j < colliderList.Length && !hit_this )
            {
                hit_this = (colliderList[j] == hit.collider);
                ++j;
            }
            if (hit_this)
                onHitSelection.Invoke(this);

            
        }
    }

  

    public void SwitchMaterial(int index)
    {
        foreach (Renderer r in rendererList)
            r.material = materials[index % materials.Count];

        reflectProbe.RenderProbe();
    }


    IEnumerator DownloadMaterialsAssetBundle(string assetBundleFolderUrl, string assetBundleName)
    {
        string assetBundleUrl = assetBundleFolderUrl + "/" + assetBundleName;
        UnityWebRequest www = UnityWebRequest.GetAssetBundle(assetBundleUrl);
        yield return www.Send();

        while (!www.downloadHandler.isDone)
        {
            if (screenText)
                screenText.text = (www.downloadProgress * 100.0f).ToString() + "%";
            yield return null;
        }

        if (screenText)
            screenText.enabled = false;

        if (www.isError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            this.assetNames = bundle.GetAllAssetNames();
            yield return null;

            if (bundle == null)
            {
                Debug.LogErrorFormat(string.Format("Could not load AssetBundle manifest. URL: {0}", assetBundleUrl));
                yield break;
            }
            else
            {
                assetBundleManifest = bundle.LoadAsset<AssetBundleManifest>("assetbundlemanifest");
                assetNames = assetBundleManifest.GetAllAssetBundles();
                yield return null;


                foreach (string sub_bundle_name in assetBundleManifest.GetAllAssetBundles())
                {
                    yield return StartCoroutine(DownloadMaterialBundle(assetBundleFolderUrl + "/" + sub_bundle_name));
                }

                bundle.Unload(false);
                yield break;
            }

        }

    }



    private IEnumerator DownloadMaterialBundle(string assetBundleUrl)
    {
        using (UnityWebRequest www = UnityWebRequest.GetAssetBundle(assetBundleUrl))
        {
            yield return www.Send();

            if (!www.isError)
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

                if (bundle == null)
                {
                    Debug.LogErrorFormat(string.Format("Could not load AssetBundle manifest. URL: {0}", assetBundleUrl));
                }
                else
                {
                    foreach (string sub_bundle_name in bundle.GetAllAssetNames())
                    {
                        Material mat = bundle.LoadAsset<Material>(sub_bundle_name);
                        materials.Add(mat);
                    }
                    bundle.Unload(false);
                    yield break;
                }
            }
        }
    }

}
