using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryKill : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<HeroControl>().Kill();
        }
    }
}
