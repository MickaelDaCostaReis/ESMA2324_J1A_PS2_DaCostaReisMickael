using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerManager : MonoBehaviour
{
    private Player player;
    private int playerID;

    [Header("Horizontal Movement")]
    [SerializeField] private float speed;
    private float horizontal;


    [Header("Jump Settings")]
    [SerializeField] private float coyoteTime;
    [SerializeField] private int jumpBufferFrames;
    [SerializeField] private int maxAirJumps; //nombre de saut supp
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpingPower;
    private bool jump; //btn down
    private bool releaseJump; //btn up
    private int jumpBufferCounter=0; 
    private float coyoteTimeCounter = 0; //Permissibilité de saut après avoir quitté le sol
    private int airJumpsCounter = 0;
    private PlayerStateList pState;
    private bool grounded;

    [Header("Dash Settings")]
    [SerializeField] private float DashSpeed;
    [SerializeField] private float DashTime;
    [SerializeField] private float DashCoolDown;
    private bool canDash = true;
    private bool hasDashed = false;
    private float gravity;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    private void Awake()
    {
        grounded = true;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        pState = GetComponent<PlayerStateList>();
        gravity = rb.gravityScale;
        player = ReInput.players.GetPlayer(playerID);
    }

    void Update()
    {
        StartDash();
        //checks
        grounded = IsGrounded();
        UpdateJumpVariables();
        // Inputs :
        horizontal = player.GetAxis("Horizontal");
        releaseJump = player.GetButtonUp("Jump");
        jump = player.GetButtonDown("Jump");
        if (hasDashed) return;
        //Sprite & Animations :
        Flip();
        //GetComponent<Animator>().SetBool("Walk", horizontal != 0);
        //GetComponent<Animator>().SetBool("Grounded", grounded);

        
        //Jump :
        Jump();
        //Movement
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // vrai si le joueur touche le sol, faux sinon
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
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
        //stop la monté au relachement de la touche de saut
        if (releaseJump && rb.velocity.y>0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            pState.isJumping = false;
        }

        if (!pState.isJumping)
        {
            //Saute
            if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                pState.isJumping = true;
            }
            else if(!grounded && airJumpsCounter < maxAirJumps && jump)
            {
                pState.isJumping = true;
                airJumpsCounter++;
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
        }
        
    }

    private void UpdateJumpVariables()
    {   
        //reset cooldown
        if (IsGrounded())
        {
            pState.isJumping = false;
            coyoteTimeCounter = coyoteTime;
            airJumpsCounter = 0;
        }
        //réduit cooldown
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (jump)
            jumpBufferCounter = jumpBufferFrames;
        else
            jumpBufferCounter--;
    }

    // ELEVATOR :
    //Le joueur passe en parent pour un mouvement plus lisse
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Elevator"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Elevator"))
        {
            transform.SetParent(null);
        }
    }

    // DASH :
    void StartDash()
    {
        if(player.GetButtonDown("Dash") && canDash/* && !hasDashed*/)
        {
            Debug.Log("dashing!");
            StartCoroutine(Dash());
            Debug.Log("dash!");
            hasDashed = true;
        }

        if (grounded)
            hasDashed = false;
    }

    IEnumerator Dash()
    {
        canDash = false;
        pState.isDashing = true;
        //animation.SetTrigger("Dashing");
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * DashSpeed, 0);
        yield return new WaitForSeconds(DashTime);
        rb.gravityScale = gravity;
        pState.isDashing = false;
        yield return new WaitForSeconds(DashCoolDown);
        canDash = true;
    }
}
