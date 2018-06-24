using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speed = 150f;
    public TouchTrigger trigger;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(transform.rotation.x, transform.rotation.y, 10f, 0f), 0.05f* Time.deltaTime);

        Vector3 tey = transform.rotation.eulerAngles;
        tey.z = 45f;

        if (trigger.isOn)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -90f), speed * Time.deltaTime);
        }
    }
}
