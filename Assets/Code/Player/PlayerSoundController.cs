using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioClip thrusterLaunch;
    [SerializeField]
    private AudioClip playerDeath;

    private AudioSource audioSource;

    private void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.audioSource.volume = Config.SfxVolume;
    }

    public void PlayPlayerDeath()
    {
        this.PlayClip(this.playerDeath);
    }

    public void PlayThrusterLaunch()
    {
        this.PlayClip(thrusterLaunch);
    }

    public void PlayClip(AudioClip clip)
    {
        this.StopPlaying();

        this.audioSource.clip = clip;
        this.audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        this.audioSource.volume = volume;
    }

    private void StopPlaying()
    {
        if (this.audioSource.isPlaying)
        {
            this.audioSource.Stop();
        }
    }
}
