using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFinish : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            PlayerPrefs.SetFloat("LevelStarted", 0);
            GameManager.instance.GoToNextScene();
        }
    }
}
