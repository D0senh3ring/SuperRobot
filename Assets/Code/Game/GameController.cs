using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour {

    public event EventHandler<RespawnCountChangedEventArgs> OnRespawnCountChanged;
    public event EventHandler<CheckpointChangedEventArgs> OnCheckpointChanged;
    public event EventHandler<PlayerChangedEventArgs> OnCurrentPlayerChanged;

    [SerializeField]
    private int levelIndex = -1;
    [SerializeField]
    private int respawnCount;

    [SerializeField]
    private Transform playerPrefab = null;
    [SerializeField]
    private Transform playerSpawn = null;

    private Checkpoint currentCheckpoint;
    private List<Transform> playerBodies;

    private void Start()
    {
        foreach(AudioSource audioSource in GameObject.FindObjectsOfType<AudioSource>())
        {
            if(audioSource.CompareTag("Music"))
            {
                audioSource.volume = Config.MusicVolume;
            }
            else
            {
                audioSource.volume = Config.SfxVolume;
            }
        }

        this.CurrentPlayer = GameObject.FindWithTag("Player")?.transform;

        if(this.CurrentPlayer == null)
        {
            this.SpawnPlayer();
        }
        else
        {
            this.RaiseCurrentPlayerChanged();
        }

        this.playerBodies = new List<Transform>();

        this.RaiseRemainingRespawnsChanged();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void RespawnPlayer(RespawnReason reason)
    {
        if (this.RemainingRespawns > 0)
        {
            Config.AddRespawn(reason);
            if (this.CurrentPlayer != null)
            {
                this.playerBodies.Add(this.CurrentPlayer);

                this.CurrentPlayer.GetComponent<PlayerController>().DisableMovement();
                this.RaiseRemainingRespawnsChanged();
            }

            this.SpawnPlayer();
        }
        else if(reason != RespawnReason.Sacrifice)
        {
            Config.AddRespawn(reason);
            this.ReloadCurrentLevel();
        }
    }

    public void LoadNextLevel()
    {
        Config.SetLevelCompleted(this.levelIndex, true);

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextIndex >= SceneManager.sceneCount ? 0 : nextIndex, LoadSceneMode.Single);
    }

    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public Transform CurrentPlayer { get; private set; }

    public int RemainingRespawns { get { return this.respawnCount - this.playerBodies.Count; } }
    public int MaximumRespawns { get { return this.respawnCount; } }

    private void SpawnPlayer()
    {
        Vector3 position = (this.currentCheckpoint?.transform ?? this.playerSpawn).position;

        this.CurrentPlayer = GameObject.Instantiate(this.playerPrefab, position, Quaternion.identity, null);
        this.RaiseCurrentPlayerChanged();
    }

    public void SetCurrentCheckpoint(Checkpoint newCheckpoint)
    {
        if(!this.IsCheckpointVisited(newCheckpoint))
        {
            this.currentCheckpoint = newCheckpoint;
            this.RaiseCheckpointChanged();
        }
    }

    public bool IsCheckpointVisited(Checkpoint checkpoint)
    {
        return checkpoint.CheckpointIndex <= (this.currentCheckpoint?.CheckpointIndex).GetValueOrDefault(-1);
    }

    private void RaiseRemainingRespawnsChanged()
    {
        this.OnRespawnCountChanged?.Invoke(this, new RespawnCountChangedEventArgs(this.respawnCount, this.RemainingRespawns));
    }

    private void RaiseCurrentPlayerChanged()
    {
        this.OnCurrentPlayerChanged?.Invoke(this, new PlayerChangedEventArgs(this.CurrentPlayer));
    }

    private void RaiseCheckpointChanged()
    {
        this.OnCheckpointChanged?.Invoke(this, new CheckpointChangedEventArgs(this.currentCheckpoint));
    }
}

public enum RespawnReason
{
    Sacrifice,
    TouchedHazard,
    FellIntoVoid
}