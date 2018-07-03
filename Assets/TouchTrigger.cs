using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrigger : MonoBehaviour {

    public BoxCollider2D boxCollider;
    public bool oneShot = true;
    public bool isOn = false;
    public bool changed = false;
    private bool lastState = false;
    public bool needsButtonBePushed = false;
    private bool inContact = false;

    private Animator animator;

    public GameObject target;

    public float delay = 0f;

    private bool isDead = false;

    private bool wating = false;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2") && inContact && !isDead)
        {
            if (oneShot)
            {
                isDead = true;
            }
            CancelInvoke();
            Invoke("ChangeState", delay);
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
                if (!isDead)
                {
                    CancelInvoke();
                    Invoke("ChangeState", delay);
                }
                if (oneShot)
                {
                    isDead = true;
                }
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isDead)
        {
            if (!needsButtonBePushed)
            {
                CancelInvoke();
                Invoke("ChangeState", delay);
            }
            else
                inContact = false;
        }
    }

    private void ChangeState()
    {
        isOn = !isOn;
        if (oneShot)
        {
            Destroy(boxCollider);
        }
    }
}
