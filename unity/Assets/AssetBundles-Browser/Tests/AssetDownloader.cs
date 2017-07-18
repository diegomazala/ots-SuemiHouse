using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

class AssetDownloader : MonoBehaviour
{
    public string GameObjectAssetBundleUrl = "http://localhost:8012/unity/bundles/box/cube.unity3d";
    public string GameObjectAssetName = "cube";
    public string SkyboxAssetBundleUrl = "http://localhost:8012/unity/bundles/skyboxes/skybox.unity3d";
    public string SkyboxAssetName = "sea";

    public GameObject assetInstantiated;

    private AssetBundle assetBundle = null;

    void OnDisable()
    {
        if (assetBundle != null)
            assetBundle.Unload(true);
    }

    public void DownloadGameObject()
    {
        StartCoroutine(GetAssetBundle_GameObject(GameObjectAssetBundleUrl, GameObjectAssetName));
    }

    public void DownloadSkybox()
    {
        StartCoroutine(GetAssetBundle_Skybox(SkyboxAssetBundleUrl, SkyboxAssetName));
    }


    IEnumerator GetAssetBundle_Skybox(string assetBundleUrl, string assetName)
    {
        UnityWebRequest www = UnityWebRequest.GetAssetBundle(assetBundleUrl);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            assetBundle = DownloadHandlerAssetBundle.GetContent(www);
            

            Material m = assetBundle.LoadAsset<Material>(assetName);
            m.shader = Shader.Find("Skybox/Cubemap");
            RenderSettings.skybox = m;

            assetBundle.Unload(false);
        }

    }

    IEnumerator GetAssetBundle_GameObject(string assetBundleUrl, string assetName)
    {
        UnityWebRequest www = UnityWebRequest.GetAssetBundle(assetBundleUrl);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            assetBundle = DownloadHandlerAssetBundle.GetContent(www);
            assetInstantiated = Instantiate(assetBundle.LoadAsset<GameObject>(assetName));
            assetBundle.Unload(false);
        }

    }
}