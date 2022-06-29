using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    /// <summary>
    /// Contains sound's name, audio clip reference, and other relevant information.
    /// To serialize a sound, go to SoundList GameObject > Unity Inspector > SoundsList and add sounds. Don't forget to fill in the sound data!
    /// </summary>

    public string soundName;
    public AudioClip audioClip;
    public bool loop = false;
    public float volume = 1f;
}
