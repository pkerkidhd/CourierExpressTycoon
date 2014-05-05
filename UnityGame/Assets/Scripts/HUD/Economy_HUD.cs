using UnityEngine;
using System.Collections;

public class Economy_HUD : MonoBehaviour {

	public float playerMoney = 100.00f;
	private float playerMoneyConvert = 0.0f;

	private string currency_url = "http://davidvo.net/unity/currency.php";
	private string conversion = "0";

	IEnumerator Start () {
		WWW www = new WWW(currency_url);
		yield return www;
		
		conversion = www.text;

		Debug.Log(www.text);
	}

	void Update () {

		playerMoneyConvert = playerMoney * float.Parse(conversion);
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 200, 20), "Money: $" + playerMoney + " USD");
		//"$1.00 USD to CAD = $" + conversion
		GUI.Label(new Rect(10, 25, 200, 20), "Your Money in CAD: $" + playerMoneyConvert);
	}

}
