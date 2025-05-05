using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IFocusableObject : MonoBehaviour
{
    public bool _isFocused = false;

    public virtual void OnFocus()
    {
        _isFocused = true;
    }


    public virtual void OnUnfocus()
    {
        _isFocused = false;
    }


    public virtual bool IsFocused()
    {
        return _isFocused;
    }
}

