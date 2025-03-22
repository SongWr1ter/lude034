using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BGMManager : SingleTon<BGMManager>
{
    private AudioClip BgmAudioClip;
    [SerializeField] private float waitingTime;
    private AudioSource audioSource;
    [SerializeField]private float volume;
    protected override void Awake()
    {
        base.Awake();
        init();
    }

    protected void init()
    {
        BgmAudioClip = Resources.Load<AudioClip>("BGM");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = BgmAudioClip;
        audioSource.loop = true;
        audioSource.volume = volume;
        Invoke("Play", waitingTime);
    }

    public void Play()
    {
        audioSource.Play();
    }
}
