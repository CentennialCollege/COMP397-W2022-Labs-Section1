using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class UIControls : MonoBehaviour
{
    


    public void OnStartButton_Pressed()
    {
        SceneManager.LoadScene("Main");
    }

    
    
}
