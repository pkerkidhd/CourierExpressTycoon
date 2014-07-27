﻿using UnityEngine;
using System.Collections;

public class LotManagment : MonoBehaviour {
	
	public float lotPrice;
	public GameObject lot_Txt;
	public GameObject lot_Placement;
	public GameObject[] spawnCarNode;

	private float newBalance = 0;
	private bool isConfirm = false;
	private LotSaving lotSaving;

	void Start () {
		renderer.material.color = Color.grey;
		lot_Txt.GetComponent<TextMesh>().text = "Lot Price: $" + lotPrice;
		lotSaving = transform.parent.GetComponent<LotSaving>();
	}

	void Update () {
		if(lotSaving.isBought) {
			lot_Placement.SetActive(true);
			foreach (GameObject go in spawnCarNode) {
				go.SetActive(true);
			}
			lot_Txt.SetActive(false);
			lotSaving.isBought = true;
		}
	}

	void OnMouseOver() {
		if (!lotSaving.isBought) {
			renderer.material.color -= new Color(0.1F, 0, 0) * 2 * Time.deltaTime;
		}
	}

	void OnMouseExit() {
		if (!lotSaving.isBought) {
			renderer.material.color = Color.grey;
		}
	}

	void OnMouseDown() {
		if (!lotSaving.isBought && !GameManager.isBuilding && !GameManager.isLotClicked) {
			isConfirm = true;
			GameManager.isLotClicked = true;
		}
	}

	void OnGUI() {
		if(!GameManager.isBuilding) {
			if(isConfirm && GameManager.isLotClicked) {
				GUI.Box(new Rect(Screen.width / 2, Screen.height / 2,150,90), "Buy Lot for $" + lotPrice);
				if(GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 + 30,80,20), "Yes")) {
					renderer.material.color = Color.grey;
					newBalance = GameManager.currencySys.getPlyMoney() - lotPrice;
					if (newBalance >= 0) {
						GameManager.currencySys.updatePlyMoney(-lotPrice);
						lot_Placement.SetActive(true);
						foreach (GameObject go in spawnCarNode) {
							go.SetActive(true);
						}
						lot_Txt.SetActive(false);
						lotSaving.isBought = true;
						GameManager.isLotClicked = false;
						isConfirm = false;
					}
				}
				if(GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 + 55,80,20), "No")) {
					GameManager.isLotClicked = false;
					isConfirm = false;
				}

			}
		}
	}

}
