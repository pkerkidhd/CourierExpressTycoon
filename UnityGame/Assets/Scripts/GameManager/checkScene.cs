using UnityEngine;
using System.Collections;

public class checkScene : MonoBehaviour {

	public static bool checkMenu = false;

	// Use this for initialization
	void Start () {
		if (!checkMenu) {
			Application.LoadLevel("MainMenu");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
