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
    [SerializeField] private GameObject runningParticles;
    [SerializeField] private float runningParticlesStopTime;
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

    [Header("WallJump Settings")]
    [SerializeField] private float wallSlindingSpeed;
    [SerializeField] private Transform wallCheck;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] private float wallJumpingDuration;
    [SerializeField] private Vector2 wallJumpingPower;
    private float wallJumpingDirection;
    private bool wallJumpingPowerUp;

    [Header("Dash Settings")]
    [SerializeField] private float DashSpeed;
    [SerializeField] private float DashTime;
    [SerializeField] private float DashCoolDown;
    [SerializeField] private bool dashPowerUp;
    private bool dashCoolDownOK;
    private bool canDash = true;
    private float gravity;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private Animator animation;

    private void Awake()
    {
        grounded = true;
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        pState = GetComponent<PlayerStateList>();
        animation = GetComponent<Animator>();
        gravity = rb.gravityScale;
        player = ReInput.players.GetPlayer(playerID);
        pState.isWallJumping = false;
    }

    void Update()
    {
        StartDash();
        grounded = IsGrounded();    //Fonctionne mieux ainsi, ne s'actualisait pas correctement
        UpdateJumpVariables();
        GetInputs();
        DashReset();
        if (wallJumpingPowerUp)
        {
            WallSlide();
            WallJump();
        }
        if (pState.isDashing) return; // empêche d'autres inputs d'interrompre le dash
        if (!pState.isWallJumping)
        {
            if(!Walled())
                Move();
            Flip();
            Jump();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (rb.velocity.x != 0 && grounded)
            runningParticles.SetActive(true);
        else
            StartCoroutine(StopRunning());
    }

    private void GetInputs()
    {
        horizontal = player.GetAxis("Horizontal");
        releaseJump = player.GetButtonUp("Jump");
        jump = player.GetButtonDown("Jump");
    }
    // vrai si le joueur touche le sol, faux sinon
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //WALL JUMP

    private bool Walled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    
    private void WallSlide()
    {
        if(Walled()&& !grounded && horizontal != 0)
        {
            pState.isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.x, -wallSlindingSpeed, float.MaxValue));
        }
        else
        {
            pState.isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (pState.isWallSliding)
        {
            pState.isWallJumping = false;
            wallJumpingDirection = !pState.isLookingRight ? 1 : -1;
            CancelInvoke(nameof(StopWallJumping));
        }
        if(player.GetButtonDown("Jump")&& pState.isWallSliding)
        {
            pState.isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            canDash = true;
            airJumpsCounter = 0;
            //Tourne le character sans movement input
            if (!pState.isLookingRight)
            {
                runningParticles.transform.localScale = new Vector3(0.5F, 0.5f, 0.5f);   //Particles
                transform.localScale = Vector3.one;                                      //Character
                pState.isLookingRight = true;
            }
            else if (pState.isLookingRight)
            {
                runningParticles.transform.localScale = new Vector3(-0.5f, 0.5F, 0.5f);
                transform.localScale = new Vector3(-1, 1, 1);
                pState.isLookingRight = false;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        pState.isWallJumping = false;
    }

    //Inverse le scale du sprite, flip le joueur et ses particules 
    private void Flip()
    {
        if (horizontal > 0.01f && !pState.isLookingRight)
        {
            runningParticles.transform.localScale =new Vector3(0.5f, 0.5f, 0.5f);    //Particles
            transform.localScale = Vector3.one;                                      //Character
            pState.isLookingRight = true;
        }        
        else if (horizontal < -0.01f && pState.isLookingRight)
        {
            runningParticles.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            transform.localScale = new Vector3(-1, 1, 1);
            pState.isLookingRight = false;
        }
    }

    //JUMP

    private void Jump()
    {
        //stop la montée au relachement de la touche de saut
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
        if(dashPowerUp && player.GetButtonDown("Dash") && canDash && !pState.isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        dashCoolDownOK = false;
        pState.isDashing = true;
        //animation.SetTrigger("Dashing");
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * DashSpeed, 0);
        yield return new WaitForSeconds(DashTime);
        rb.gravityScale = gravity;
        pState.isDashing = false;
        yield return new WaitForSeconds(DashCoolDown);
        dashCoolDownOK = true;
    }
    private void DashReset()
    {
        if (dashCoolDownOK && grounded)
            canDash = true;
    }

    //Particules :

    IEnumerator StopRunning()
    {
        yield return new WaitForSeconds(runningParticlesStopTime);
        runningParticles.SetActive(false);
    }
}
