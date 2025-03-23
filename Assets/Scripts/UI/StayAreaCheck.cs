using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StayAreaCheck : MonoBehaviour
{
    public Image mask;
    public enum Logic
    {
        exit,
        play,
    }
    public Logic logic;
    public float maxValue;
    public float speed;
    public float initSize;
    private float value;
    bool flag = false;
    bool flagIn = false;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out MenuMove move))
        {
            flagIn = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out MenuMove move))
        {
            flagIn = true;
        }
    }

    private void Update()
    {
        if (flagIn)
        {
            if (value < maxValue && flag == false)
            {
                value += speed * Time.deltaTime;
                transform.localScale = new Vector3(initSize + value/maxValue, initSize + value/maxValue, transform.localScale.z);
            }
            else
            {
                flag = true;
                invoke1();
            }
        }
        else
        {
            if (value > 0f)
            {
                value -= 1.5f * speed * Time.deltaTime;
                transform.localScale = new Vector3(initSize + value/maxValue, initSize + value/maxValue, transform.localScale.z);
            }
        }
    }

    public void invoke1()
    {
        if (logic == Logic.exit)
        {
            ExitButton();
        }else if (logic == Logic.play)
        {
            StartButton();
        }
    }


    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartButton()
    {
        mask.color = new Color(0f, 0f, 0f, 1f);
        Invoke("ToPlay", 1f);
        
    }

    public void ToPlay()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
