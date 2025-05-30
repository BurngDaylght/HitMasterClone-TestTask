using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void RestartSceneWithDelay(float delay)
    {
        StartCoroutine(LoadSceneCoroutine(SceneManager.GetActiveScene().name, delay));
    }

    public void LoadSceneWithDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, delay));
    }
    
    public void ExitGameWithDelay(float delay)
    {
        StartCoroutine(ExitGameCoroutine(delay));
    }
    
    private IEnumerator LoadSceneCoroutine(string sceneName, float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    
    private IEnumerator ExitGameCoroutine(float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}