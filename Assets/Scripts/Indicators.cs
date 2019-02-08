using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicators : MonoBehaviour {

    public GameObject ledge;

    // Update is called once per frame
    void Update()
    {
        transform.position = ledge.transform.position;
        transform.rotation = ledge.transform.rotation;
    }

    public void newIndicator(int i, bool delim = false)
    {
        string text;
        if (i == -1)
            text = "End";
        else if (delim)
            text = "Part 1 finished";
        else
            text = i.ToString();
        transform.GetChild(0).GetComponent<TextMesh>().text = text;
    }
}
