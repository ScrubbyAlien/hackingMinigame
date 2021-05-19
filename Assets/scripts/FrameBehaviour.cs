using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class FrameBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Vector2[] openings;
    float[] openingAngles;

    ContentManager.Thing content;
    Transform contentTransform;

    void Start()
    {
        contentTransform = transform.GetChild(0).transform;
        FindObjectOfType<ClickHandler>().onClickEvent += ReceiveClickEvent;
    }

    public void HandleOnClick()
    {
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
        transform.localEulerAngles = new Vector3(lr.x, lr.y, lr.z - 90);
        for (int i = 0; i < openingAngles.Length; i++)
        {
            openingAngles[i] -= 90 * Mathf.Deg2Rad;
            openingAngles[i] %= Mathf.PI * 2;
        }
        for (int i = 0; i < openings.Length; i++)
        {
            openings[i] = new Vector2(Mathf.Cos(openingAngles[i]), Mathf.Sin(openingAngles[i]));
        }

        foreach (var a in openingAngles)
        {
            Debug.Log(a * Mathf.Rad2Deg);
        }
        foreach (var a in openings)
        {
            Debug.Log(a);
        }
    }

    public void SetContent(string con, float[] angles)
    {
        content = ContentManager.stringToEnum[con];
        openingAngles = angles;
        openings = new Vector2[]
        {
            new Vector2(Mathf.Cos(openingAngles[0]), Mathf.Sin(openingAngles[0])),
            new Vector2(Mathf.Cos(openingAngles[1]), Mathf.Sin(openingAngles[1]))
        };
    }

    public void ReceiveClickEvent(ClickHandler.OnClickEventArgs args)
    {
        //receives the onClick event call from ClickHandler script
        Vector2 pos = args.pos;
        if (transform.GetComponent<BoxCollider2D>().OverlapPoint(pos))
        {
            HandleOnClick();
        }
    }
}
