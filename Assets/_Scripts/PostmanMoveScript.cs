using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostmanMoveScript : MonoBehaviour {
    private Rigidbody2D rb;
    private PostmanStateHandler state;
    public float maxSpeed = 1.0f,
        acceleration = 1.0f,
        jumpHeight = 3.0f;
    // Geht Besser
    private bool rest = false;

    void Start () {
        ReferenceComponents();
	}

    void FixedUpdate() {
        HandleMovement();
	}

    private void HandleMovement()
    {
        if (!rest && !state.isMissionComplete())
        {
            HandleJump();
            HandleAxisMovement();
        }
        else if (!rest)
        {
            rb.velocity = Vector2.zero;
            rest = true;
        }
        else
        {
            if (state.isGrounded())
            {
                rb.AddForce(jumpHeight * Vector2.up, ForceMode2D.Impulse);
            }
        }
    }

    private void HandleAxisMovement()
    {
        Vector2 direction = Vector2.zero;
        if (state.gottaTurn())
        {
            TurnAround();
            Debug.Log("Turn");
        }
        else if (state.gottaClimb())
        {
            direction = Vector2.up;
            Debug.Log("CLimbing");
            move(direction);
        }
        else if (state.gottaCrawl())
        {
            direction = state.isFacingRight() ?
                Vector2.right :
                -Vector2.right;
            direction = state.getSteigung() * acceleration * direction;
            Debug.Log("Crawling");
            move(direction);
        }
        else {
            direction = state.isFacingRight() ?
                Vector2.right:
                -Vector2.right;
            Debug.Log("Walking");
            move(direction);
        }
        
    }

    
    private void HandleJump()
    {
        if (state.gottaJump())
        {
            rb.AddForce(jumpHeight * Vector2.up, ForceMode2D.Impulse);
            Debug.Log("Lets jump the shit outta this block");
        }
        else if(state.gottaWedgeJump())
        {
            rb.AddForce(jumpHeight * Vector2.up, ForceMode2D.Impulse);
            Debug.Log("Lets jump the shit outta this wedge");
        }
        else if (state.gottaWallJump())
        {
            Vector2 right = state.isFacingRight()?
                -acceleration * Vector2.right:
                acceleration * Vector2.right;
            rb.velocity = Vector2.zero;
            rb.AddForce(right + jumpHeight * Vector2.up, ForceMode2D.Impulse);
            TurnAround();
        }
    }

    public void TurnAround()
    {
        transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.z);
    }

    private void move (Vector2 direction)
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(acceleration * direction, ForceMode2D.Force);
        }
    }

    private void ReferenceComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        state = GetComponent<PostmanStateHandler>();
    }
}
