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




    // Variables
    private float moveX = 0f;
    [SerializeField] private float moveStrengthX = 7.0f; 
    [SerializeField] private float jumpStrength = 14.0f; 
    [SerializeField] private LayerMask jumpableGround; // sets the layer to check for
        

    // States
    private enum MovementState { idle, running, jumping, falling} // limits what values something can be set to


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
        //        float moveX = Input.GetAxis("Horizontal"); // DONT USE RAW = ICE

        moveX = Input.GetAxisRaw("Horizontal"); // takes input from user
        rb.velocity = new Vector2(moveX * moveStrengthX, rb.velocity.y); 

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
        

        // updates aniamtion state
        UpdateAnimUpdate();
    }


    private void UpdateAnimUpdate() 
    {

     MovementState state;


        if (moveX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        } 
        else if (moveX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f) // 0.1f to account for inaccuracies  (just cant use 0)
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
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);   // creates a box around the player trhe same as the player collider  (first 3 variables) then it moves it down just a bit (so you can detect before hitting the ground)  (allows overlap) 

    }
 
}
