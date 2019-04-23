using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ninja : Souls
{
    public Souls MasterNinja;
    public Animator ninjaAnimator;
    public Rigidbody2D ninjaRb;
    public SpriteRenderer ninjaRenderer;
    public static float doubleJumpHeight;
    [SerializeField] float axisH;
    public bool isJumpVelPositive;
    [SerializeField] float jumpingVel;
    

    // Start is called before the first frame update
    void Start()
    {
        doubleJumpHeight = 1.2f;

    }

    // Update is called once per frame
    private void Update()
    {
        GetDistance(Direction);
        GetAngle(Direction);
        Movement();
        Attack();
        Debug.Log(jumpingVel);
    }
    
    public override void Attack()
    {
       if(Input.GetButton("Fire1") && canAttack)
       {
            ninjaAnimator.SetBool("isAttacking", true);
            canAttack = false;
       }
     
       else if(Input.GetButtonDown("Fire1") && canAttack && isJumping)
       {
            canAttack = false;
       }

        if (ninjaAnimator.GetBool("isAttacking"))
        {
            ninjaAnimator.SetBool("isLongJumping", false);
        }
    }

    public override void Movement()
    {
        #region WalkandRun
        axisH = Input.GetAxisRaw("Horizontal");
        Vector2 walkSpeed = new Vector2(axisH * xSpeed, ninjaRb.velocity.y);

        if (!Input.GetButton("Sneak"))
        {
            ninjaRb.velocity = walkSpeed;
            ninjaAnimator.SetBool("isRunning", true);
            ninjaAnimator.SetBool("isWalking", false);
        }

        else
        {
            walkSpeed.x /= 2.4f;
            ninjaRb.velocity = walkSpeed;
            ninjaAnimator.SetBool("isWalking", true);
            ninjaAnimator.SetBool("isRunning", false);
            canAttack = false;
        }

        if(axisH == 0.0f)
        {
            ninjaAnimator.SetBool("isWalking", false);
            ninjaAnimator.SetBool("isRunning", false);
        }

        if (axisH < 0.0f)
        {
            ninjaRenderer.flipX = true;
        }
        
        else if (axisH > 0.0f)
        {
            ninjaRenderer.flipX = false;
        }
        #endregion

        #region Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            ninjaRb.velocity = new Vector2(ninjaRb.velocity.x, ySpeed);
        }
        
        if(!isGrounded && canDoubleJump)
        {
            if(isJumping && Input.GetButtonDown("Jump"))
            {
                canDoubleJump = false;
                ninjaRb.velocity = new Vector2(ninjaRb.velocity.x, ySpeed * doubleJumpHeight);
            }
        }
        #endregion

        #region JumpAnimations
        jumpingVel = ninjaRb.velocity.y;

        if (isGrounded)
        {
            isJumping = false;
            canDoubleJump = true;
            ninjaAnimator.SetBool("isLongLanding", false);
            ninjaAnimator.SetBool("isLongJumping", false);
            ninjaAnimator.SetBool("isShortLanding", false);
            ninjaAnimator.SetBool("isLanded", true);
            canAttack = true;
        }

        else
        {
            ninjaAnimator.SetBool("isLanded", false);
        }

        if(jumpingVel > 1 && axisH < 0 | axisH > 0 | axisH == 0)
        {
            ninjaAnimator.SetBool("isLongJumping", true);
            isJumping = true;
        }

        else if(jumpingVel < -50 && axisH < 0 | axisH > 0)
        {
            ninjaAnimator.SetBool("isLongJumping", false);
            ninjaAnimator.SetBool("isLongLanding", true);
            ninjaAnimator.SetBool("isShortLanding", false);
        }

        else if(jumpingVel < -40 && axisH == 0)
        {
            ninjaAnimator.SetBool("isLongJumping", false);
            ninjaAnimator.SetBool("isLongLanding", false);
            ninjaAnimator.SetBool("isShortLanding", true);
        }
     

        if (ninjaAnimator.GetBool("isLongJumping"))
        {
            ninjaAnimator.SetBool("isLongLanding", false);
            ninjaAnimator.SetBool("isShortLanding", false);
        }
        #endregion

        #region JumpAndAttacking
        if(ninjaAnimator.GetBool("isShortLanding") | ninjaAnimator.GetBool("isLongLanding"))
        {
            canAttack = false;
            ninjaAnimator.SetBool("isAttacking", false);
        }

        
        #endregion
    }

    public void endLandAnimation()
    {
        ninjaAnimator.SetBool("isLanded", false);
    }

    public void EndOfAttack()
    {
        ninjaAnimator.SetBool("isAttacking", false);
    }

    public override void Death()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "FloorCollider")
        {
            isGrounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "FloorCollider")
        {
            
            isJumping = true;
        }
    }
}
