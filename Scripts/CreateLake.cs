using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CreateLake
{
    public static GameObject createLake(Vector3[] vertices, int[] triangles, Material mat, Vector3 center, bool collider = true, Vector2[] uv = null)
    {
        GameObject lakeSurface = new GameObject("Plane");
        MeshFilter mf = lakeSurface.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = lakeSurface.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        Mesh m = new Mesh();
        m.vertices = shift(vertices, center);
        m.triangles = triangles;
        if(uv != null)
        {
            m.uv = uv;
        }

        if(collider)
        {
            (lakeSurface.AddComponent(typeof(MeshCollider)) as MeshCollider).sharedMesh = m;
        }

        mr.material = mat;
        mf.mesh = m;
        m.RecalculateBounds();
        m.RecalculateNormals();

        return lakeSurface;
    }

    static Vector3[] shift(Vector3[] original, Vector3 offset)
    {
        Vector3[] mod = new Vector3[original.Length];
        int index = 0;
        foreach (Vector3 prev in original)
        {
            mod[index++] = prev + offset;
        }
        return mod;
    }
}
