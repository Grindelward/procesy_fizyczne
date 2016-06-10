using UnityEngine;
using System.Collections;

public class RandomTerrain : MonoBehaviour
{

    public Terrain terrain;

    float[,] Tp, T, Tdelay;
    float dx = 1.0f, dy = 1.0f;
    float beta, denom;
    float terrWidth, terrLength;

    // Use this for initialization
    void Start()
    {
        beta = dx / dy;
        denom = 2 * (1.0f + beta * beta);
        float res = terrain.terrainData.heightmapResolution;
        terrWidth = res;
        terrLength = res;

        T = new float[(int)terrWidth, (int)terrLength];
        Tdelay = new float[(int)terrWidth, (int)terrLength];

        for (int i = 1; i < terrWidth - 1; i++)
        {
            for (int j = 1; j < terrLength - 1; j++)
            {
                T[i, j] = 0;
                Tdelay[i, j] = 0;
            }
        }

        terrain.terrainData.SetHeights(0, 0, T);

        Tp = terrain.terrainData.GetHeights(0, 0, (int)terrWidth, (int)terrLength);

    }

    private void GenerateLaplaceHeights()
    {
        int rx = Random.Range(0, (int)terrWidth);
        int rz = Random.Range(0, (int)terrLength);

        if (Tp[rx, rz] > 0.1f)
        {
            Tp[rx, rz] = 0;
            Tdelay[rx, rz] = 100;
        }
        else
        {
            Tp[rx, rz] = 0.9f;
        }

        for (int i = 1; i < terrWidth - 1; i++)
        {
            for (int j = 1; j < terrLength - 1; j++)
            {
                float tmp = Tp[i, j];
                if (Tdelay[i, j] <= 0)
                {
                    tmp = (Tp[i - 1, j] + Tp[i + 1, j] + beta * beta * (Tp[i, j - 1] + Tp[i, j + 1])) / denom;
                }
                T[i, j] = tmp;
            }
        }

        for (int i = 0; i < terrWidth; i++)
        {
            for (int j = 0; j < terrLength; j++)
            {
                if (T[i, j] == 0.5)
                {
                    Tp[i, j] = 0;
                }
                if (Tp[i, j] < 0.89)
                    Tp[i, j] = T[i, j];

                if (Tdelay[i, j] > 0)
                    Tdelay[i, j]--;
            }
        }

        terrain.terrainData.SetHeights(0, 0, T);
    }

    // Update is called once per frame
    void Update()
    {
        GenerateLaplaceHeights();
    }
}