using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : Ditch {
    protected virtual int[] triangles
    {
        get
        {
            return new int[]
            {
                3, 2, 0,  2, 1, 0,  7, 6, 4,  6, 5, 4,  0, 1, 4,
                1, 5, 4,  1, 2, 5,  2, 6, 5,  2, 3, 6,  3, 7, 6
            };
        }
    }

    protected virtual Vector3[] generateVertices(float length, float width, float height)
    {
        return new Vector3[]
        {
            new Vector3(-length / 2, 0, -width / 2), new Vector3(-length / 2, -height, -width / 2),
            new Vector3(-length / 2, -height, width / 2), new Vector3(-length / 2, 0, width / 2),
            new Vector3(length / 2, 0, -width / 2), new Vector3(length / 2, -height, -width / 2),
            new Vector3(length / 2, -height, width / 2), new Vector3(length / 2, 0, width / 2)
        };
    }

    protected virtual Vector2[] generateUV()
    {
        return new Vector2[]
        {
            new Vector2(0, 0), new Vector2 (1/3.0f, 0), new Vector2(2/3.0f, 0), new Vector2(1, 0),
            new Vector2(0, 1), new Vector2 (1/3.0f, 1), new Vector2(2/3.0f, 1), new Vector2(1, 1)
        };
    }
}
