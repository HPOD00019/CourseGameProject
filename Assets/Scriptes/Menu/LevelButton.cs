using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public string LevelName;
    public void OnButtonClickHandler()
    {
        if(LevelName != null)
        {
            InformationManager.LevelToLoadName = LevelName;
        }
        SceneManager.LoadScene("SampleScene");
    }
}
