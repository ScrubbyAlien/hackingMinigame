using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public Transform framePrefab;
    public Transform pipeStraight;
    public Transform pipeCorner;

    Transform[,] grid;
    Vector2Int gridSize;
    Vector2 startPos;

    void Start()
    {
        startPos = new Vector2(-4, 4);
        gridSize = new Vector2Int(8, 8);
        grid = new Transform[gridSize.x, gridSize.y];
        CreateGrid(startPos);
        CreatePipes(grid);
    }

    void CreateGrid(Vector2 startPos)
    {
        for (int x = (int)startPos.x; x < (int)startPos.x + gridSize.x; x++)
        {
            for (int y = (int)startPos.y; y > (int)startPos.y - gridSize.y; y--)
            {
                //creates a gridSize.x * gridSize.y grid of frames and adds each to grid
                Vector2 pos = new Vector2(x + 0.5f, y - 0.5f);
                Transform newFrame = Instantiate<Transform>(framePrefab, pos, Quaternion.identity);
                newFrame.parent = transform;
                grid[x + 4, -(y - 4)] = newFrame;
            }
        }
    }

    void CreatePipes(Transform[,] grid)
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                //iterates over every frame in grid and creates a pipe (should be random pipe)
                Vector2 pos = grid[x, y].position;
                Transform pipe = Instantiate<Transform>(pipeStraight, pos, Quaternion.identity);
                pipe.parent = grid[x, y];
            }
        }
    }
}
