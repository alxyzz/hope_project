using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource aSource; //for music coz it should play from the camera
    public Sound backgroundMusic;
    public AudioClip locationBasedBackgroundMusic;
    public float fadeOutVariable = 0.1f;

    [HideInInspector]
    public GameObject previousBGMObject;

    public float soundDestroyDelay = 0.2f; // delay for destroying individual sounds


    public void ChangeBGMusic(string soundName) // fades out and destroys previous bg music and plays the next one 
    {
        Sound sound = SoundPlayer.GetSound(soundName);
        if (sound != null)
        {
            StartCoroutine(LowerVolumeToZero(sound.volume, fadeOutVariable));
            Destroy(previousBGMObject);
            backgroundMusic = sound;
            SoundPlayer.PlaySound(backgroundMusic.soundName, backgroundMusic.volume);
        }
        else
        {
            Debug.LogWarning("sound in ChangeBGMusic() is null! must be a non-existent sound name. ");
        }
    }

    private void Start()
    {
        SoundPlayer.PlaySound("party_bg_music"); // test
    }

    public IEnumerator LowerVolumeToZero(float volume, float amount) // simple volume lowering coroutine
    {
        volume -= amount;
        if (volume != 0)
        {
            yield return null;
            StartCoroutine(LowerVolumeToZero(volume, amount));
        }
        else
        {
            yield return null;
        }
    }
}
/// <summary>
/// For playing individual sounds. 
/// </summary>
public static class SoundPlayer 
{
    /// <summary>
    /// Plays any sound, as long as its defined within SoundsList. <br></br>
    /// Possible to manipulate volume and panning if one desires.
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="volume"></param>
    /// <param name="steroPan"></param>
    public static void PlaySound(string soundName, float volume = 1, float steroPan = 0)
    {
        if (SoundList.Instance)
        {
            Sound sound = GetSound(soundName);
            if (sound != null)
            {
                GameObject soundObject = new GameObject(soundName); // creates sound gameobject
                AudioSource audioSource = soundObject.AddComponent<AudioSource>();

                // sound settings in the SoundsList vvvv
                audioSource.volume = sound.volume;
                audioSource.loop = sound.loop;

                // special sound settings vvvv
                if (volume != 1)
                {
                    audioSource.volume = sound.volume * volume;
                }
                if (steroPan != 0)
                {
                    audioSource.panStereo = steroPan;
                }
                audioSource.PlayOneShot(sound.audioClip); // playing

                if (!audioSource.loop)
                {
                    Object.Destroy(soundObject, sound.audioClip.length + DataStorage.GameManagerComponent.SoundComponent.soundDestroyDelay); // as long as its not looping, destroys once the sound is finished + delay
                }
                else
                {
                    DataStorage.GameManagerComponent.SoundComponent.previousBGMObject = soundObject;
                }
            }
        }
    }

    public static Sound GetSound(string soundName) // retrieves sound from SoundsList
    {
        foreach (Sound sound in SoundList.Instance.soundsList)
        {
            if (sound.soundName == soundName)
            {
                return sound;
            }
        }
        Debug.LogError($"Sound '{soundName}' doesn't exist in the SoundsList!");
        return null;
    }
}