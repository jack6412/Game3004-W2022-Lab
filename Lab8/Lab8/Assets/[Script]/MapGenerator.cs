using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Player Spawn")]
    public Transform player;
    public Transform spawnPlace;

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
        ResetMap();
        Generater();
        //DisableCollider();
        RemoveTiles();
        PlayerPosition();
    }

    private void PlayerPosition()
    {
        player.gameObject.GetComponent<CharacterController>().enabled = false;
        player.position = new Vector3(Width * .5f, Hight * 2.0f, Depth * .5f);
        spawnPlace.position = player.position;
        player.gameObject.GetComponent<CharacterController>().enabled = true;
    }

    //check for tile and disable the tile not been use
    private void DisableCollider()
    {
        var normalArr = new Vector3[] { Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
        List<GameObject> disableTile = new List<GameObject>();

        foreach (var t in grid)
        {
            int collisionCounter = 0;
            //check tile contect
            for (int i = 0; i < normalArr.Length; i++)
            {
                if (Physics.Raycast(t.transform.position, normalArr[i], t.transform.localScale.magnitude * 03f))
                    collisionCounter++;
            }

            if (collisionCounter > 5)
                disableTile.Add(t);
        }

        foreach (var t in disableTile)
        {
            var boxCollider = t.GetComponent<BoxCollider>();
            var meshRender = t.GetComponent<MeshRenderer>();

            boxCollider.enabled = false;
            meshRender.enabled = false;
        }
    }
    private void RemoveTiles()
    {
        var normalArr = new Vector3[] { Vector3.up, Vector3.down, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };
        List<GameObject> TileRemove = new List<GameObject>();

        foreach (var t in grid)
        {
            int collisionCounter = 0;
            //check tile contect
            for (int i = 0; i < normalArr.Length; i++)
            {
                if (Physics.Raycast(t.transform.position, normalArr[i], t.transform.localScale.magnitude * 03f))
                    collisionCounter++;
            }

            if (collisionCounter > 5)
                TileRemove.Add(t);
        }

        int size = TileRemove.Count;
        for (int i = 0; i < size; i++)
        {
            grid.Remove(TileRemove[i]);
            Destroy(TileRemove[i].gameObject);
        }


    }

    private void Initialize()
    {
        startDepth = Depth;
        startHight = Hight;
        startWidth = Width;
        startMax = max;
        startMin = min;
    }
    private void ResetMap()
    {
        var size = grid.Count;

        for (int i = 0; i < size; i++)
        {
            grid[i].GetComponent<BoxCollider>().enabled = false;
            Destroy(grid[i]);
        }
        

        grid.Clear();
    }
    
}
