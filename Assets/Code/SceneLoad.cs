using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoad : MonoBehaviour
{
    public Slider progressBar;
    public Text loadText;
    public static string loadScene;
    public static int loadType;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    public static void LoadSceneHandle(string _name, int _loadType)
    {
        loadScene = _name;
        loadType = _loadType;
        SceneManager.LoadScene("loading");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        // Start loading the scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if(loadType == 0)
                Debug.Log("new");
            else if(loadType == 0)
                Debug.Log("continue");
            // If the scene loading progress is less than 0.9, update the progress bar towards 0.9
            if (operation.progress < 0.9f)
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 0.9f, Time.deltaTime);
            }
            // If the scene loading progress is greater than or equal to 0.9, update the progress bar towards 1
            else
            {
                progressBar.value = Mathf.MoveTowards(progressBar.value, 1f, Time.deltaTime);

                // When the progress bar reaches 1, update the text to prompt the user
                if (progressBar.value >= 1f)
                {
                    loadText.text = "Press spacebar";

                    // Allow scene activation when the spacebar is pressed
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        operation.allowSceneActivation = true;
                    }
                }
            }

            yield return null;
        }
    }
}


