using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    // Controla se o usuario está usando keyboard ou um controle
    public bool onController = false;
    public GameObject pauseMenu;

    [SerializeField]
    public string nextScene;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Ensures that there aren't multiple Singletons

        instance = this;

        Time.timeScale = 1;
        Cursor.visible = false;
    }

    // Use this for initialization
    void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            onController = false;
        }

        // Pausando Jogo
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
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

    public static void GoToNextScene()
    {
        SceneManager.LoadScene(GameManager.instance.nextScene);
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
