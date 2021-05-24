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

    IEnumerator PourFluid()
    {
        Debug.Log(gridSize.ToString());
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                grid[x, y].GetComponent<FrameBehaviour>().SetContentFull();
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void onKeySpaceDown()
    {
        Debug.Log("Space!");
        StartCoroutine(PourFluid());
    }
}
