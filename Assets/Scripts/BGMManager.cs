using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BGMManager : SingleTon<BGMManager>
{
    [SerializeField] private AudioClip normalBgmAudioClip;
    [SerializeField] private AudioClip mainTitleBgmAudioClip;
    [SerializeField] private AudioClip timestopBgmAudioClip;
    private AudioSource normalBGMplayer;
    private float normalBGMtimer=0f;
    private float timestopBGMtimer = 3f;
    private AudioSource timeStopBGMplayer;
    private AudioSource mainTitleBGMplayer;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        init();
        PlayMainTitleBGM();
    }

    public void StartGame()
    {
        //main title fade in
        // mainTitleBGMplayer.DOFade(0f, .7f).OnComplete(() =>
        // {
        //     StopMainTitleBGM();
        //     PlayNormalBGM();
        // });
    }

    public void RenturnToMainTitle()
    {
        // normalBGMplayer.DOFade(0f, .7f).OnComplete(() =>
        // {
        //     StopNormalBGM();
        //     PlayMainTitleBGM();
        // });
    }

    private void init()
    {
        //normal
        normalBGMplayer = gameObject.AddComponent<AudioSource>();
        normalBGMplayer.clip = normalBgmAudioClip;
        normalBGMplayer.loop = true;
        normalBGMplayer.playOnAwake = false;
        //ts
        timeStopBGMplayer = gameObject.AddComponent<AudioSource>();
        timeStopBGMplayer.clip = timestopBgmAudioClip;
        timeStopBGMplayer.loop = false;
        timeStopBGMplayer.playOnAwake = false;
        //main title
        mainTitleBGMplayer = gameObject.AddComponent<AudioSource>();
        mainTitleBGMplayer.clip = mainTitleBgmAudioClip;
        mainTitleBGMplayer.playOnAwake = true;
        mainTitleBGMplayer.loop = true;
    }

    private void PlayNormalBGM()
    {
        normalBGMplayer.time = normalBGMtimer;
        normalBGMplayer.volume = 1f;
        normalBGMplayer.Play();
    }

    private void StopNormalBGM()
    {
        normalBGMtimer = normalBGMplayer.time;
        normalBGMplayer.Stop();
    }

    private void PlayTimeStopBGM()
    {
        timeStopBGMplayer.time = timestopBGMtimer;
        timeStopBGMplayer.Play();
    }

    private void StopTimeStopBGM()
    {
        timeStopBGMplayer.Stop();
    }

    private void PlayMainTitleBGM()
    {
        mainTitleBGMplayer.volume = 1f;
        mainTitleBGMplayer.Play();
    }

    private void StopMainTitleBGM()
    {
        mainTitleBGMplayer.Stop();
    }

    public void TimeStopOn()
    {
        StopNormalBGM();
        PlayTimeStopBGM();
    }

    public void TimeStopOff()
    {
        StopTimeStopBGM();
        PlayNormalBGM();
    }
}
