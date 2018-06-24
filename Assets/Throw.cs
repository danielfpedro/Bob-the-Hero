using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

    public TouchTrigger trigger;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (trigger.isOn && trigger.target)
        {
            Rigidbody2D rb = trigger.target.GetComponent<Rigidbody2D>();
            Vector3 dir = Quaternion.AngleAxis(90, Vector2.up) * Vector2.right;
            // rb.AddForce(dir * 1000f, ForceMode2D.Impulse);

            rb.AddRelativeForce(new Vector2(20f, 20f), ForceMode2D.Impulse);
        }
	}
}
