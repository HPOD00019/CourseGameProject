using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class InformationManager
{
    public static string LevelToLoadName { get; set; }


    public static void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
