using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip underwaterSounds;
    public AudioClip abovewaterSounds;
    public AudioClip bubbles;
    public AudioClip splash;
    public AudioClip drip;
    public AudioClip choke;
    public AudioClip playerDeath;
    public AudioClip playerHurt;
    public AudioClip playerAttack;
    public AudioClip treasureCollect;
    public AudioClip treasureDropOffs;
    public AudioClip oxygenAlert;
    public AudioClip oxygenRefill;
    public AudioClip buttonHover;
    public AudioClip buttonPress;
    public AudioClip enemyHurt;
    public AudioClip enemyDies;


    public void addAudioSource(GameObject gm) {
        AudioSource source = gm.AddComponent<AudioSource>();
    }
    public void playSoundeffect(AudioSource source,AudioClip sound) {
        
        source.clip = sound;
        source.Play();
    }

    public void playAmbientMusic(AudioSource source, AudioClip sound) {
        source.clip = sound;
        source.volume = 0.2f;
        source.loop = true;
        source.Play();
    }
}
