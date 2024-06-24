using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    

    public void LoadSceneMethod(string sceneNameToLoad)
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }


    public void Quit()
    {
        Application.Quit();
    }
}