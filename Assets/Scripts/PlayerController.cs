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
    
    //Animation
    public SpriteRenderer SpriteRenderer;
    public Animator Animator;
    
    //Attack
    public float RaycastLength;
    public LayerMask EnemiesLayer;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckTransform.position, GroundCheckRadius);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(GroundCheckTransform.position,  GroundCheckTransform.position + Vector3.down * RaycastLength);
    }

    public void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        JumpCount = 0;
    }

    public void Update()
    {
        CheckForEnemy();
        CalculateMoveInput();
        HandleWalkAnimation();
        HandleJumpAnimation();
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

    private void HandleWalkAnimation()
    {
        int animationState = 0;
        if (Mathf.Abs(MoveInput.x) > 0)
        {
            animationState = 1; // Walk
        }
        SetAnimatorState(animationState);

        if (MoveInput.x > 0)
        {
            SpriteRenderer.flipX = false;
        }
        else if (MoveInput.x < 0)
        {
            SpriteRenderer.flipX = true;
        }
    }

    private void HandleJumpAnimation()
    {
        if (IsGrounded())
        {
            return;
        }
        
        SetAnimatorState(2);
        float yVelocity = Rigidbody.linearVelocityY;
        int jumpState = 0;
        
        if (Mathf.Abs(yVelocity) <= 1f)
        {
            jumpState = 1;
        }
        else if (yVelocity < -0.1f)
        {
            jumpState = 2;
        }
        
        Animator.SetInteger("JumpState", jumpState);
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

        Animator.SetTrigger("StartJump");
        Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, JumpVelocity);
        JumpCount--;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundLayer);
    }

    private void SetAnimatorState(int state)
    {
        Animator.SetInteger("AnimationState", state);
    }

    private void CheckForEnemy()
    {
        var result = Physics2D.Raycast(GroundCheckTransform.position, Vector2.down, RaycastLength, EnemiesLayer);
        if (result)
        {
            Destroy(result.transform.gameObject);
            Rigidbody.linearVelocity = new Vector2(Rigidbody.linearVelocity.x, JumpVelocity);
        }
    }
}