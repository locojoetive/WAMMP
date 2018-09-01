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
            HandleHorizontalMovement();
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

    private void HandleHorizontalMovement()
    {
        if (state.isWalled())
        {
            transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.z);
        }
        Vector2 whatIsRight = state.isFacingRight()?
            new Vector2(1.0f, 0.0f) :
            new Vector2(-1.0f, 0.0f);
        if (Mathf.Abs(rb.velocity.x) < maxSpeed) {
            rb.AddForce(acceleration * whatIsRight);
        }
    }

    private void HandleJump()
    {
        if(state.gottaJump())
        {
            rb.AddForce(jumpHeight * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void ReferenceComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        state = GetComponent<PostmanStateHandler>();
    }
}
