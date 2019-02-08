using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAffordance : MonoBehaviour
{
    public float length, width, height;

    // Use this for initialization
    void Start()
    {
        Transform physical = transform.GetChild(0);
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 size = new Vector3(length, height, width);
        Vector3 position = new Vector3(0, -height / 2, 0);
        collider.size = size;
        collider.center = position;
        physical.localScale = size;
        physical.localPosition = position;
    }
}
