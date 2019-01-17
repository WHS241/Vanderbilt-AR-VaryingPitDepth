using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

/**
 * Hides experiment when navigating in menu
 **/
public class AffordanceActivator : MonoBehaviour, IInputClickHandler {
    public GameObject experiment; // parent of all affordances; shuffler attached
    public GameObject[] experimentComp; // experiment-specific objects to be disabled when using menu
    public GameObject[] menuSpecific;  // show only when navigating menu
    public GameObject expCenter; //BoxCollider needs to be deactivated during experiment

	// Use this for initialization
	void Start () {
        experiment.SetActive(false);
		foreach(GameObject exp in experimentComp)
        {
            exp.SetActive(false);
        }
	}

    // click to start
    public virtual void OnInputClicked(InputClickedEventData eventData)
    {
        foreach(GameObject exp in experimentComp)
        {
            exp.SetActive(true);
        }
        foreach(GameObject exp in menuSpecific)
        {
            exp.SetActive(false);
        }

        expCenter.GetComponent<HoloToolkit.Unity.SpatialMapping.TapToPlace>().enabled = false;
        expCenter.GetComponent<BoxCollider>().enabled = false;

        experiment.SetActive(true);
        experiment.GetComponent<AffordanceManager>().experimentStart();

        gameObject.SetActive(false);
    }
}
