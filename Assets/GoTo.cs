using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTo : MonoBehaviour {

    public Vector2 positionToGo;
    [HideInInspector]
    public Vector2 initialPosition;

    public float delay = 0;
    private float time = 0;

    public float smooth = 3f;
    private bool lastState;
    public TouchTrigger trigger;

    // Use this for initialization
    void Start () {
        initialPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (trigger.isOn)
        {
            StartCoroutine(moveSmooth(positionToGo));
            Invoke("moveSmooth", delay);
        } else
        {
            moveSmooth(initialPosition);
        }
    }

    private IEnumerator moveSmooth(Vector2 goTo)
    {
        yield return new WaitForSeconds(delay);
        transform.position = Vector2.MoveTowards(transform.position, goTo, smooth * Time.deltaTime);
    }
}
