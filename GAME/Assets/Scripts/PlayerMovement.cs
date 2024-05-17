using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components 

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite; 
    private BoxCollider2D coll;  // for grounded method

    private bool OnIce = false; 


    // Variables
    private float moveX = 0f;
    [SerializeField] private float moveStrengthX = 9.0f; 
    [SerializeField] private float jumpStrength = 15f; 
    private float jumpStrength_add = 0f; 
    private float timer;


    private bool spriteBounced = false;
    [SerializeField] private LayerMask jumpableGround; // sets the layer to check for
        

    // States
    private enum MovementState { idle, running, jumping, falling, crouching, bouncing} // limits what values something can be set to
    private bool sprite_crouching = false;

    [SerializeField] private AudioSource jumpSoundEffect;




    void Start()
    {
        //Define Components
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (OnIce)
        {
            moveX = Input.GetAxis("Horizontal"); // Smoother, sliding movement
        }
        else
        {
            moveX = Input.GetAxisRaw("Horizontal");

        }
       //        float moveX = Input.GetAxis("Horizontal"); // DONT USE RAW = ICE
        // takes input from user 

        // updates aniamtion state
        JumpandCrouchHandling();
        UpdateAnimUpdate();
    }

    private void JumpandCrouchHandling()
    {
        if (IsGrounded())
        {
            spriteBounced = false; 
        }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            sprite_crouching = true;
            timer = 0f; 
        }
        else if (Input.GetButton("Jump") && sprite_crouching)
        {
            timer += Time.deltaTime;
        }
        else if (Input.GetButtonUp("Jump") && IsGrounded()){
            if (timer>1.8f)
            {
                timer = 1.8f;
            }
            jumpSoundEffect.Play();
            jumpStrength_add = timer*16;
            sprite_crouching = false; 
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength+jumpStrength_add);
            timer = 0f;
        }
        if (sprite_crouching)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (IsGrounded() && !sprite_crouching)
        {
            rb.velocity = new Vector2(moveX * moveStrengthX, rb.velocity.y);
        }
        if(!IsGrounded() && (IsTouchingLeft() || IsTouchingRight()) && !spriteBounced)
        {
            spriteBounce();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            OnIce = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            OnIce = false;
        }
    }
    private void spriteBounce()
    {
            spriteBounced = true;
            float bounceForce = 3f;
            rb.velocity = new Vector2(-rb.velocity.x, jumpStrength / 2 + bounceForce);
        }
    
    private void UpdateAnimUpdate() 
    {
        MovementState state;

        if (sprite_crouching)
        {
            state = MovementState.crouching;
        }
        else if (rb.velocity.x > 0f && !sprite_crouching)
        {
            state = MovementState.running;
            sprite.flipX = false;
        } 
        else if (rb.velocity.x < 0f && !sprite_crouching)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }
        if (spriteBounced)
        {
            state = MovementState.bouncing;
        }
        else if (rb.velocity.y > 0.1f) // 0.1f to account for inaccuracies  (just cant use 0)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f) 
        {
            state = MovementState.falling;
        }


        anim.SetInteger("state", (int)state); // convert to int so unity can interpret (setup in unities animator through numbers )
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);   // creates a box around the player the same as the player collider  (first 3 variables) then it moves it down just a bit (so you can detect before hitting the ground)  (allows overlap)  q q 
    }

    private bool IsTouchingLeft()
    {
        Vector2 bigSize = new Vector2(coll.bounds.size.x * 1.1f, coll.bounds.size.y);
        bool touchingLeft = Physics2D.BoxCast(coll.bounds.center, bigSize, 0f, Vector2.left, 0.1f, jumpableGround);
        bool Bounce = touchingLeft && rb.velocity.y > 0f;
        return Bounce; 
    } 

    private bool IsTouchingRight()
    {
        Vector2 bigSize = new Vector2(coll.bounds.size.x * 1.1f, coll.bounds.size.y);
        bool touchingRight =  Physics2D.BoxCast(coll.bounds.center, bigSize, 0f, Vector2.right, 0.1f, jumpableGround);   
        bool Bounce = touchingRight && rb.velocity.y > 0f;
        return Bounce; 
    } 

}







