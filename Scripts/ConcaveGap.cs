using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcaveGap : GapAffordance {
    public float slantParameter;
    protected static readonly int[] TRIANGLES =
       {
        16, 15, 14,  16, 14, 13,  16, 13, 12,  16, 12, 11,  16, 11, 10,
        16, 10,  9,  16,  9,  8,  16,  8,  0,   8,  7,  0,   7,  6,  0,
         6,  5,  0,   5,  4,  0,   4,  3,  0,   3,  2,  0,   2,  1,  0,

        17, 18, 19,  17, 19, 20,  17, 20, 21,  17, 21, 22,  17, 22, 23,
        17, 23, 24,  17, 24, 25,  17, 25, 33,  25, 26, 33,  26, 27, 33,
        27, 28, 33,  28, 29, 33,  29, 30, 33,  30, 31, 33,  31, 32, 33,

         0,  1, 18,   1,  2, 19,   2,  3, 20,   3,  4, 21,   4,  5, 22,   5,  6, 23,   6,  7, 24,   7,  8, 25,
         8,  9, 26,   9, 10, 27,  10, 11, 28,  11, 12, 29,  12, 13, 30,  13, 14, 31,  14, 15, 32,  15, 16, 33,
        33, 32, 15,  32, 31, 14,  31, 30, 13,  30, 29, 12,  29, 28, 11,  28, 27, 10,  27, 26,  9,  26, 25,  8,
        25, 24,  7,  24, 23,  6,  23, 22,  5,  22, 21,  4,  21, 20,  3,  20, 19,  2,  19, 18,  1,  18, 17,  0
    };

    // Use this for initialization
    void Start()
    {
        createMesh();

        if (transform.parent != null)
        {
            transform.localPosition = new Vector3(0, 0, width / 2);
            transform.localRotation = Quaternion.identity;
        }
    }

    protected override Vector3[] generateVertices()
    {
        Debug.Assert(slantParameter >= 0 && slantParameter <= 1);
        Vector3[] vertexList = new Vector3[34];
        for (int i = 0; i < 17; ++i)
        {
            vertexList[i] = new Vector3(length * 0.5f * (-1 + slantParameter * (float)Math.Sin(Math.PI * i / 16.0)), vertical * (float)Math.Sin(Math.PI * i / 16.0), width / 2 * -(float)Math.Cos(Math.PI * i / 16.0));
        }
        for (int i = 17; i < 34; ++i)
        {
            vertexList[i] = new Vector3(length * 0.5f * (1 - slantParameter * (float)Math.Sin(Math.PI * (i - 17) / 16.0)), vertical * (float)Math.Sin(Math.PI * (i - 17) / 16.0), width / 2 * -(float)Math.Cos(Math.PI * (i - 17) / 16.0));
        }
        return vertexList;
    }

    protected override Vector2[] generateUV()
    {
        Vector2[] uv = new Vector2[34];
        for (int i = 0; i < 17; ++i)
        {
            uv[i] = new Vector2((float)Math.Sin(Math.PI * i / 16.0), i / 16f);
            uv[i + 17] = new Vector2(1 - (float)Math.Sin(Math.PI * i / 16.0), i / 16f);
        }
        return uv;
    }

    protected override int[] generateTriangles()
    {
        return TRIANGLES;
    }
}
