using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OnVictoryClick()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void OnVictoryBackClick()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
