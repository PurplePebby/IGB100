using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    //from https://learn.unity.com/tutorial/architecture-and-polish?projectId=5c514a00edbc2a0020694718#5c7f8528edbc2a002053b6ee
    //and from https://www.daggerhartlab.com/unity-audio-and-sound-manager-singleton-script/

    public AudioClip underWaterSounds; //use music template in comments below
    public AudioClip aboveWaterSounds;

    public AudioClip bubbles;
    [Tooltip("When player enters water")]
    public AudioClip splash;
    [Tooltip("When player leaves water")]
    public AudioClip drip;
    public AudioClip choke;
    public AudioClip playerDeath;
    public AudioClip playerHurt;
    public AudioClip playerAttack;

    public AudioClip treasureCollect; //added
    public AudioClip treasureDropOffs; //added

    public AudioClip oxygenAlert; //added
    public AudioClip oxygenRefill; //added

    //feel free to probably skip theses
    public AudioClip buttonHover;
    public AudioClip buttonPress;

    public AudioClip enemyHurt;
    public AudioClip enemyDies;

    public AudioClip cannonFire; //added
    public AudioClip cannonHit; //added
    [Tooltip("When player sinks pirate ship")]
    public AudioClip Explosion; //added
    [Tooltip("When pirates spawnned && when treasure has been stolen")]
    public AudioClip pirates; //added


    [SerializeField]
    [Tooltip("Don't assign anything here. Variable for comparing if an audio has changed")]
    private AudioClip oldClip;

    //Drag a reference to the audio source which will play the sound effects.
    public AudioSource efxSource;
    //Drag a reference to the audio source which will play the music.
    public AudioSource musicSource;
    //Allows other scripts to call functions from the SoundManager
    public static SoundManager instance = null;



    /// <summary>
    /// THING TO PASTE FOR SOUNDS EFFECTS
    /// </summary>
    ///SOUND EFFECT
    ///
    //sound for....
    //SoundManager.instance.PlaySingle(SoundManager.instance.NAME_OF_FIELD);
    ///
    ///SOUND EFFECT
    ///

    /// <summary>
    /// THING TO PASTE FOR MUSIC
    /// </summary>
    ///MUSIC
    ///
    //sound for....
    //SoundManager.instance.PlayMusic(SoundManager.instance.NAME_OF_FIELD);
    ///
    ///MUSIC
    void Awake() {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }


	//Used to play single sound clips.
	public void PlaySingle(AudioClip clip) {
        if (clip == null) {
            return;
        }
        else {
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource.clip = clip;
			efxSource.loop = false;
			//Play the clip.
			efxSource.Play();
        }
    }

    //Used to play music. If the same clip is already playing when it's called, or if there is no music, nothing should break
    //but if there's new music it will start playing that
    public void PlayMusic(AudioClip clip) {
        //don't play thing again
        if (clip== null) {
            return;
        }
        else if (oldClip == clip) {
            return;
        }
        else{
            //if the clip sound is different
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            musicSource.clip = clip;
            oldClip = clip;

			//loops the music
			musicSource.loop = true;

			//Play the clip.
			musicSource.Play(); 
        }
        

    }

}


