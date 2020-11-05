using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise
{
    public static float Perlin(float x)
    {
        return Mathf.PerlinNoise(x, 1.0f); // 1D perlin
    }

    public static float FBM(float x, int octaves, float persistence)
    {
        float total     = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue  = 0;

        for (var i = 0; i < octaves; i++)
        {
            total     += Perlin(x * frequency) * amplitude;
            maxValue  += amplitude;
            amplitude *= persistence;
            frequency *= 2;
        }

        return total / maxValue;
    }
}
