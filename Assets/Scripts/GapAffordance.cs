using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class GapAffordance : MonoBehaviour {
    public float length, width, vertical;
    public Material material;

    //protected Vector3[] vertices;
    //protected Vector2[] uv;
    //protected GameObject createdGap;
    
    //protected Vector3 position;
    //protected Quaternion rotation;

    protected virtual void createMesh()
    {
        MeshFilter filter = gameObject.AddComponent<MeshFilter>() as MeshFilter;
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>() as MeshRenderer;

        

        Mesh mesh = new Mesh();
        mesh.vertices = generateVertices();
        mesh.uv = generateUV();
        mesh.triangles = generateTriangles();
        filter.mesh = mesh;
        renderer.material = material;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    protected abstract Vector3[] generateVertices();

    protected abstract Vector2[] generateUV();

    protected abstract int[] generateTriangles();

    // 2018-06-27
    //protected virtual void Start()
    //{
    //    vertices = createCylinderVertices(length, width, depth);
    //    uv = new Vector2[vertices.Length];
    //    float curveLength = (float)(Math.PI * (3 * (width + depth) / 2 - Math.Sqrt((3 * width + depth) * (3 * depth + width) / 4)));
    //    for (int i = 0; i < 17; ++i)
    //    {
    //        uv[i] = new Vector2(0, i * curveLength / 16);
    //        uv[i + 17] = new Vector2(length, i * i * curveLength / 16);
    //    }

    //    Transform center = GetComponent<Transform>();
    //    createdGap = CreateLake.createLake(vertices, TRIANGLES, material, center.position, false, uv);
    //    position = transform.position;
    //    rotation = transform.rotation;
    //}

    //protected virtual void Update()
    //{
    //    if (position != transform.position || rotation != transform.rotation)
    //    {
    //        Vector3 diffPos = transform.position - position;
    //        Quaternion diffRot = Quaternion.Inverse(rotation) * transform.rotation;
    //        Vector3[] newVert = new Vector3[vertices.Length];
    //        for(int i = 0; i < vertices.Length; ++i)
    //        {
    //            newVert[i] = diffRot * vertices[i];
    //        }
    //        vertices = newVert;
    //        Vector3[] gapVert = createdGap.GetComponent<MeshFilter>().mesh.vertices;
    //        for(int i = 0; i < vertices.Length; ++i)
    //        {
    //            gapVert[i] = vertices[i] + transform.position;
    //        }
    //        position = transform.position;
    //        rotation = transform.rotation;
    //    }
    //}

    //protected virtual Vector3[] generateVertices(float length, float width, float height)
    //{
    //    int direction = curveUp ? 1 : -1;
    //    Vector3[] finalResult = new Vector3[34];
    //    for (int i = 0; i < 17; ++i)
    //    {
    //        finalResult[i] = new Vector3(-length / 2, height * direction * (float)Math.Sin(Math.PI * i / 16.0), width / 2 * -(float)Math.Cos(Math.PI * i / 16.0));
    //    }
    //    for (int i = 17; i < 34; ++i)
    //    {
    //        finalResult[i] = new Vector3(length / 2, height * direction * (float)Math.Sin(Math.PI * (i - 17) / 16.0), width / 2 * -(float)Math.Cos(Math.PI * (i - 17) / 16.0));
    //    }

    //    return finalResult;
    //}

    //protected virtual Vector2[] generateUV()
    //{
    //    Vector2[] uv = new Vector2[34];
    //    for (int i = 0; i < 17; ++i)
    //    {
    //        uv[i] = new Vector2(0, i / 16f);
    //        uv[i + 17] = new Vector2(1, i / 16f);
    //    }
    //    return uv;
    //}
}
