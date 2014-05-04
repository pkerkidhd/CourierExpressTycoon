using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaceableBuilding : MonoBehaviour {

	[HideInInspector]
	public List<Collider> colliders = new List<Collider>();

	private bool IsSelected;

	void OnGUI() {
		if(IsSelected) {
			GUI.Button(new Rect(100, 200, 100, 30), name);
		}
	}

	void OnTriggerEnter(Collider c) {
		if(c.tag == "Building") {
			colliders.Add(c);
		}
	}
	
	void OnTriggerExit(Collider c) {
		if(c.tag == "Building") {
			colliders.Remove(c);
		}
	}

	public void SetSelected(bool s) {
		IsSelected = s;
	}
}
