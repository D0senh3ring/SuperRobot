using UnityEngine;

[RequireComponent(typeof(PlayerAnimationController))]
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(PlayerSoundController))]
public class PlayerController : MonoBehaviour
{
    private Collider2D boxCollider;

    private void Start()
    {
        this.GameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        this.AnimationController = this.GetComponent<PlayerAnimationController>();
        this.MovementController = this.GetComponent<MovementController>();
        this.SoundController = this.GetComponent<PlayerSoundController>();
        this.InputController = this.GetComponent<InputController>();

        this.RigidBody2D = this.GetComponent<Rigidbody2D>();
        this.boxCollider = this.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if((this.InputController?.IsSacrificeDown).GetValueOrDefault(false) && this.GameController.RemainingRespawns > 0)
        {
            this.Sacrifice(RespawnReason.Sacrifice);
        }
    }

    public void Sacrifice(RespawnReason reason)
    {
        this.SoundController.PlayPlayerDeath();
        this.gameObject.layer = LayerMask.NameToLayer("Bodies");
        this.GameController?.RespawnPlayer(reason);
    }

    public void DisableMovement()
    {
        this.boxCollider.sharedMaterial = null;
        this.RigidBody2D.freezeRotation = false;

        GameObject.Destroy(this.InputController);
        this.MovementController = null;
        GameObject.Destroy(this.MovementController);
        this.InputController = null;
    }

    public PlayerAnimationController AnimationController { get; private set; }
    public PlayerSoundController SoundController { get; private set; }
    public MovementController MovementController { get; private set; }
    public InputController InputController { get; private set; }
    public GameController GameController { get; private set; }
    public Rigidbody2D RigidBody2D { get; private set; }
}

