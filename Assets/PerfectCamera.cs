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

        float desiredLookAhead = (facingRight) ? lookAheadFactor : -lookAheadFactor;
        float desiredX = (diff > lookAheadThreshold) ? target.position.x + desiredLookAhead : transform.position.x;
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

        transform.position = new Vector3(xMove, yMove, -1);

        if (Mathf.Round(transform.position.y * 100) / 100 == Mathf.Round(target.position.y + yOffset * 100) / 100)
            centeredY = true;

        /**if (Mathf.Round(transform.position.x * 100) / 100 == Mathf.Round(target.position.x * 100) / 100)
            centeredX = true;**/

        lastXPosition = target.position.x;
        if (facingRight)
        {
            lastXFacingRight = target.position.x;
        } else
        {
            lastXFacingLeft = target.position.x;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(staticTey.x, transform.position.y), new Vector2(xDeadZone * 2f, yDeadZone * 2f));
    }

}
