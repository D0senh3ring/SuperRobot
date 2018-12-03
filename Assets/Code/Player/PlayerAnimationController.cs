using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour {

    [SerializeField]
    private ParticleSystem thrusterParticleSystem;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;

    private PlayerController playerController;

    private void Start()
    {
        this.playerController = this.GetComponent<PlayerController>();
    }

    public void PlayThrusterLaunch()
    {
        this.thrusterParticleSystem.Play();
    }

    private void Update()
    {
        if (this.playerController.InputController != null)
        {
            float horizontalInput = this.playerController.InputController.HorizontalMoveAxis;

            this.FlipSpriteIfNecessary(this.playerController.InputController.HorizontalMoveAxis);
            this.SetWalking(horizontalInput != 0.0f);
            this.SetRunning(this.playerController.InputController.IsRunPressed);

            if(this.thrusterParticleSystem.isPlaying && this.playerController.RigidBody2D.velocity.y < 0.0f)
            {
                this.thrusterParticleSystem.Stop();
            }
        }
        else
        {
            this.animator.enabled = false;
        }
    }

    private void SetWalking(bool walking)
    {
        this.animator.SetBool("IsWalking", walking);
    }

    private void SetRunning(bool running)
    {
        this.animator.SetBool("IsRunning", running);
    }

    private void FlipSpriteIfNecessary(float horizontalInput)
    {
        if(horizontalInput > 0.0f && this.spriteRenderer.flipX)
        {
            this.spriteRenderer.flipX = false;
            this.FlipThruster();
        }
        else if(horizontalInput < 0.0f && !this.spriteRenderer.flipX)
        {
            this.spriteRenderer.flipX = true;
            this.FlipThruster();
        }
    }

    private void FlipThruster()
    {
        this.thrusterParticleSystem.transform.localPosition = new Vector3(
                this.thrusterParticleSystem.transform.localPosition.x * -1,
                0,
                0
            );
    }
}
