using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {

	public GameObject[] buildings;
	private BuildingPlacement buildingPlacement;
	private Economy_HUD ecoHUD;

	private float buildingPrice = 25;
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
		for (int i = 0; i < buildings.Length; i++) {
			if (GUI.Button(new Rect(Screen.width/20,Screen.height/15 + Screen.height/12 * i, 100, 30), buildings[i].name)) {
				newBalance = ecoHUD.playerMoney - buildingPrice;
				if (newBalance >= 0) {
					buildingPlacement.SetItem(buildings[i], buildingPrice);
				}
			}
		}
	}
}
