using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleAffordance : MonoBehaviour {
    public float poleDist;

	// Use this for initialization
	void Start () {
        Transform left = transform.GetChild(0);
        Transform right = transform.GetChild(1);
        BoxCollider collider = GetComponent<BoxCollider>();
        left.localPosition = new Vector3(-0.15f - poleDist / 2, left.localPosition.y, 0);
        right.localPosition = new Vector3(0.15f + poleDist / 2, right.localPosition.y, 0);
        collider.size = new Vector3(poleDist, 2, 1);
    }
}
