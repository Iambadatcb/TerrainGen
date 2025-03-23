using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.Mathematics;
using System.IO.Compression;

public class Generator2D : MonoBehaviour
{
    [Header("World settings")]
    public int width = 100;
    public int length = 100;
    [Range(0,1)] public float groundLimit = 0.5f;

    [Header("Tiles")]
    public GameObject groundTile;
    public GameObject[] decorationTiles;

    [Header("Generator settings")]
    public float scale = 100;
    public float seed;

    private float[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new float[width,length];

        GenerateNoise();
        BuildWorld();
        Decorate();
    }

    // Update is called once per frame
    void GenerateNoise()
    {
        seed = Random.Range(-1000,1000);

        for(int z = 0; z<length; z++)
        {
            for(int x = 0; x<width; x++)
            {
                var px = (x+seed)/scale;
                var pz = (z+seed)/scale;
                grid[x,z] = noise.snoise((new float2(px,pz))+1) / 2;
            }
        }
    }
    
    void BuildWorld()
    {
        for (int z = 0; z< length; z++)
        {
            for (int x=0; x<width;x++)
            {
                if(grid[x,z]>= groundLimit)
                {
                    Instantiate(groundTile, new Vector3(x,0,z), Quaternion.identity);
                }
            }

        }

    }
    
    void Decorate()
    {
        for (int i =0; i<100; i++)
        {
            var x = Random.Range(0,width);
            var z = Random.Range(0,length);
            var ray = new Ray(new Vector3(x,10,z), Vector3.down);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                var title = decorationTiles[Random.Range(0,decorationTiles.Length)];

                var rotationY = Random.Range(0,360);
                Instantiate(title,hit.point, Quaternion.Euler(0, rotationY, 0));
            }
        }
    }
}
