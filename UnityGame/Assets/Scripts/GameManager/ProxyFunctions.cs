using UnityEngine;
using System.Collections;

public class ProxyFunctions : MonoBehaviour {

	public Rigidbody House;
	public Rigidbody Office;
	public Rigidbody CET;

	private Rigidbody currentPrefab;

	void Start () {
	
	}

	public void ProxyBuilding(string blgName, float x, float y, float z) {
		Vector3 position = new Vector3(x, y, z);
		switch (blgName) {
		case "House":
			currentPrefab = House;
			break;
		case "Office":
			currentPrefab = Office;
			break;
		case "CET":
			currentPrefab = CET;
			break;
		}
		Rigidbody building;
		building = Instantiate(currentPrefab, position, Quaternion.identity) as Rigidbody;
		building.name = blgName;
	}

}
