using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject LevelsPanel;

    private bool _onMainPanel = true;

    public void LoadLevelEditor()
    {
        SceneManager.LoadSceneAsync( "LevelEditor");
    }


    public void ChooseLevel()
    {
        MainPanel.SetActive(!_onMainPanel);
        LevelsPanel.SetActive(_onMainPanel);

        _onMainPanel = false;
    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!_onMainPanel) ChooseLevel();
        }
    }
}
