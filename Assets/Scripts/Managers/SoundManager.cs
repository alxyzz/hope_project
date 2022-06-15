using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource aSource; //for music coz it should play from the camera
    public AudioClip backgroundMusic;
    public AudioClip locationBasedBackgroundMusic;



    public void ChangeMusic(AudioClip a)
    {

        backgroundMusic = a;

    }




}
