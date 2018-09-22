using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimate : MonoBehaviour {

    public Animator animator;
    public Rigidbody2D rb;

    public PlayerMovement playerMovement;
    public CharacterController2D character;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        animator.SetFloat("HorizontalVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
        animator.SetBool("Grounded", character.m_Grounded);
        animator.SetFloat("HorizontalAxis", playerMovement.horizontalAxis);
    }
}
