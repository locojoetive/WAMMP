using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostmanStateHandler : MonoBehaviour {
    public Transform groundCheck,
        wallCheck;
    private new CapsuleCollider2D collider;
    public Collider2D groundedOn,
        leaningOn,
        jumpOn,
        missionAim,
        turnOn;
    public bool facingRight = true;
    private int whatIsGround = 1 << 0,
        whatIsJumpable = 1 << 9,
        whatIsAim = 1 << 10,
        whatIsTurn = 1 << 11;
    public float boxSize = 1.0f;

    void Start () {
        collider = GetComponent<CapsuleCollider2D>();
	}
	
	void Update () {
        positionSensors();
        HandleState();
	}

    private void HandleState()
    {
        facingRight = transform.localScale.x > 0.0f;
        Vector2 groundCheckSize = new Vector2(0.5f * collider.size.x, boxSize),
            wallCheckSize = new Vector2(boxSize, 0.5f * collider.size.y);
        groundedOn = Physics2D.OverlapBox(
            groundCheck.position,
            groundCheckSize,
            0.0f,
            whatIsGround
        );
        jumpOn = Physics2D.OverlapBox(
            groundCheck.position,
            groundCheckSize,
            0.0f,
            whatIsJumpable
        );
        leaningOn = Physics2D.OverlapBox(
            wallCheck.position,
            wallCheckSize,
            0.0f,
            whatIsGround
        );
        turnOn = Physics2D.OverlapBox(
            wallCheck.position,
            wallCheckSize,
            0.0f,
            whatIsTurn
        );
        turnOn = turnOn? turnOn : Physics2D.OverlapBox(
            wallCheck.position,
            wallCheckSize,
            0.0f,
            whatIsTurn
        );
        missionAim = Physics2D.OverlapBox(
            groundCheck.position,
            groundCheckSize,
            0.0f,
            whatIsAim
        );
        missionAim = missionAim?  missionAim : Physics2D.OverlapBox(
            wallCheck.position,
            wallCheckSize,
            0.0f,
            whatIsAim
        );
        DrawBox(wallCheck.position, wallCheckSize, Color.red);
    }

    public void DrawBox(Vector3 center, Vector3 size, Color color)
    {
        Vector3 rightTop = center + size,
            leftTop = center + new Vector3(-size.x, size.y),
            rightBottom = center + new Vector3(size.x, -size.y),
            leftBottom = center + new Vector3(-size.x, -size.y);
        Debug.DrawLine(rightTop, leftTop, color);
        Debug.DrawLine(leftTop, leftBottom, color);
        Debug.DrawLine(leftBottom, rightBottom, color);
        Debug.DrawLine(rightBottom, rightTop, color);
    }

    private void positionSensors()
    {
        groundCheck.localPosition = new Vector2(collider.offset.x, collider.offset.y - 0.5f * collider.size.y);
        wallCheck.localPosition = new Vector2(collider.offset.x + 0.5f * collider.size.x, collider.offset.y);
    }

    public bool isGrounded()
    {
        return groundedOn;
    }
    public bool isWalled()
    {
        return leaningOn;
    }
    public bool isFacingRight()
    {
        return facingRight;
    }
    public bool gottaJump()
    {
        return jumpOn;
    }
    public bool gottaTurn()
    {
        return turnOn;
    }
    public bool isMissionComplete()
    {
        return missionAim;
    }
}
