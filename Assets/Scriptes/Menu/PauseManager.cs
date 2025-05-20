using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject menu;
    public bool IsPaused = false;


    public void Pause()
    {
        IsPaused = !IsPaused;

        Time.timeScale = IsPaused ? 0 : 1;

        menu.SetActive(IsPaused);

    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }


    public void ReturnToMenu()
    {
        Pause();
        EventManager.GetInstance().Clear();
        SceneManager.LoadScene("MenuScene");
    }
}
