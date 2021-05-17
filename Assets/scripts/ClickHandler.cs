using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public Camera mCamera;

    void Start()
    {
        mCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(mCamera.transform.position, Input.mousePosition);
            if (hitInfo)
            {
                hitInfo.transform.gameObject.GetComponent<FrameBehaviour>().HandleOnClick();
            }
        }
    }
}
