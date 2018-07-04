using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            GameManager.Resume();
        }
        if (Input.GetButtonDown("Jump"))
        {
            GameManager.instance.RestartLevel();
        }
    }
}
