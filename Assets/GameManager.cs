using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public static bool levelFinished = false;


    // Controla se o usuario está usando keyboard ou um controle
    public bool onController = false;
    public GameObject pauseMenu;

    public int nextScene;
    public int currentScene;
    public int timeScaled = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Ensures that there aren't multiple Singletons

        instance = this;
    }

    // Use this for initialization
    void Start () {
        levelFinished = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            onController = false;
        }

        // Pausando Jogo
        if (Input.GetButtonDown("Pause") && levelFinished == false)
        {
            Pause();
        }

        if (levelFinished == true)
        {
            Time.timeScale = 0;
        }
    }

    public static void Pause()
    {
        instance.pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public static void Resume()
    {
        instance.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
    }
}
