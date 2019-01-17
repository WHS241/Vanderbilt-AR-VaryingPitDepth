using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceView : MonoBehaviour {
    public GameObject closeMarker;
    public GameObject farMarker;
    public GameObject indicator;

    public void requestDistanceUse(bool farDist)
    {
        closeMarker.SetActive(!farDist);
        farMarker.SetActive(farDist);
        indicator.GetComponent<Indicators>().ledge = farDist ? farMarker : closeMarker;
    }
}
