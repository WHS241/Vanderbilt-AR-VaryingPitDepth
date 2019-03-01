using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ditch : GapAffordance
{
    protected static readonly int[] TRIANGLES =
       {
        16, 15,  0,  15, 14,  0,  14, 13,  0,  13, 12,  0,  12, 11,  0,
        11, 10,  0,  10,  9,  0,   9,  8,  0,   8,  7,  0,   7,  6,  0,
         6,  5,  0,   5,  4,  0,   4,  3,  0,   3,  2,  0,   2,  1,  0,

        17, 18, 19,  17, 19, 20,  17, 20, 21,  17, 21, 22,  17, 22, 23,  17, 23, 24,
        17, 24, 25,  17, 25, 26,  17, 26, 27,  17, 27, 28,  17, 28, 29,  17, 29, 30,
        17, 30, 31,  17, 31, 32,  17, 32, 33,

         0,  1, 18,   1,  2, 19,   2,  3, 20,   3,  4, 21,   4,  5, 22,   5,  6, 23,   6,  7, 24,   7,  8, 25,
         8,  9, 26,   9, 10, 27,  10, 11, 28,  11, 12, 29,  12, 13, 30,  13, 14, 31,  14, 15, 32,  15, 16, 33,
        33, 32, 15,  32, 31, 14,  31, 30, 13,  30, 29, 12,  29, 28, 11,  28, 27, 10,  27, 26,  9,  26, 25,  8,
        25, 24,  7,  24, 23,  6,  23, 22,  5,  22, 21,  4,  21, 20,  3,  20, 19,  2,  19, 18,  1,  18, 17,  0
    };

    public Material hideMat;
    private Vector3[] hideVert;
    private int[] hideTriangles;
    private GameObject hideMesh;

    protected virtual void Start()
    {
        createMesh();

        // add small epsilon to ensure occlusion along sides of gap 
        float epsilon = 0.2f;  // 

        hideVert = new Vector3[]
        {
            new Vector3(-length/2, 0, -width / 2), new Vector3(-length/2, 0, width / 2),
            new Vector3(length/2, 0, width / 2), new Vector3(length/2, 0, -width / 2),
            new Vector3(-length/2 - epsilon, 0, -width / 2 - epsilon), new Vector3(-length/2 - epsilon, 0, width / 2 + epsilon),
            new Vector3(length/2 + epsilon, 0, width / 2 + epsilon), new Vector3(length/2 + epsilon, 0, -width / 2 - epsilon),
            new Vector3(-length/2 - epsilon, -vertical, -width / 2 - epsilon), new Vector3(-length/2 - epsilon,  -vertical, width / 2 + epsilon),
            new Vector3(length/2 + epsilon, -vertical, width / 2  +epsilon), new Vector3(length/2 + epsilon,  -vertical, -width / 2 - epsilon),
        };

        hideTriangles = new int[]
        {
            0, 4, 5,  0, 5, 1,  1, 5, 6,  1, 6, 2,  2, 6, 7,  2, 7, 3,  3, 7, 4,  3, 4, 0,
            8, 5, 4,  5, 8, 9,  9, 6, 5,  6, 9,10, 10, 7, 6,  7,10,11, 11, 4, 7,  4,11, 8
        };

        hideMesh = new GameObject("GapHider");
        hideMesh.transform.parent = gameObject.transform;
        hideMesh.transform.localPosition = Vector3.zero;
        hideMesh.transform.localRotation = Quaternion.identity;
        MeshFilter filter = hideMesh.AddComponent<MeshFilter>() as MeshFilter;
        MeshRenderer renderer = hideMesh.AddComponent<MeshRenderer>() as MeshRenderer;

        Mesh mesh = new Mesh();
        mesh.vertices = hideVert;
        mesh.triangles = hideTriangles;
        filter.mesh = mesh;
        renderer.material = hideMat;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        if(transform.parent != null)
        {
            transform.localPosition = new Vector3(0, 0, width / 2);
            transform.localRotation = Quaternion.identity;
        }
    }

    protected override Vector3[] generateVertices()
    {
        
        Vector3[] finalResult = new Vector3[34];
        for (int i = 0; i < 17; ++i)
        {
            finalResult[i] = new Vector3(-length / 2, vertical * -1 * (float)Math.Sin(Math.PI * i / 16.0), width / 2 * -(float)Math.Cos(Math.PI * i / 16.0));
        }
        for (int i = 17; i < 34; ++i)
        {
            finalResult[i] = new Vector3(length / 2, vertical * -1 * (float)Math.Sin(Math.PI * (i - 17) / 16.0), width / 2 * -(float)Math.Cos(Math.PI * (i - 17) / 16.0));
        }

        return finalResult;
    }

    protected override Vector2[] generateUV()
    {
        Vector2[] uv = new Vector2[34];
        for (int i = 0; i < 17; ++i)
        {
            uv[i] = new Vector2(0, i / 16f);
            uv[i + 17] = new Vector2(1, i / 16f);
        }
        return uv;
    }

    protected override int[] generateTriangles()
    {
        return TRIANGLES;
    }

    // Use this for initialization
    //void Start()
    //{
    //    curveUp = false;
    //    base.Start();

    //    BoxCollider collider = GetComponent<BoxCollider>();
    //    collider.size = new Vector3(length, 0.2f, width);

    //    hideVert = new Vector3[]
    //    {
    //        new Vector3(-length/2, 0, -width / 2), new Vector3(-length/2, 0, width / 2),
    //        new Vector3(length/2, 0, width / 2), new Vector3(length/2, 0, -width / 2),
    //        new Vector3(-length/2 - 0.02f, 0, -width / 2 - 0.02f), new Vector3(-length/2 - 0.02f, 0, width / 2 + 0.02f),
    //        new Vector3(length/2 + 0.02f, 0, width / 2 + 0.02f), new Vector3(length/2 + 0.02f, 0, -width / 2 - 0.02f),
    //        new Vector3(-length/2 - 0.02f, -depth, -width / 2 - 0.02f), new Vector3(-length/2 - 0.02f,  -depth, width / 2 + 0.02f),
    //        new Vector3(length/2 + 0.02f, -depth, width / 2 + 0.02f), new Vector3(length/2 + 0.02f,  -depth, -width / 2 - 0.02f),
    //    };

    //    hideTriangles = new int[]
    //    {
    //        0, 4, 5,  0, 5, 1,  1, 5, 6,  1, 6, 2,  2, 6, 7,  2, 7, 3,  3, 7, 4,  3, 4, 0,
    //        8, 5, 4,  5, 8, 9,  9, 6, 5,  6, 9,10, 10, 7, 6,  7,10,11, 11, 4, 7,  4,11, 8
    //    };

    //    hideMesh = CreateLake.createLake(hideVert, hideTriangles, hideMat, transform.position, false);
    //}

    //protected virtual void Update()
    //{
    //    if (position != transform.position || rotation != transform.rotation)
    //    {
    //        Vector3 diffPos = transform.position - position;
    //        Quaternion diffRot = Quaternion.Inverse(rotation) * transform.rotation;
    //        Vector3[] newVert = new Vector3[hideVert.Length];
    //        for (int i = 0; i < hideVert.Length; ++i)
    //        {
    //            newVert[i] = diffRot * hideVert[i];
    //        }
    //        hideVert = newVert;
    //        Vector3[] hideGapVert = createdGap.GetComponent<MeshFilter>().mesh.vertices;
    //        for (int i = 0; i < vertices.Length; ++i)
    //        {
    //            hideGapVert[i] = vertices[i] + transform.position;
    //        }
    //        base.Update();
    //    }
    //}
}
