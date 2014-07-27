using UnityEngine;
using System.Collections;

public class Servers : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MasterServer.ClearHostList();
		MasterServer.RequestHostList("Survival");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RefreshServerList() {
		if (MasterServer.PollHostList().Length != 0) {
			HostData[] hostData = MasterServer.PollHostList();
			int i = 0;
			while (i < hostData.Length) {
				Debug.Log("Game name: " + hostData[i].gameName);
				i++;
			}
			MasterServer.ClearHostList();
		}
	}
}
