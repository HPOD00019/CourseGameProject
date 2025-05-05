using UnityEngine;

public class FocusControl 
{
    private static FocusControl _instance;

    private FocusControl()
    {

    }


    public static FocusControl GetInstance()
    {
        if (_instance == null)
        {
            _instance = new FocusControl();
        }
        return _instance;
    }


    private IFocusableObject _focusedObj;

    public IFocusableObject GetFocusedObject()
    {
        return _focusedObj;
    }


    public void CheckForFocus()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] objects = Physics.RaycastAll(ray);

        var hit = objects[0];

        if (hit.collider != null)
        {
            IFocusableObject[] focusableObjects = hit.collider.GetComponents<IFocusableObject>();
            IFocusableObject focusable = focusableObjects.Length > 0 ? focusableObjects[0] : null;

            if (focusable != null)
            {
                IFocusableObject OldObj = _focusedObj;
                _focusedObj = focusable;
                UnfocusObject(OldObj);
                SetFocus(focusable);
            }
            else
            {
                UnfocusCurrentObject();
            }
        }

        else
        {
            UnfocusCurrentObject();
        }
    }


    public void SetFocus(IFocusableObject obj)
    {
        _focusedObj = obj;
        obj?.OnFocus();
    }


    public void UnfocusCurrentObject()
    {
        _focusedObj = null;
        _focusedObj?.OnUnfocus();
    }


    private void UnfocusObject(IFocusableObject obj)
    {
        obj?.OnUnfocus();
    }
}

