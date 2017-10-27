using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [Header("StateGround")]
    public bool isGrounded;
    public bool wasGroundedLastFrame;
    public bool justGrounded;
    public bool justNOTGrounded;
    public bool isFalling;

    [Header("StateTop")]
    public bool isTop;
    public bool wasTopLastFrame;
    public bool justTop;
    public bool justNOTTop;
    public bool isOnair;

    [Header("StateWall")]
    public bool isWall;
    public bool wasWallLastFrame;
    public bool justWall;
    public bool justNOTWall;
    public bool isOnWall;

    [Header("GroundBoxes")]
    public Vector2 GroundBoxPos;
    public Vector2 GroundBoxSize;

    [Header("TopBoxes")]
    public Vector2 TopBoxPos;
    public Vector2 TopBoxSize;

    [Header("WallBoxes")]
    public Vector2 WallBoxPos;
    public Vector2 WallBoxSize;

    [Header("GroundProperties")]
    public int maxHits = 1;
    public bool detectGround = true;
    public ContactFilter2D filterGround;

    [Header("TopProperties")]
    public int maxHitsTop = 1;
    public bool detectTop = true;
    public ContactFilter2D filterTop;

    [Header("WallProperties")]
    public int maxHitsWall = 1;
    public bool detectWall = true;
    public ContactFilter2D filterWall;


    private void FixedUpdate()
    {
        ResetStateGround();
        DetectGround();
        ResetStateTop();
        DetectTop();
        ResetStateWall();
        DetectWall();
    }

    void ResetStateGround()
    {
        wasGroundedLastFrame = isGrounded;
        isGrounded = false;
        justNOTGrounded = false;
        justGrounded = false;
        isFalling = true;
    }

    void ResetStateTop()
    {
        wasTopLastFrame = isTop;
        isTop = false;
        justNOTTop = false;
        justTop = false;
        isOnair = true;
    }

    void ResetStateWall()
    {
        wasWallLastFrame = isGrounded;
        isWall = false;
        justNOTWall = false;
        justWall = false;
        isWall = true;
    }

    void DetectGround()
    {
        if(!detectGround) return;

        Collider2D[] results = new Collider2D[maxHits];
        Vector3 newPos = (Vector3)GroundBoxPos + transform.position;
        int numHits = Physics2D.OverlapBox(newPos, GroundBoxSize, 0, filterGround, results);

        if(numHits > 0)
        {
            isGrounded = true;
        }
        isFalling = !isGrounded;

        if(!wasGroundedLastFrame && isGrounded)
        {
            Debug.Log("JUST GROUNDED");
            justGrounded = true;
        }
        if(wasGroundedLastFrame && !isGrounded)
        {
            Debug.Log("JUST NOT GROUNDED");
            justNOTGrounded = true;
        }

    }

    void DetectTop()
    {
        if(!detectTop) return;

        Collider2D[] results = new Collider2D[maxHitsTop];
        Vector3 newPos = (Vector3)TopBoxPos + transform.position;
        int numHits = Physics2D.OverlapBox(newPos, TopBoxSize, 0, filterTop, results);

        if(numHits > 0)
        {
            isTop = true;
        }
        isOnair = !isTop;

        if(!wasGroundedLastFrame && isGrounded)
        {
            Debug.Log("JUST TOP");
            justTop = true;
        }
        if(wasGroundedLastFrame && !isGrounded)
        {
            Debug.Log("JUST NOT TOP");
            justNOTTop = true;
        }
    }

    void DetectWall()
    {
        if(!detectWall) return;

        Collider2D[] results = new Collider2D[maxHitsWall];
        Vector3 newPos = (Vector3)WallBoxPos + transform.position;
        int numHits = Physics2D.OverlapBox(newPos, WallBoxSize, 0, filterWall, results);

        if(numHits > 0)
        {
            isTop = true;
        }
        isOnWall = !isWall;

        if(!wasWallLastFrame && isGrounded)
        {
            Debug.Log("JUST WALL");
            justGrounded = true;
        }
        if(wasWallLastFrame && !isGrounded)
        {
            Debug.Log("JUST NOT WALL");
            justNOTWall = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 newPos = (Vector3)GroundBoxPos + transform.position;
        Gizmos.DrawWireCube(newPos, GroundBoxSize);

        Gizmos.color = Color.red;
        Vector3 newPosTop = (Vector3)TopBoxPos + transform.position;
        Gizmos.DrawWireCube(newPosTop, TopBoxSize);

        Gizmos.color = Color.cyan;
        Vector3 newPosWall = (Vector3)WallBoxPos + transform.position;
        Gizmos.DrawWireCube(newPosWall, WallBoxSize);
    }
}
