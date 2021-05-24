using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class FrameBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int[] openings;
    float[] openingAngles;

    ContentManager.Thing content;
    Transform contentTransform;

    [HideInInspector]
    public bool full;

    void Start()
    {
        full = false;
        contentTransform = transform.GetChild(1).transform;
        FindObjectOfType<ClickHandler>().onMouseClickEvent += ReceiveClickEvent;
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
            openings[i] = new Vector2Int((int)Mathf.Cos(openingAngles[i]), (int)Mathf.Sin(openingAngles[i]));
        }
    }

    public void SetContent(string con, float[] angles)
    {
        content = ContentManager.stringToEnum[con];
        openingAngles = angles;
        openings = new Vector2Int[]
        {
            new Vector2Int((int)Mathf.Cos(openingAngles[0]), (int)Mathf.Sin(openingAngles[0])),
            new Vector2Int((int)Mathf.Cos(openingAngles[1]), (int)Mathf.Sin(openingAngles[1]))
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

    public void SetContentFull()
    {
        full = true;
        for (int i = 0; i < contentTransform.childCount; i++)
        {
            contentTransform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.cyan;
        };
    }
}
