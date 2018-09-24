using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostmanMoveScript : MonoBehaviour {
    private Rigidbody2D rb;
    private PostmanStateHandler state;
    public float maxSpeed = 1.0f,
        acceleration = 1.0f,
        jumpHeight = 3.0f;
    private bool rest = false,
        waiting = false;

    void Start () {
        ReferenceComponents();
	}

    void FixedUpdate() {
        HandleMovement();
	}

    private void HandleMovement()
    {
        if (!state.isWaiting()) {
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
        } else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void HandleAxisMovement()
    {
        Vector2 direction = Vector2.zero;
        if (state.gottaTurn() && !state.gottaWallJump())
        {
            TurnAround();
        }
        else if (state.gottaClimb())
        {
            direction = 0.75f * Vector2.up;
            move(direction);
        }
        else if (state.gottaCrawl())
        {
            direction = state.isFacingRight() ?
                Vector2.right :
                -Vector2.right;
            direction += Vector2.up;
            move(direction);
        }
        else {
            direction = state.isFacingRight() ?
                Vector2.right:
                -Vector2.right;
            move(direction);
        }
        
    }

    private void HandleJump()
    {
        Vector2 direction = Vector2.zero;
        if (state.gottaWallJump())
        {
            direction = state.isFacingRight() ?
                - Vector2.right :
                Vector2.right;
            direction = direction + Vector2.up;
            rb.velocity = Vector2.zero;
            TurnAround();
            rb.AddForce(jumpHeight * direction, ForceMode2D.Impulse);
            Debug.Log("Lets jump the shit outta this wall");
        } else if (state.gottaJump())
        {
            rb.AddForce(jumpHeight * Vector2.up, ForceMode2D.Impulse);
            Debug.Log("Lets jump the shit outta this block");
        }
    }

    public void TurnAround()
    {
        transform.localScale = new Vector3(
            -transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z);
        state.toggleFacing();
    }

    private void move (Vector2 direction)
    {
        if(rb.velocity.magnitude < maxSpeed)
            rb.AddForce(acceleration * direction, ForceMode2D.Force);
    }

    private void ReferenceComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        state = GetComponent<PostmanStateHandler>();
    }
}
