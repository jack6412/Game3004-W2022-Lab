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
    public GameObject tile;
    public Transform tileParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
