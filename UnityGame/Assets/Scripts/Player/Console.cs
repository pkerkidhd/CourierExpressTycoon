using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour {

	private Economy_HUD ecoHUD;
	private bool isOpen = false;
	private string consoleCommand = "";

	string[] strArr = null;
	char[] splitchar = { ' ' };

	// Use this for initialization
	void Start () {
		ecoHUD = GetComponent<Economy_HUD>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1)) {
			isOpen = !isOpen;
		}

	}

	void OnGUI() {
		if(isOpen) {
			consoleCommand = GUI.TextField(new Rect(10, Screen.height / 2, 200, 20), consoleCommand, 25);
			if (GUI.Button(new Rect(250, Screen.height / 2, 50, 30), "Send")) { 
				strArr = consoleCommand.Split(splitchar);
				for (int i = 0; i < strArr.Length; i++) {
					Debug.Log(strArr[i]);
				}
				if (strArr[0].Contains("money")) {
					float _money;
					float.TryParse(strArr[1], out _money);
					ecoHUD.playerMoney += _money;
				}
			}
		}
	}
}
