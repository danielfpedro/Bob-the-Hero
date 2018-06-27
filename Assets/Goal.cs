using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public GameObject menu;
    public TouchTrigger switcher;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (switcher != null && switcher.isOn)
        {
            menu.SetActive(true);
            GameManager.levelFinished = true;
        }
    }
}
