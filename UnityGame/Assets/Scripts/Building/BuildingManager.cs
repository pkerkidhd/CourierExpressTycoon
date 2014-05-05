using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {

	public GameObject[] buildings;
	private BuildingPlacement buildingPlacement;
	private Economy_HUD ecoHUD;

	private float buildingPrice = 0;
	private float newBalance = 0;

	// Use this for initialization
	void Start () {
		buildingPlacement = GetComponent<BuildingPlacement>();
		ecoHUD = GetComponent<Economy_HUD>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		int i = 0;
		foreach (GameObject go in buildings) {
			buildingPrice = go.gameObject.GetComponent<BuildingCost>().Cost;
			if (GUI.Button(new Rect(Screen.width/20,Screen.height/15 + Screen.height/12 * i, 100, 30), go.gameObject.name)) {
				newBalance = ecoHUD.playerMoney - buildingPrice;
				if (newBalance >= 0) {
					buildingPlacement.SetItem(go.gameObject, buildingPrice);
				}
			}
			i++;
		}
	}
}
