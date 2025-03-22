using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Light2D globalLight;
    [SerializeField]
    private Light2D spotLight;

    [SerializeField,Tooltip("玩家移动速度")] private float playerSpeed;
    [SerializeField, Tooltip("时间步")] private float timeStep;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float openTime;
    [SerializeField]private Transform player;
    private bool flag = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("Open",openTime);
    }

    private void OnEnable()
    {
        MessageCenter.AddListener(Win,MESSAGE_TYPE.WIN);
        MessageCenter.AddListener(Lose,MESSAGE_TYPE.LOSE);
    }

    private void OnDisable()
    {
        MessageCenter.RemoveListener(Win,MESSAGE_TYPE.WIN);
        MessageCenter.RemoveListener(Lose,MESSAGE_TYPE.LOSE);
    }
    

    public void Lose(CommonMessage message)
    {
        flag = true;
        StartCoroutine(LoseVFX1());
        StartCoroutine(LoseVFX2());
    }

    public void Win(CommonMessage message)
    {
        StartCoroutine(WinVFX());
        StartCoroutine(PlayerMove());
    }

    IEnumerator WinVFX()
    {
        float timer = 0f;
        while (globalLight.intensity < maxIntensity)
        {
            if(flag) yield break;
            timer += timeStep;
            globalLight.intensity += f(timer);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator LoseVFX1()
    {
        float timer = 0f;
        while (globalLight.intensity > 0)
        {
            timer += timeStep;
            globalLight.intensity -= f2(timer);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator LoseVFX2()
    {
        float timer = 0f;
        while (spotLight.intensity > 0)
        {
            timer += timeStep;
            spotLight.intensity -= f2(timer);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator PlayerMove()
    {
        while (true)
        {
            Vector3 dir = (spotLight.transform.position + Vector3.up * 5.0f) - player.position;
            player.Translate(dir.normalized * (playerSpeed));
            yield return new WaitForSeconds(0.01f);
            if(dir.magnitude < 0.1f) yield break;
        }
    }

    private static float f(float x)
    {
        return Mathf.Exp((3 * x * x - 2 * x * x * x)/100f)/55.0f;
    }
    
    private static float f2(float x)
    {
        return Mathf.Exp((3 * x * x - 2 * x * x * x)/10f)/10f;
    }

    public void Open()
    {
        spotLight.enabled = true;
        globalLight.intensity = 0.04f;
        SoundManager.PlayAudio("Open");
    }
}
