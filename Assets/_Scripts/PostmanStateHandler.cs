using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostmanStateHandler : MonoBehaviour {
    public Transform groundCheckFront,
        groundCheckBack,
        wallCheckDown,
        wallCheckUp,
        wedgeCheck;
    private new CapsuleCollider2D collider;
    public Collider2D groundedFront,
        groundedBack,
        walledUp,
        letsSee,
        walledDown,
        wedged,
        jumpOn,
        jumpFrom,
        missionAim;
    public bool facingRight = true,
        dying = false,
        grounded = false,
        walled = false,
        wedgeJump = false;
    private int whatIsGround = 1 << 0,
        whatIsJumpable = 1 << 9,
        whatIsAim = 1 << 10,
        whatIsDeath = 1 << 12;
    public float boxSize = 1.0f;

    void Start () {
        collider = GetComponent<CapsuleCollider2D>();
	}
	
	void FixedUpdate () {
        positionSensors();
        HandleState();
	}

    private void HandleState()
    {
        Vector2 groundCheckSize = new Vector2(0.125f * collider.size.x, boxSize),
            wallCheckSize = new Vector2(boxSize, 0.25f * collider.size.y),
            letsSeeSize = new Vector2(0.5f * collider.size.x, 0.25f * collider.size.y),
            wedgeCheckSize = new Vector2(boxSize,boxSize);

        dying = Physics2D.OverlapBox(
            groundCheckFront.position,
            groundCheckSize,
            0.0f,
            whatIsDeath
        );
        dying = dying || Physics2D.OverlapBox(
             groundCheckBack.position,
             groundCheckSize,
             0.0f,
             whatIsDeath
        );
        groundedFront = Physics2D.OverlapBox(
            groundCheckFront.position,
            groundCheckSize,
            0.0f,
            whatIsGround
        );
        groundedBack = Physics2D.OverlapBox(
             groundCheckBack.position,
             groundCheckSize,
             0.0f,
             whatIsGround
         );
        jumpOn = Physics2D.OverlapBox(
            groundCheckFront.position,
            groundCheckSize,
            0.0f,
            whatIsJumpable
        );
        jumpFrom = Physics2D.OverlapBox(
            wallCheckDown.position,
            wallCheckSize,
            0.0f,
            whatIsJumpable
        );
        walledUp = Physics2D.OverlapBox(
            wallCheckUp.position,
            wallCheckSize,
            0.0f,
            whatIsGround
        );
        letsSee = Physics2D.OverlapBox(
            wallCheckUp.position,
            letsSeeSize,
            0.0f,
            whatIsGround
        );
        walledDown = Physics2D.OverlapBox(
            wallCheckDown.position,
            wallCheckSize,
            0.0f,
            whatIsGround
        );
        wedged = Physics2D.OverlapBox(
            wedgeCheck.position,
            wedgeCheckSize,
            0.0f,
            whatIsGround
        );
        missionAim = Physics2D.OverlapBox(
            groundCheckFront.position,
            groundCheckSize,
            0.0f,
            whatIsAim
        );
        missionAim = missionAim? missionAim : Physics2D.OverlapBox(
             wallCheckDown.position,
             wallCheckSize,
             0.0f,
             whatIsAim
         );
        walled = walledUp && walledDown;
        grounded = groundedFront && groundedBack;
        facingRight = transform.localScale.x > 0.0f;
        DrawBox(groundCheckFront.position, groundCheckSize, Color.red);
        DrawBox(groundCheckBack.position, groundCheckSize, Color.red);
        DrawBox(wallCheckDown.position, wallCheckSize, Color.red);
        DrawBox(wallCheckUp.position, wallCheckSize, Color.red);
        DrawBox(wedgeCheck.position, wedgeCheckSize, Color.red);
    }

    private void DrawBox(Vector3 center, Vector3 size, Color color)
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
        groundCheckFront.localPosition = new Vector2(collider.offset.x + 0.125f * collider.size.x, collider.offset.y - 0.5f * collider.size.y);
        groundCheckBack.localPosition = new Vector2(collider.offset.x - 0.125f * collider.size.x, collider.offset.y - 0.5f * collider.size.y);
        wallCheckUp.localPosition = new Vector2(collider.offset.x + 0.5f * collider.size.x, collider.offset.y + 0.25f * collider.size.y);
        wallCheckDown.localPosition = new Vector2(collider.offset.x + 0.5f * collider.size.x, collider.offset.y - 0.25f * collider.size.y);
        wallCheckDown.localPosition = new Vector2(collider.offset.x + 0.5f * collider.size.x, collider.offset.y - 0.25f * collider.size.y);
        wedgeCheck.localPosition = new Vector2(collider.offset.x + 0.5f * collider.size.x, collider.offset.y - 0.5f * collider.size.y + 0.5f * boxSize);
    }

    public bool isDying()
    {
        return dying;
    }
    public bool isGrounded()
    {
        return grounded;
    }
    public bool isFacingRight()
    {
        return facingRight;
    }
    public bool isMissionComplete()
    {
        return missionAim;
    }
    public bool isWalled()
    {
        return walled;
    }
    public bool gottaClimb()
    {
        return walledDown && wedged && !walledUp && 
            (grounded || !groundedFront && !groundedBack);
    }
    public bool gottaCrawl()
    {
        return wedged && 
            (!walledUp && !groundedBack);
    }
    public bool gottaJump()
    {
        return jumpOn;
    }
    public bool gottaWallJump()
    {
        return jumpFrom;
    }
    public bool gottaWedgeJump()
    {
        return false;
    }
    public bool gottaTurn()
    {
        return walledDown && walledUp && !jumpFrom;
    }
    public float getSteigung()
    {
        if (walledDown)
        {
            Debug.Log(walledDown.gameObject.transform.eulerAngles);
            return 1.0f - (walledDown.gameObject.transform.eulerAngles.z / 90F);
        } else
        {
            return 0.0f;
        }
    }
}
