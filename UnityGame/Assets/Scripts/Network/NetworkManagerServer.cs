using UnityEngine;
using System.Collections;

public class NetworkManagerServer : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Network.isServer) {
			networkView.RPC("updateMoney", RPCMode.All);
			Debug.Log("I'm the server");
		} else if (Network.isClient) 
		{
			Debug.Log("I'm the client");
		}
	}

	[RPC]
	void updateMoney() {
		gameManager.GetComponent<CurrencySystem>().updatePlyMoney(100.0f);
	}
}
