using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public AudioClip pickup;
    public AudioClip shoot;
    public AudioClip ambient;

    public AudioSource source_effects;
    public AudioSource source_ambient;

    public float volume_effects;
    public float volume_ambient;

    public static AudioSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public void Start()
    {
        /*
        source_effects = GetComponent<AudioSource>();
        source_ambient = GetComponent<AudioSource>();
        */
        source_ambient.volume = volume_ambient;
    }

    public void AddAudio_Effects(AudioClip audioclip)
    {
        if (audioclip)
        {
            source_effects.clip = audioclip;
            source_effects.Play();
        }
    }

    public void AddAudio_Ambient(AudioClip audioclip)
    {
        source_ambient.loop = true;
        if (audioclip)
        {
            source_ambient.clip = audioclip;
            source_ambient.Play();
        }
    }
}
