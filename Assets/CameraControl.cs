using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform target;
    public float xOffset;
    public float xSmooth;
    public float ySmooth;

    public float lastTargetXPosition;

    public Transform limitLeft;
    public Transform limitRight;
    public Transform limitBottom;

    public Transform yTopBoundary;
    public Transform yBottomBoundary;
    public float yOffset;

    // Use this for initialization
    void Start () {
        transform.position = target.position;
	}
    private void FixedUpdate()
    {
        /**
        float xMove = Mathf.Lerp(transform.position.x, target.position.x + xOffset, xSmooth);
        // float yMove = Mathf.Lerp(transform.position.x, transform.position.y, .5f);
        
        Vector2 hey = yTopBoundary.TransformPoint(yTopBoundary.position);
        Vector2 heyBottom = yBottomBoundary.TransformPoint(yBottomBoundary.position);

        Vector2 targetPositionTransformed = target.TransformPoint(target.position);

        float yMove = Mathf.Lerp(transform.position.y, target.position.y, .1f);
        if (targetPositionTransformed.y <= hey.y && targetPositionTransformed.y >= heyBottom.y)
        {
            yMove = Mathf.Lerp(transform.position.y, transform.position.y, .1f);
        }**/


        float xMove = target.position.x;
        if (target.GetComponent<HeroControl>().facingRight == true)
        {
            xMove += xOffset;
        } else
        {
            xMove -= xOffset;
        }

        // xMove = Mathf.Lerp(transform.position.x, xMove, xSmooth);
        xMove = Mathf.Clamp(xMove, limitLeft.position.x, limitRight.position.x);
        float tey = 0f;
        xMove = Mathf.SmoothDamp(transform.position.x, xMove, ref tey, .2f);

        float yMove = Mathf.Lerp(transform.position.y, target.position.y, ySmooth);
        yMove = Mathf.Clamp(yMove, limitBottom.position.y, 5000f);

        transform.position = new Vector3(xMove, yMove, -1f);

        lastTargetXPosition = target.position.x;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawWireCube(transform.position, new Vector2(3f, yDeadZone));
    }
}
