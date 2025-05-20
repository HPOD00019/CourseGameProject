using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    private TMP_InputField _inputField;
    private SaveButton _saveButton;
    
    void Start()
    {
        _inputField = GetComponentInChildren<TMP_InputField>();
        _saveButton = GetComponentInChildren<SaveButton>();
        if (_inputField != null && _saveButton != null) 
        {
            _saveButton.InputField = _inputField;
        }
        
    }

    void Update()
    {
        
    }
}
