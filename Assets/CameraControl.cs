using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform target;

    public float xOffset;
    public float yOffset = 0f;

    public float yDeadZone;

    public float xSmooth;
    public float ySmooth;
    public float horizontalDump;

    public float lastTargetXPosition;

    public Transform limitLeft;
    public Transform limitRight;
    public Transform limitBottom;

    public Transform yTopBoundary;
    public Transform yBottomBoundary;
    
    public bool lastTargetFacing;

    private float dumpCurrentVelocity = 0f;

    public float changeRate = 2f;
    private float nextMove;

    private float lastXCamera;

    public float changeDelay = .3f;

    public float deadzoneUp = 0f;
    public float lastDeadUp = 0;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z - 1f);
        deadzoneUp = transform.position.y;
        lastDeadUp = deadzoneUp;
    }
    private void FixedUpdate()
    {
        float xMove = target.position.x;

        bool targetFacingRight = target.GetComponent<HeroControl>().facingRight;
        if (targetFacingRight == true)
        {
            xMove += xOffset;
        } else
        {
            xMove -= xOffset;
        }

        // xMove = Mathf.Lerp(transform.position.x, xMove, xSmooth);
        float vert = Camera.main.orthographicSize;
        float hori = vert * Screen.width / Screen.height;
        float maxX = limitRight.position.x - hori;
        float minX = limitLeft.position.x + hori;

        xMove = Mathf.SmoothDamp(transform.position.x, xMove, ref dumpCurrentVelocity, horizontalDump);

        float yMove = transform.position.y;
        Vector2 targetTey = Camera.main.WorldToScreenPoint(target.position);
        Vector2 camTey = Camera.main.WorldToScreenPoint(transform.position);
        float camHeight = Camera.main.orthographicSize * 2f;

        if (target.position.y < (transform.position.y - yDeadZone) || targetTey.y > (camTey.y - camHeight + yDeadZone))
        {
            // deadzoneUp = Mathf.Lerp(lastDeadUp, target.position.y + yDeadZone, .1f);
            yMove = Mathf.Lerp(transform.position.y, target.position.y + yOffset, .1f); ;
        }

        nextMove += Time.deltaTime;
        
        if (lastTargetFacing != targetFacingRight)
        {

            nextMove = 0f;
        }

        bool stopped = false;
        if (lastTargetXPosition == target.position.x)
        {
            stopped = true;
        }

        float seconds = nextMove % 60;

        if (seconds <= changeDelay)
        {
            xMove = lastXCamera;

        }

        xMove = Mathf.Clamp(xMove, minX, maxX);
        transform.position = new Vector3(xMove, yMove, -1f);


        lastTargetXPosition = target.position.x;
        lastTargetFacing = targetFacingRight;
        lastXCamera = transform.position.x;

        lastDeadUp = deadzoneUp;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), new Vector2(3f, yDeadZone));
    }
}
