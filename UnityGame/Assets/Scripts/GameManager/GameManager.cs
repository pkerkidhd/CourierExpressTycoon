using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static Economy_HUD ecoHUD;

	public static bool isBuilding = false;

	// Use this for initialization
	void Start () {
		ecoHUD = (Economy_HUD)FindObjectOfType(typeof(Economy_HUD));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
