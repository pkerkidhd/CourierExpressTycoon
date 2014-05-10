using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LotSaving : MonoBehaviour {

	public bool isBought = false;
	public GameObject LotPlacement;
	public List<Collider> buildings = new List<Collider>();


	void Start () {
	
	}

	void Update () {
		buildings = LotPlacement.GetComponent<LotBuildingCheck>().buildings;
	}
}
