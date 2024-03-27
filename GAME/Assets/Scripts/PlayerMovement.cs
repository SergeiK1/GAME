using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    //Components 

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite; 


    // Variables
    private float moveX = 0f;
    [SerializeField] private float moveStrengthX = 7.0f; 
    [SerializeField] private float jumpStrength = 14.0f; 
        



    void Start()
    {
        //Define Components
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //        float moveX = Input.GetAxis("Horizontal"); // DONT USE RAW = ICE

        moveX = Input.GetAxisRaw("Horizontal"); // takes input from rigid body
        rb.velocity = new Vector2(moveX * moveStrengthX, rb.velocity.y); 

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
        

        // updates aniamtion state
        UpdateAnimUpdate();
    }


    private void UpdateAnimUpdate() {
        if (moveX > 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = false;
        } 
        else if (moveX < 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else 
        {
            anim.SetBool("running", false);
        }
    }


}
