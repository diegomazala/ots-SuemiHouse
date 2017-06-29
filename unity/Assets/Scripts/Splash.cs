using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

	public string sceneToLoad; // scene name which will be loaded
	public Text loadingText;
	public float delay = 0.25f; // delay between adding another dot

	private int counter = 0;
	private float timeAccu = 0f;
	private string loadingString; // get string from loadingText; we will add dots to it

	void Start () {
		loadingString = loadingText.text;
		StartCoroutine (LoadGame ()); // start loading coroutine
	}

	void Update () {
		timeAccu += Time.deltaTime;
		if (timeAccu >= delay)
        { 
            // if delay passed
			loadingText.text = loadingString.PadRight (loadingString.Length + counter % 4, '.'); // add another dot
			counter++;
			timeAccu = 0f;
		}
	}

	private IEnumerator LoadGame ()
    {
		// here is dummy waint time to simulate loading heavy scene; remove it in target application!
		yield return new WaitForSeconds (3f);
		AsyncOperation async = SceneManager.LoadSceneAsync (sceneToLoad); // load scene by its name asychronically

        if (sceneToLoad.Length > 0)
            async = SceneManager.LoadSceneAsync(sceneToLoad); // load scene by its name asychronically
        else
            async = SceneManager.LoadSceneAsync(1); // load scene by its index asychronically

        while (!async.isDone)
        { 
            // wait until scene loading is done
			yield return null; // wait and let to update splash screen
		}
	}
}
