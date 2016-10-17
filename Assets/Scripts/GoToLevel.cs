using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GoToLevel : MonoBehaviour {

    public string LevelName;

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            SceneManager.LoadScene(LevelName);
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(LevelName);
    }

    public void StartButton()
    {
        SceneManager.LoadScene(LevelName);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(LevelName);
    }

    public void YesButton()
    {
        //SceneManager.Quit();
    }

    public void NoButton()
    {
        SceneManager.LoadScene(LevelName);
    }
}
