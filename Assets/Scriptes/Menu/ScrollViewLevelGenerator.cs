using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;


public class ScrollViewLevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform contentParent;


    void Start()
    {
        var levels = GetLevelsNames();

        foreach (var level in levels)
        {

            GameObject newButton = Instantiate(buttonPrefab, contentParent);

            var n = newButton.GetComponentInChildren<TextMeshProUGUI>();

            n.text = level;
            n.color = Color.white;

            var saveButton = newButton.GetComponent<LevelButton>();

            if (saveButton != null)
            {
                saveButton.LevelName = level;
                newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(saveButton.OnButtonClickHandler);
            }


            n.enableWordWrapping = false;
            n.fontSize = 40;
        }
    }


    private List<string> GetLevelsNames()
    {
        List<string> levels = new List<string>();

        var path = Path.Combine(Application.streamingAssetsPath, "Levels");
        string[] names = Directory.GetFiles(path);

        string runningName = string.Empty;

        foreach (var name in names)
        {
            runningName = GetFileName(name);

            if (name.Contains(".json") && !levels.Contains(runningName))
            {
                levels.Add(runningName);
            }
        }
        return levels;
    }


    private string GetFileName(string file)
    {
        string ans = string.Empty;

        ans = Path.GetFileName(file);

        while (!string.IsNullOrEmpty(Path.GetExtension(ans)))
        {
            ans = Path.GetFileNameWithoutExtension(ans);
        }

        return ans;
    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
