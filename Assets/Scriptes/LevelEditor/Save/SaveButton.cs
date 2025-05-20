using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SaveButton : MonoBehaviour
{
    private UnityEngine.UI.Button _button;
    private Saver _saver;
    public TMP_InputField InputField;

    void Start()
    {
        _button = GetComponent<UnityEngine.UI.Button>();
        _button.onClick.AddListener(ButtonClickedHandler);
        _saver = FindAnyObjectByType<Saver>();
    }

    void Update()
    {
        
    }


    public void ButtonClickedHandler()
    {
        string Levelname = InputField.text;
        _saver.SaveToMemory(Levelname);
    }
}
