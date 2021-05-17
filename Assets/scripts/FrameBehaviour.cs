using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class FrameBehaviour : MonoBehaviour
{

    ContentManager.Thing content;
    Transform contentTransform;

    void Start()
    {
        contentTransform = transform.GetChild(0).transform;
        Debug.Log(content.ToString());
    }

    public void HandleOnClick()
    {
        Debug.Log("click");
        Debug.Log(content.ToString());

        switch (content)
        {
            case ContentManager.Thing.Pipe:
                RotatePipe();
                break;
            case ContentManager.Thing.Bomb:
                //do other thing
                break;
        }
    }

    void RotatePipe()
    {
        Vector3 lr = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(lr.x, lr.y, lr.z + 90);
    }

    public void SetContent(string con)
    {
        content = ContentManager.stringToEnum[con];
    }
}
