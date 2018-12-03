using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteSwapper), typeof(AudioSource))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private int checkpointIndex = 0;

    private GameController gameController;
    private AudioSource audioSource;
    private SpriteSwapper swapper;

    private void Start()
    {
        this.gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        this.gameController.OnCheckpointChanged += this.LevelCheckpointChanged;

        this.audioSource = this.GetComponent<AudioSource>();
        this.swapper = this.GetComponent<SpriteSwapper>();

        this.GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnDestroy()
    {
        this.gameController.OnCheckpointChanged -= this.LevelCheckpointChanged;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && this.gameController.CurrentPlayer.Equals(collision.transform))
        {
            if(!this.gameController.IsCheckpointVisited(this))
            {
                this.gameController.SetCurrentCheckpoint(this);
                this.audioSource.Play();
            }
        }
    }

    private void LevelCheckpointChanged(object sender, CheckpointChangedEventArgs e)
    {
        this.swapper.SwapSprite(!this.gameController.IsCheckpointVisited(this));
    }

    public int CheckpointIndex { get { return this.checkpointIndex; } }
}
