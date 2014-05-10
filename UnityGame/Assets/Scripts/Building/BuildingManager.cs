using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {

	public GameObject[] buildings;
	private BuildingPlacement buildingPlacement;

	private float buildingPrice = 0;
	private float newBalance = 0;

	// Use this for initialization
	void Start () {
		buildingPlacement = GetComponent<BuildingPlacement>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		int i = 0;
		foreach (GameObject go in buildings) {
			buildingPrice = go.gameObject.GetComponent<BuildingCost>().Cost;
			if (GUI.Button(new Rect(Screen.width/20,Screen.height/15 + Screen.height/12 * i, 100, 30), go.gameObject.name)) {
				if(!GameManager.isBuilding) {
					newBalance = GameManager.ecoHUD.playerMoney - buildingPrice;
					if (newBalance >= 0) {
						buildingPlacement.SetItem(go.gameObject, buildingPrice);
						GameManager.isBuilding = true;
						GameManager.isLotClicked = false;
					}
				}
			}
			i++;
		}
	}
}
