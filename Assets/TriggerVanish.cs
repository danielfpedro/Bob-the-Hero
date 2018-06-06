﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVanish : MonoBehaviour {

    public LayerMask validContact;
    public GameObject target;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Fora If");
        if (validContact == (validContact | (1 << collider.gameObject.layer)))
        {
            Debug.Log("Dentro If");
            target.SetActive(false);
        }
    }
}
