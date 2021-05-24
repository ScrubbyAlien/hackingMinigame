using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickHandler : MonoBehaviour
{
    public event Action<OnClickEventArgs> onMouseClickEvent;
    public event Action<OnKeyDownEventArgs> onKeyDownEvent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //sends onClickEvent with mouse position arg
            onMouseClickEvent(new OnClickEventArgs
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition)
            });
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //send onKeyDownEvent with keycode arg
            onKeyDownEvent(new OnKeyDownEventArgs
            {
                key = KeyCode.Space
            });
        }
    }

    public class OnClickEventArgs : EventArgs
    {
        public Vector3 pos;
    }

    public class OnKeyDownEventArgs : EventArgs
    {
        public KeyCode key;
    }
}
