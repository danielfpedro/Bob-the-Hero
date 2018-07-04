using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetFloat("LevelStarted", 0);
        SceneManager.LoadScene(1);
    }
}
