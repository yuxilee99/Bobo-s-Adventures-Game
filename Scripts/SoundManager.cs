using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S; // singleton definition

    private AudioSource audio;

    // public AudioSource ambientSound;

    public AudioClip playerHopClip;
    public AudioClip playerDieClip;
    public AudioClip enemyHopClip;
    public AudioClip bonchHitClip;
    public AudioClip coinClip;
    public AudioClip winClip;

    private void Awake() {
        S = this; // singleton assigned
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>(); // fetch the audio source component
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakePlayerHopSound() {
        audio.PlayOneShot(playerHopClip);
    }
    
    public void MakeEnemyHopSound() {
        audio.PlayOneShot(enemyHopClip);
    }

    public void MakePlayerDieSound() {
        audio.PlayOneShot(playerDieClip);
    }

    public void MakeBonchHitSound() {
        audio.PlayOneShot(bonchHitClip);
    }

    public void MakeCoinSound() {
        audio.PlayOneShot(coinClip);
    }

    public void MakeWinSound() {
        audio.PlayOneShot(winClip);
    }

    // public void StopAllSounds() {
    //     // stop the ambient noise
    //     ambientSound.Stop();

    //     // top all child sounds
    //     foreach(Transform child in this.transform) {
    //         Destroy(child.gameObject);
    //     }
    // }

    // public void StartSounds() {
    //     // start the ambient noise
    //     if (!ambientSound.isPlaying) {
    //         ambientSound.Play();
    //     }
    // }
}