using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMoveEnter : MonoBehaviour
{
    public Image mask;
    


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
