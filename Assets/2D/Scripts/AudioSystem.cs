using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] public AudioClip _pickup1;
    [SerializeField] public AudioClip _pickup2;
    [SerializeField] public AudioClip shoot;
    [SerializeField] public AudioClip ambient;

    [SerializeField] public AudioSource source_effects;
    [SerializeField] public AudioSource source_ambient;

    [SerializeField] public float volume_effects;
    [SerializeField] public float volume_ambient;

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
        source_effects.volume = volume_effects;
        source_ambient.volume = volume_ambient;
    }

    // Update is called once per frame
    void Update()
    {
        source_effects.volume = volume_effects;
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
