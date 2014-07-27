using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class ServerScript : MonoBehaviour {

	private bool isServer = false;

	private string fileLocation;
	private int port;
	private int maxPlayers;
	private string serverType;
	private string serverName;
	private string serverComments;

	// Use this for initialization
	void Start () {

		MasterServer.ipAddress = "108.168.70.201";
		MasterServer.port = 23466;
		Debug.Log("Connected to MasterServer");

		fileLocation = "Config.xml";
		LoadFunction();

		Debug.Log(isServer);

		if (isServer) {
			createServer();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(isServer);
	}

	void createServer() {
		Network.InitializeServer(maxPlayers, port);
		MasterServer.dedicatedServer = true;
		MasterServer.RegisterHost(serverType, serverName, serverComments);
		checkScene.checkMenu = true;
		GOD.isLoad = true;
		Application.LoadLevel("MainGame");
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info) {
		Debug.Log("Could not connect to master server: " + info);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.RegistrationSucceeded)
			Debug.Log("Server registered");
	}

	void LoadFunction() {
		
		XmlDocument doc = new XmlDocument();
		doc.Load(fileLocation);

		XmlNode dedicatedServerNode = doc.SelectSingleNode("/Config/ServerData/DedicatedServer");
		XmlNode ServerPortNode = doc.SelectSingleNode("/Config/ServerData/ServerPort");
		XmlNode ServerMaxPlayersNode = doc.SelectSingleNode("/Config/ServerData/ServerMaxPlayers");
		XmlNode ServerTypeNode = doc.SelectSingleNode("/Config/ServerData/ServerType");
		XmlNode ServerNameNode = doc.SelectSingleNode("/Config/ServerData/ServerName");
		XmlNode ServerCommentsNode = doc.SelectSingleNode("/Config/ServerData/ServerComments");

		if (dedicatedServerNode.InnerText == "True") {
			isServer = true;
		}

		int.TryParse(ServerPortNode.InnerText, out port);
		int.TryParse(ServerMaxPlayersNode.InnerText, out maxPlayers);
		serverType = ServerTypeNode.InnerText;
		serverName = ServerNameNode.InnerText;
		serverComments = ServerCommentsNode.InnerText;

		Debug.Log("Load Complete!");
	}
}
