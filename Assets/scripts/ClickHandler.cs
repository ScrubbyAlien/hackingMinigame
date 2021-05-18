using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickHandler : MonoBehaviour
{
    public event Action<OnClickEventArgs> onClickEvent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //sends onClickEvent with mouse position arg
            onClickEvent(new OnClickEventArgs
            {
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition)
            });
        }
    }

    public class OnClickEventArgs : EventArgs
    {
        public Vector3 pos;
    }
}
