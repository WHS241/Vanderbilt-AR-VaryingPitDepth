using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;
using System.IO;

public class AffordanceManager : MonoBehaviour, IInputClickHandler {


    public GameObject line; // indicates where the participant should be
    public GameObject indicators; // indicates to participant when next affordance is ready

    private int delim = 0;
    private int[] shuffledOrder;
    private int[] displayNumber;
    private Transform[] child;
    private int currentActive;
    private TextWriter ostream;
    private DistanceView distance;

	// Use this for initialization
	public void experimentStart () {
        currentActive = -1;
        shuffledOrder = new int[transform.childCount];
        displayNumber = new int[transform.childCount];
        child = new Transform[transform.childCount];
        List<int> pit = new List<int>(); // Concave affordances
        List<int> bump = new List<int>(); // Convex affordances

        Random.InitState((int)(System.DateTime.Now.Ticks % System.Int64.MaxValue - System.Int32.MaxValue));

        for (int i = 0; i < transform.childCount; ++i)
        {
            child[i] = transform.GetChild(i);
            switch(child[i].ToString()[0])
            {
                case 'B':
                    bump.Add(i);
                    break;

                case 'P':
                    delim = i;
                    break;

                default:
                    pit.Add(i);
                    break;
            }
            displayNumber[i] = Random.Range(0, 1000000);
            child[i].gameObject.SetActive(false);
        }

        int[] bumpCopy = bump.ToArray();
        int[] pitCopy = pit.ToArray();

        shuffle(bumpCopy);
        shuffle(pitCopy);

        applyShuffleConstraints(bumpCopy);
        applyShuffleConstraints(pitCopy);

        int currentIndex = 0;
        addIndices(pitCopy, ref currentIndex);
        shuffledOrder[currentIndex++] = delim;
        addIndices(bumpCopy, ref currentIndex);

        string path = Path.Combine(Application.persistentDataPath, "GapOrder.txt");
        ostream = File.CreateText(path);
        using (ostream)
        {
            foreach (int i in shuffledOrder)
            {
                ostream.Write(displayNumber[i]);
                ostream.WriteLine(" " + child[i].ToString());
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = line.transform.position - line.transform.rotation * new Vector3(0, 0, line.transform.localScale.z * 5);
        transform.rotation = line.transform.rotation;

	}

    public virtual void OnInputClicked(InputClickedEventData eventData)
    {  
        if (currentActive >= child.Length) { return; }
        if(currentActive == -1)
        {
            line.SetActive(false);
        }
        else if (currentActive >= 0)
        {
            child[shuffledOrder[currentActive]].gameObject.SetActive(false);
        }
        ++currentActive;
        if (currentActive < child.Length && currentActive >= 0)
        {
            child[shuffledOrder[currentActive]].gameObject.SetActive(true);
        }

        if (currentActive != child.Length)
            indicators.GetComponent<Indicators>().newIndicator(displayNumber[shuffledOrder[currentActive]], (shuffledOrder[currentActive] == delim));
        else
            indicators.GetComponent<Indicators>().newIndicator(-1);
    }

    private void applyShuffleConstraints(int[] index)
    {
        bool applied = false;
        List<int> toSwap = new List<int>();
        while (!applied)
        {
            foreach (int i in toSwap)
            {
                int target = Random.Range(0, index.Length);
                int temp = index[i];
                index[i] = index[target];
                index[target] = temp;
            }

            toSwap.Clear();
            applied = true;

            for (int i = 1; i < index.Length; ++i)
            {
                float toCompareWidth = child[index[i]].GetComponent<GapAffordance>().width;
                float toKeepWidth = child[index[i - 1]].GetComponent<GapAffordance>().width;
                float toCompareDepth = child[index[i]].GetComponent<GapAffordance>().vertical;
                float toKeepDepth = child[index[i - 1]].GetComponent<GapAffordance>().vertical;

                if (Mathf.Abs(toCompareWidth - toKeepWidth) < 1e-5 && Mathf.Abs(toCompareDepth - toKeepDepth) < 1e-5)
                {
                    applied = false;
                    toSwap.Add(i);
                }
            }
        }
    }

    /**
     * Shuffles an array
     * @param target - the array to shuffle
     **/
    private void shuffle(int[] target)
    {
        for (int i = 0; i < target.Length; ++i)
        {
            int index = Random.Range(i, target.Length);
            int temp = target[i];
            target[i] = target[index];
            target[index] = temp;
        }
    }

    /**
     * Copies values to shuffledOrder
     * @param source - array containing values to copy
     * @param index - index to start copying
     **/
    private void addIndices(int[] source, ref int index)
    {
        for (int i = 0; i < source.Length; ++i)
        {
            shuffledOrder[index++] = source[i];
        }
    }

}
