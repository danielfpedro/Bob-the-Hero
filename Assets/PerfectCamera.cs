using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectCamera : MonoBehaviour {

    public Transform target;

    [Header("Dead Zone")]
    public float xDeadZone;
    public float yDeadZone;

    public float xSmooth = 5f;
    public float ySmooth = 5f;

    public float lookAheadFactor;
    public float lookAheadThreshold;
    public float yOffset;

    public bool centeredX;
    public bool centeredY;

    private float xMove;
    private float yMove;

    public Vector2 staticTey;

    public bool facingRight;

    public float lastXPosition;

    public float lastXFacingRight;
    public float lastXFacingLeft;

    public float diff;

    public float traveledAfterChange = 0f;
    public bool lastFacingRight;

    public Vector3 lastTargetPosition;

    public Transform minLeft;
    public float minLeftFloat;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(target.position.x + lookAheadFactor, target.position.y + yOffset, -1);
        centeredY = true;
        centeredX = true;

        staticTey = target.position;

        facingRight = true;

        lastXPosition = target.position.x;
        lastXFacingRight = target.position.x;
        lastXFacingLeft = target.position.x;

        minLeftFloat = minLeft.position.x + (GetComponent<Camera>().orthographicSize);
    }

    private void LateUpdate()
    {
        diff = lastXFacingRight - lastXFacingLeft;
        if (!Mathf.Approximately(lastXPosition, target.position.x))
        {
            if (lastXPosition > target.position.x)
            {
                facingRight = false; 
            }
            else
            {
                facingRight = true;
            }
        }

        if (lastFacingRight != facingRight)
        {
            traveledAfterChange = 0;
        }

        float desiredLookAhead = (facingRight) ? lookAheadFactor : -lookAheadFactor;
        float desiredX = (traveledAfterChange > lookAheadThreshold) ? target.position.x + desiredLookAhead : transform.position.x;
        xMove = Mathf.Lerp(transform.position.x, desiredX, xSmooth * Time.deltaTime);

        // Y Move
        if (target.position.y > transform.position.y + yDeadZone || target.position.y < transform.position.y - yDeadZone)
            centeredY = false;

        if (centeredY == true)
        {
            yMove = transform.position.y;
        } else
        {
            yMove = Mathf.Lerp(transform.position.y, target.position.y + yOffset, ySmooth * Time.deltaTime);
        }

        xMove = Mathf.Clamp(xMove, minLeftFloat, xMove);

        transform.position = new Vector3(xMove, yMove, -1);

        if (Mathf.Round(transform.position.y * 100) / 100 == Mathf.Round(target.position.y + yOffset * 100) / 100)
            centeredY = true;

        Vector3 dist = lastTargetPosition - target.position;
        traveledAfterChange += Mathf.Abs(dist.x);

        lastTargetPosition = target.position;
        lastXPosition = target.position.x;

        if (facingRight)
        {
            lastXFacingRight = target.position.x;
        } else
        {
            lastXFacingLeft = target.position.x;
        }

        lastFacingRight = facingRight;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(staticTey.x, transform.position.y), new Vector2(xDeadZone * 2f, yDeadZone * 2f));
    }

}
