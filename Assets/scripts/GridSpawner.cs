using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public Transform framePrefab;
    public Transform pipeStraight;
    public Transform pipeCorner;
    public Transform pipeStart;

    [HideInInspector]
    public static Transform[,] grid;
    [HideInInspector]
    public static Vector2Int gridSize;
    Vector2 startPos;

    Transform[] pipes;

    class prefabNamePair
    {
        public Transform prefab;
        public string name;
        public float[] angles;

        public prefabNamePair(Transform mPrefab, string mName, float[] mAngles)
        {
            prefab = mPrefab;
            name = mName;
            angles = mAngles;
        }
    }

    List<prefabNamePair> prefabNamePairs;

    void Start()
    {
        //prefabNamePair class created for multi-typed list
        prefabNamePairs = new List<prefabNamePair>()
        {
            new prefabNamePair(pipeStraight, "pipe", new float[]{0.0f * Mathf.Deg2Rad, 180.0f * Mathf.Deg2Rad}),
            new prefabNamePair(pipeCorner, "pipe", new float[]{0.0f * Mathf.Deg2Rad, 90.0f * Mathf.Deg2Rad}),
            new prefabNamePair(pipeStart, "start", new float[]{0.0f * Mathf.Deg2Rad, 0.0f * Mathf.Deg2Rad})
        };

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
                prefabNamePair curPair = prefabNamePairs[Random.Range(0, prefabNamePairs.Count)];

                //iterates over every frame in grid and creates a pipe
                Vector2 pos = grid[x, y].position;
                Transform pipe = Instantiate<Transform>(curPair.prefab, pos, Quaternion.identity);
                pipe.parent = grid[x, y];
                //sets the content field of the FrameBehaviour instance
                pipe.parent.GetComponent<FrameBehaviour>().SetContent(curPair.name, curPair.angles);
            }
        }
    }

    void CreateStartPipe(Transform[,] grid)
    {
        Vector2 startPos = (Vector2)grid[0, 0].transform.localPosition + Vector2.left;

    }
}
