using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrigger : MonoBehaviour {

    public BoxCollider2D boxCollider;
    public bool oneShot = true;
    public bool isOn = false;
    public bool needsButtonBePushed = false;
    private bool inContact = false;

    private Animator animator;

    public GameObject target;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2") && inContact)
        {
            isOn = !isOn;
        }
        if (animator != null)
        {
            animator.SetBool("IsOn", isOn);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (needsButtonBePushed)
            {
                inContact = true;
                target = null;
            }
            else
            {
                isOn = true;
                target = collision.gameObject;
                if (oneShot)
                {
                    Destroy(boxCollider);
                }
            }
        } else
        {
            target = null;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!needsButtonBePushed)
            isOn = false;
        else
            inContact = false;
    }
}
