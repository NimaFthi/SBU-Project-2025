using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float JumpVelocity;
    public Rigidbody2D Rigidbody;

    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundLayer;

    public Vector2 MoveInput;

    public int MaxJumpCount = 2;
    public int JumpCount;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckTransform.position, GroundCheckRadius);
    }

    public void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        JumpCount = 0;
    }

    public void Update()
    {
        CalculateMoveInput();
        HandleJump();
    }

    public void FixedUpdate()
    {
        Move();
    }

    private void CalculateMoveInput()
    {
        var x = Input.GetAxisRaw("Horizontal");
        MoveInput = new Vector2(x, 0f);
    }

    private void Move()
    {
        Rigidbody.linearVelocity = new Vector2(MoveInput.x * Speed, Rigidbody.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (IsGrounded())
        {
            JumpCount = MaxJumpCount;
        }
        
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (JumpCount <= 0) return;

        Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, JumpVelocity);
        JumpCount--;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}