using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LotBuildingCheck : MonoBehaviour {

	public List<Collider> buildings = new List<Collider>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < buildings.Count; i++) {
			if(buildings[i] == null) {
				buildings.RemoveAt(i);
			}
		}
	}

	void OnTriggerEnter(Collider c) {
		if(c.tag == "Building") {
			buildings.Add(c);
		}
	}
	void OnTriggerExit(Collider c) {
		if(c.tag == "Building") {
			buildings.Remove(c);
		}
	}
}
