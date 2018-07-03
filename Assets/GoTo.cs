using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTo : MonoBehaviour {

    public Vector2 move;

    [HideInInspector]
    public Vector2 initialPosition;

    public float delay = 0;
    public float time = 0;

    public float smooth = 3f;
    private bool lastState;
    public TouchTrigger trigger;

    public bool triggerState;

    // Use this for initialization
    void Start () {
        initialPosition = transform.position;
    }
	
	void Update () {
        if (trigger != null)
        {
            triggerState = trigger.isOn;
        }
        
        if (triggerState == true)
        {
            MoveSmooth(new Vector2(initialPosition.x + move.x, initialPosition.y + move.y));
        }
        else
        {
            MoveSmooth(initialPosition);
        }
    }

    private void MoveSmooth(Vector2 goTo)
    {
        transform.position = Vector2.MoveTowards(transform.position, goTo, smooth * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawSphere(new Vector2(transform.position.x + move.x, transform.position.y + move.y), .1f);

        float maxX = 0f;
        float ySum = 0f;

        BoxCollider2D rb = GetComponent<BoxCollider2D>();
        maxX = rb.bounds.size.x;
        ySum= rb.bounds.size.y;

        Gizmos.DrawWireCube(new Vector2((transform.position.x + rb.offset.x) + move.x, (transform.position.y + rb.offset.y) + move.y), new Vector2(maxX, ySum));
    }
}
