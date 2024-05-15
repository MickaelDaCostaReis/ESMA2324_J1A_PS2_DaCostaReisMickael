using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float horizontal;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private float wallJumpCd;
    //private Animator animation;
    private bool grounded;

    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private void Awake()
    {
        grounded = true;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        //animation = GetComponent<Animator>();
    }

    void Update()
    {
        // Inputs :
        horizontal = Input.GetAxis("Horizontal");

        //Animations :
        Flip();
        grounded = IsGrounded();
        //GetComponent<Animation>().SetBool("Walk", horizontal != 0);
        //GetComponent<Animation>().SetBool("Grounded", grounded);

        //checks
        JumpConditions();
    }

    private void FixedUpdate()
    {
        //Movement
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        //Jump :
        
        if (Input.GetButton("Jump"))
        {
            Jump();
        }
    }
    // vrai si le joueur touche le sol
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    // vrai si le joueur touche un mur qu'il regarde
    private bool IsonWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    //Inverse le scale du sprite, flip le joueur

    private void Flip()
    {
        if (horizontal > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            grounded = false;
        }
        else if (IsonWall())
        {
            rb.velocity = new Vector2(-transform.localScale.x * 32, 16);
        }
    }

    private void JumpConditions()
    {

        // Divise par deux la velocity si le bouton jump est relâché

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            Debug.Log("jump!");
        }
    }
}
