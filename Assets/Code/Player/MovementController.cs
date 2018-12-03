using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PlayerController), typeof(CapsuleCollider2D))]
public class MovementController : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayermasks = 0;

    [SerializeField, Range(1, 10)]
    private float moveSpeed = 5.0f;
    [SerializeField, Range(1, 20)]
    private float runSpeed = 12.0f;

    [SerializeField, Range(1, 10)]
    private float jumpForce = 7.5f;
    [SerializeField, Range(1, 5)]
    private float defaultFallMultiplier = 5.0f;
    [SerializeField, Range(1, 5)]
    private float jumpFallMultiplier = 3.5f;

    private CapsuleCollider2D collider2d;
    private PlayerController playerController;

    private void Start()
    {
        this.playerController = this.GetComponent<PlayerController>();
        this.collider2d = this.GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        if (this.playerController.InputController != null)
        {
            this.UpdateIsGrounded();

            Vector2 velocity = this.playerController.RigidBody2D.velocity;

            if (this.playerController.InputController.IsJumpDown)
            {
                if (this.IsGrounded)
                {
                    this.playerController.AnimationController.PlayThrusterLaunch();
                    this.playerController.SoundController.PlayThrusterLaunch();
                    velocity.y = this.jumpForce;
                    this.IsJumping = true;
                }

                this.playerController.InputController.IsJumpDown = false;
                this.IsGrounded = false;
            }

            if (velocity.y < 0.0f)
            {
                velocity += Vector2.up * Physics2D.gravity.y * (this.defaultFallMultiplier - 1) * Time.deltaTime;
            }
            else if (velocity.y > 0.0f && !this.playerController.InputController.IsJumpPressed)
            {
                velocity += Vector2.up * Physics2D.gravity.y * (this.jumpFallMultiplier - 1) * Time.deltaTime;
            }

            float horizontal = this.playerController.InputController.HorizontalMoveAxis;
            velocity.x = horizontal * (this.playerController.InputController.IsRunPressed ? this.runSpeed : this.moveSpeed) * 25 * Time.deltaTime;

            this.playerController.RigidBody2D.velocity = velocity;
        }
    }

    public bool IsGrounded { get; private set; }
    public bool IsJumping { get; private set; }

    private void UpdateIsGrounded()
    {
        if(this.playerController.InputController.IsJumpDown)
        {
            Debug.Log("Jump, grounded ? -> " + this.IsGrounded + ", jumping ? -> " + this.IsJumping);
        }

        Vector2 bottomLeft = new Vector2(-this.collider2d.size.x / 2 + this.transform.position.x + this.collider2d.offset.x + 0.015f,
                                         -this.collider2d.size.y / 2 + this.transform.position.y + this.collider2d.offset.y);
        Vector2 bottomRight = bottomLeft + Vector2.right * (this.collider2d.size.x - 0.03f);

        int rayCount = 4;
        float rayLength = -this.playerController.RigidBody2D.velocity.y * this.playerController.RigidBody2D.gravityScale * Time.fixedDeltaTime + 0.25f;
        float horizontalOffset = (bottomRight.x - bottomLeft.x) / (rayCount - 1);

        bool hit = false;

        for(int i = 0; i < rayCount; i++)
        {
            Vector3 test = new Vector3(bottomLeft.x + horizontalOffset * i, bottomLeft.y);
            Debug.DrawRay(test, Vector3.down * rayLength, Color.red);
            RaycastHit2D result = Physics2D.Raycast(bottomLeft + Vector2.right * horizontalOffset * i, Vector2.down, rayLength, this.groundLayermasks);
            if(result)
            {
                hit = true;
                break;
            }
        }

        if(hit)
        {
            this.IsGrounded = true;
            this.IsJumping = false;
        }
        else
        {
            this.IsGrounded = false;
        }
    }
}
