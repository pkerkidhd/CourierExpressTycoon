using UnityEngine;
using System.Collections;

public class LotManagment : MonoBehaviour {
	
	public float lotPrice;
	public GameObject lot_Txt;
	public GameObject lot_Placement;
	public GameObject[] spawnCarNode;

	private float newBalance = 0;
	private bool isConfirm = false;
	private bool isBought = false;
	
	void Start () {
		renderer.material.color = Color.grey;
		lot_Txt.GetComponent<TextMesh>().text = "Lot Price: $" + lotPrice;
	}

	void Update () {

	}

	void OnMouseOver() {
		if (!isBought) {
			renderer.material.color -= new Color(0.1F, 0, 0) * 2 * Time.deltaTime;
		}
	}

	void OnMouseExit() {
		if (!isBought) {
			renderer.material.color = Color.grey;
		}
	}

	void OnMouseDown() {
		if (!isBought) {
			isConfirm = true;
		}
	}

	void OnGUI() {
		if(!GameManager.isBuilding) {
			if(isConfirm) {
				GUI.Box(new Rect(Screen.width / 2, Screen.height / 2,150,90), "Buy Lot for $" + lotPrice);
				if(GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 + 30,80,20), "Yes")) {
					newBalance = GameManager.ecoHUD.playerMoney - lotPrice;
					if (newBalance >= 0) {
						GameManager.ecoHUD.playerMoney -= lotPrice;
						lot_Placement.SetActive(true);
						foreach (GameObject go in spawnCarNode) {
							go.SetActive(true);
						}
						lot_Txt.SetActive(false);
						isBought = true;
						isConfirm = false;
					}
				}
				if(GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 + 55,80,20), "No")) {
					isConfirm = false;
				}

			}
		}
	}

}
