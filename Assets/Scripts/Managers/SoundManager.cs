using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource aSource; //for music coz it should play from the camera
    public AudioClip backgroundMusic;
    public AudioClip locationBasedBackgroundMusic;


    public float soundDestroyDelay = 0.2f; // delay for destroying individual sounds


    public void ChangeMusic(AudioClip a)
    {

        backgroundMusic = a;

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
                Object.Destroy(soundObject, sound.audioClip.length + DataStorage.GameManagerComponent.SoundComponent.soundDestroyDelay); // destroys once the sound is finished + delay
            }
        }
    }

    private static Sound GetSound(string soundName) // retrieves sound from SoundsList
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