using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.Mathematics;

public class Generator2D : MonoBehaviour
{
    public int width = 100;
    public int length = 100;

    private float[,] grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new float[width,length];
        GenerateNoise();
    }

    // Update is called once per frame
    void GenerateNoise()
    {
        for(int z = 0; z<length; z++)
        {
            for(int x = 0; x<width; x++)
            {
                grid[x,z] = noise.snoise(new float2(x,z));
                print(grid[x,z]);
            }
        }
    }
}
