using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickerTrigger : MonoBehaviour {

    public LayerMask validContact;
    public float kickForce = 10f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (validContact == (validContact | (1 << collider.gameObject.layer)))
        {
            Debug.Log("JOGAR");
            collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.up * kickForce;
        }
    }
}
