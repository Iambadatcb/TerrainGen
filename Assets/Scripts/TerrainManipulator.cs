using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using UnityEngine.Experimental.Playables;

public class TerrainManipulator : MonoBehaviour
{
    public float seed;
    public float scale = 100;
    private Terrain terrain;
    private TerrainData terrainData;
    private int resolution;
    private float[,] map;
    private float[,,] textureMap;

    private const int GRASS =0;
    private const int ROCKS = 1;
    private const int SAND = 2;
    void Start()
    {
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;
        resolution = terrainData.heightmapResolution;
        map = new float[resolution,resolution];

        GenerateTerrainData();
        PaintTerrain();
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTerrainData();
            PaintTerrain();
        }
    }

    void GenerateTerrainData()
    
    {
        seed = Random.Range(-10000, 10000);
        for(int z = 0; z<resolution;z++)
        {
            for(int x = 0; x < resolution; x++){
                var px = (x+seed)/scale;
                var pz = (z+seed)/scale;
                map[x,z] = (noise.snoise(new float2(px,pz))+1)/2;
            }
        }

        terrainData.SetHeights(0,0,map);
        terrain.Flush();
    }

    void PaintTerrain()
    {
        var height = terrainData.alphamapHeight;
        var width = terrainData.alphamapWidth;

        textureMap = terrainData.GetAlphamaps(0,0,width,height);

        for(int y = 0; y<height;y++)
        {
            for (int x=0;x<width;x++)
            {
                //clear
                textureMap[x,y,GRASS]= 0;
                textureMap[x,y, ROCKS]= 0;
                textureMap[x,y,SAND]=0;

                if(map[x,y]<0.25f)
                {
                    textureMap[x,y,SAND] = 1;
                }
                else if(map[x,y]<0.45f)
                {
                    textureMap[x,y,GRASS]= 1;

                }
                else
                {
                    textureMap[x,y,ROCKS] = 1;
                }
            }
        }
        terrainData.SetAlphamaps(0,0,textureMap);
        terrain.Flush();
    }
}
