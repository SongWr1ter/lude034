using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Light2D globalLight;
    [SerializeField]
    private Light2D spotLight;

    [SerializeField,Tooltip("屏幕变白的速度")] private float flashSpeed;
    [SerializeField,Tooltip("玩家移动速度")] private float playerSpeed;
    [SerializeField] private float maxIntensity;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        //MessageCenter.AddListener(Win,MESSAGE_TYPE.WIN);
    }

    private void OnDisable()
    {
        //MessageCenter.RemoveListener(Win,MESSAGE_TYPE.WIN);
    }

    public void Lose(CommonMessage message)
    {
        
    }

    public void Win(CommonMessage message)
    {
        
        Vector3 dir = spotLight.transform.position - player.position;
        dir.Normalize();
        StartCoroutine(WinVFX(dir));
    }

    IEnumerator WinVFX(Vector3 dir)
    {
        while (globalLight.intensity < maxIntensity)
        {
            player.Translate(dir * (playerSpeed * Time.deltaTime));
            
            globalLight.intensity += flashSpeed;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
