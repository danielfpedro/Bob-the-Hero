using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroControl : MonoBehaviour {


    [Header("Horizontal Move")]
    public float horizontalMoveForce = 2f;
    public float maxHorizontalVelocity = 5f;

    [Header("Jump")]
    public float jumpForce = 2f;
    [Range(0.1f, 1f)]
    public float airMoveMultiplier = 1f;
    
    [HideInInspector]
    public bool grounded;
    [Header("Grounded Control")]
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    public float groundCheckOffset;

    [HideInInspector]
    public bool facingRight;
    [HideInInspector]
    public bool alive;

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Rigidbody2D rb;

    public float velocity;
    public float horizontal;

    // Use this for initialization
    void Start () {
        facingRight = true;
        alive = true;

        animator = animator.GetComponent<Animator>();
        rb = animator.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (alive == false)
        {
            return;
        }

        horizontal = Input.GetAxis("Horizontal");

        grounded = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - groundCheckOffset), groundCheckRadius, whatIsGround);
        animator.SetBool("Grounded", grounded);

        if (facingRight && rb.velocity.x < 0)
        {
            facingRight = false;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        } else if (!facingRight && rb.velocity.x > 0) {
            facingRight = true;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
            // animator.SetTrigger("ChargeJump");
        }

        animator.SetFloat("HorizontalAxis", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        if (alive == false)
        {
            return;
        }

        velocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("VelocityY", rb.velocity.y);

        /**
        float desiredMove = horizontalMoveForce;
        if (!grounded)
        {
            desiredMove *= airMoveMultiplier;
        }
        desiredMove = Mathf.Clamp(velocity + desiredMove, 0f, maxHorizontalVelocity);
        
        rb.velocity = new Vector2(desiredMove * horizontal, rb.velocity.y);**/

        float desiredHorizontal = 0f;
        if (horizontal > 0f)
        {
            desiredHorizontal = 1f;
        } else if (horizontal < 0f) {
            desiredHorizontal = -1f;
        }

        float xVelocity = desiredHorizontal * horizontalMoveForce;

        /**
        if (horizontal == 0f)
        {
            if (rb.velocity.x > 0f)
            {
                xVelocity = Mathf.Clamp(rb.velocity.x - 3f, 0f, 1000f);
            } else
            {
                xVelocity = Mathf.Clamp(rb.velocity.x + 3f, -1000, 0f);
            }
        }**/
        // xVelocity = Mathf.Clamp(xVelocity, -maxHorizontalVelocity, maxHorizontalVelocity);

        rb.velocity = new Vector2(xVelocity, rb.velocity.y);

        animator.SetFloat("VelocityY", rb.velocity.y);
        animator.SetFloat("VelocityX", velocity);
    }

    public void ChargeJump()
    {

    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y - groundCheckOffset), groundCheckRadius);
    }

    public void Kill()
    {
        Camera camera = Camera.main;

        // camera.gameObject.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().enabled = false;

        alive = false;

        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = new Vector2(0f, 12f);
        rb.AddTorque(60f);
        Destroy(GetComponent<BoxCollider2D>());

        Invoke("RestartLevel", 3);

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
