using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancePlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 7f;
    public float dashSpeed = 20f;
    public float crouchHight = .5f;
    public LayerMask whatIsGround;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;

    public AudioClip jumpSound;
    public AudioClip dashSound;
    public AudioClip footStepSound;

    private bool grounded;
    private bool canDoubleJump = false;
    private bool isDashing = false;
    private bool isCrouching = false;
    private bool facingRight = true;

    private Rigidbody2D body;
    private Animator anim;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight))
        {
            Flip();
        }
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);


        if (Input.GetKey(KeyCode.Space) && (grounded))
        {
            Jump();
        }
        if (transform.position.y < -5)
        {
            transform.position = new Vector2(0, 0);
        }
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpHeight);
        grounded = false;
        anim.SetTrigger("jump");
    }
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}
