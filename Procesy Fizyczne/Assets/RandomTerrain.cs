using UnityEngine;
using System.Collections;

public class RandomTerrain : MonoBehaviour {

    [Header("Tiles Random")]
    public float tiling = 10.0f;
    public Terrain terrain;

    // Use this for initialization
    void Start () {
        GenerateHeights(terrain, tiling);
    }

    public void GenerateHeights(Terrain terrain, float tileSize)
    {
        float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];

        for (int i = 0; i < terrain.terrainData.heightmapWidth; i++)
        {
            for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
            {
                heights[i, k] = Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapWidth) * tileSize, ((float)k / (float)terrain.terrainData.heightmapHeight) * tileSize) / 20.0f;
            }
        }
        terrain.terrainData.SetHeights(0, 0, heights);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
