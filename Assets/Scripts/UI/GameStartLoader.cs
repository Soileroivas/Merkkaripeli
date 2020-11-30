using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameStartLoader : MonoBehaviour
{

    public int sceneToLoad;
    public Slider progressBar;
 
   


    // Start is called before the first frame update
    void Start()
    {
        progressBar.GetComponent<Slider>();
        StartCoroutine(LoadLevelAsync(sceneToLoad));
    }


    IEnumerator LoadLevelAsync(int sceneIndex)
    {
        // Delay for download.
        yield return new WaitForSeconds(2.0f);

        // Loading scene async and getting loading progress.
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        // While loading isn't done.
        while (!operation.isDone)
        {
            
            // Load progress to the loadbar.
            progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
         
        }
    }

}

