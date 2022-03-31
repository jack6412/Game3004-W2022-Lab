using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("World Properties")]
    [Range(8,64)]
    public int Hight = 8;
    [Range(8, 64)]
    public int Width = 8;
    [Range(8, 64)]
    public int Depth = 8;

    [Header("Scaling Valye")]
    [Range(8, 64)]
    public float min = 16.0f;
    [Range(8, 64)]
    public float max = 24.0f;

    [Header("Tile Properties")]
    public GameObject tileObject;
    public Transform tileParent;

    [Header("Grid")]
    public List<GameObject> grid;

    private int startHight,
                startWidth,
                startDepth;
    private float startMax,
                  startMin;

    // Start is called before the first frame update
    void Start()
    {
        GenerataCenter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hight != startHight || Width != startWidth || Depth != startDepth || min != startMin || max != startMax)
            GenerataCenter();

        if (Input.GetKeyDown(KeyCode.R))
            GenerataCenter();
    }


    private void Generater()
    {
        float randomScale = Random.Range(min, max);
        float offsetX = Random.Range(-1024.0f, 1024.0f);
        float offsetZ = Random.Range(-1024.0f, 1024.0f);

        for (int y = 0; y < Hight; y++)
        {
            for (int z = 0; z < Depth; z++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var perlinVlaue = Mathf.PerlinNoise((x + offsetX) / randomScale, (z + offsetZ) / randomScale) * Depth * 0.5f;

                    //Debug.Log(y+" : "+ perlinVlaue);

                    if (y < perlinVlaue)
                    {
                        var tile = Instantiate(tileObject, new Vector3(x, y, z), Quaternion.identity);
                        tile.transform.SetParent(tileParent);
                        grid.Add(tile);
                    }
                }
            }
        }
    }

    private void GenerataCenter()
    {
        Initialize();
        Reset();
        Generater();
    }
    private void Initialize()
    {
        startDepth = Depth;
        startHight = Hight;
        startWidth = Width;
        startMax = max;
        startMin = min;
    }
    private void Reset()
    {
        foreach(var t in grid)
        {
            Destroy(t);
        }
        grid.Clear();
    }

}
