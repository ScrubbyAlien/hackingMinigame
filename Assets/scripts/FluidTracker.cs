using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidTracker : MonoBehaviour
{
    Transform[,] grid;
    Vector2Int gridSize;

    void Start()
    {
        grid = GridSpawner.grid;
        gridSize = GridSpawner.gridSize;
        FindObjectOfType<ClickHandler>().onKeyDownEvent += HandleKeyDown;
    }

    public void HandleKeyDown(ClickHandler.OnKeyDownEventArgs args)
    {
        onKeySpaceDown();
    }

    IEnumerator PourFluid(int x, int y)
    {
        int xInd = x;
        int yInd = y;
        FrameBehaviour curFrame = grid[x, y].GetComponent<FrameBehaviour>();
        Vector2Int nextFrameDir = curFrame.openings[0];
        curFrame.SetContentFull();

        while (true)
        {
            if (!checkDir(xInd, yInd, nextFrameDir))
            {
                nextFrameDir = curFrame.openings[1];
            }
            if (!checkDir(xInd, yInd, nextFrameDir))
            {
                break;
            }
            xInd = x + nextFrameDir.x;
            yInd = y + nextFrameDir.y;

            curFrame = grid[xInd, yInd].GetComponent<FrameBehaviour>();
            nextFrameDir = curFrame.openings[0];
            curFrame.SetContentFull();

            yield return new WaitForEndOfFrame();
        }
    }

    bool checkDir(int x, int y, Vector2Int dir)
    {
        bool ok = false;
        bool notOutOfBounds = (x + dir.x >= 0 && x + dir.x < gridSize.x) &&
                              (y + dir.y >= 0 && y + dir.y < gridSize.y);
        if (notOutOfBounds)
        {
            FrameBehaviour frame = grid[x + dir.x, y + dir.y].GetComponent<FrameBehaviour>();
            bool connected = false;
            foreach (Vector2Int o in frame.openings)
            {
                if (frame.transform.position + new Vector3(o.x, o.y, 0) == grid[x, y].transform.position)
                {
                    connected = true;
                }
            }
            Debug.Log(connected);
            foreach (Vector2Int a in frame.openings)
            {
                Debug.Log(a);
            }
            Debug.Log(dir);
            ok = !frame.full && connected;
        }
        return ok;
    }

    void onKeySpaceDown()
    {
        StartCoroutine(PourFluid(0, 0));
    }
}
