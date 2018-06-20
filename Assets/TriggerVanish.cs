using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVanish : MonoBehaviour {

    public enum TipoOptions{Esconder, Aparecer};

    public TipoOptions tipo;

    public LayerMask validContact;
    public GameObject target;

    private void Start()
    {
        if (tipo == TipoOptions.Aparecer)
        {
            target.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (validContact == (validContact | (1 << collider.gameObject.layer)))
        {

            bool flag = false;
            if (tipo == TipoOptions.Aparecer)
            {
                flag = true;
            }
            target.SetActive(flag);

            // Trigger de uma vida se detroi depois que faz seu papel
            Destroy(gameObject);
        }
    }
}
