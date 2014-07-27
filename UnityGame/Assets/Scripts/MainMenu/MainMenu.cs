using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	//Sound Options
	private float musicVolume = 0.75F;
	private float soundVolume = 0.75F;

	private float vSliderValue = 0.0F;
	private float hSValue = 0.0F;
	private float vSValue = 0.0F;

	//
	private string _btnName;
	private Resolution[] resolutions;
	private int[] refreshRate = new int[2] { 30, 60 };

	//
	private bool editing = false;

	private GOD god;

	private Servers server;

	//Network
	private bool loading = false;
	private Vector2 scrollPos = Vector2.zero;
	//private Vector2 scrollPos = new Vector2(Screen.width / 2, Screen.height / 2);

	// Use this for initialization
	void Start () {
		
		god = (GOD)FindObjectOfType(typeof(GOD));
		server = gameObject.AddComponent<Servers>();

		refreshHostList();

		resolutions = new Resolution[20];

		resolutions[0].width = 640;
		resolutions[0].height = 480;

		resolutions[1].width = 720;
		resolutions[1].height = 576;

		resolutions[2].width = 800;
		resolutions[2].height = 600;
		
		resolutions[3].width = 1024;
		resolutions[3].height = 768;

		resolutions[4].width = 1152;
		resolutions[4].height = 648;

		resolutions[5].width = 1152;
		resolutions[5].height = 864;

		resolutions[6].width = 1280;
		resolutions[6].height = 720;
		
		resolutions[7].width = 1280;
		resolutions[7].height = 800;
	
		resolutions[8].width = 1280;
		resolutions[8].height = 960;

		resolutions[9].width = 1280;
		resolutions[9].height = 1024;

		resolutions[10].width = 1360;
		resolutions[10].height = 768;

		resolutions[11].width = 1360;
		resolutions[11].height = 1024;
		
		resolutions[12].width = 1366;
		resolutions[12].height = 768;

		resolutions[13].width = 1400;
		resolutions[13].height = 1050;

		resolutions[14].width = 1440;
		resolutions[14].height = 900;
		
		resolutions[15].width = 1600;
		resolutions[15].height = 900;

		resolutions[16].width = 1600;
		resolutions[16].height = 1200;

		resolutions[17].width = 1680;
		resolutions[17].height = 1050;

		resolutions[18].width = 1776;
		resolutions[18].height = 1000;
		
		resolutions[19].width = 1920;
		resolutions[19].height = 1080;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		Settings.AutoResize();
		Settings.setGUISkin();

		GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 150, 300, 300), "");

		switch (_btnName)
		{
		case "Options":
			GUI.Label(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 95, 100, 20), "Music Volume: ");
			GUI.Label(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 57, 100, 20), "Sound Volume: ");
			musicVolume = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 75, 100, 30), musicVolume, 0.0F, 1.0F);
			soundVolume = GUI.HorizontalSlider(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 40, 100, 30), soundVolume, 0.0F, 1.0F);

			Settings.enableFullScreen = GUI.Toggle(new Rect(Screen.width / 2 - 25, Screen.height / 2 + -10, 100, 30), Settings.enableFullScreen, " FullScreen");

			if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 + 15, 100, 30), "Save Settings"))
			{
				Settings.setVolume(musicVolume, soundVolume);
				if (Settings.enableFullScreen) {
					Screen.fullScreen = true;
				} else {
					Screen.fullScreen = false;
				}
				Debug.Log("Save Settings BTN");
			}

			if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 + 50, 100, 30), "Back"))
			{
				setCurrentBtn(null);
				Debug.Log("Back BTN");
			}
			break;
		case "Multiplayer":
			GUILayout.BeginArea(new Rect(Screen.width / 2, Screen.height / 2, 200, 300));
			if (GUILayout.Button("Refresh")) {
				refreshHostList();
			}

			if (loading) {
				GUILayout.Label("Loading...");
			} else {
				scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(200.0f), GUILayout.Height(200.0f));
				HostData[] hosts = MasterServer.PollHostList();
				for (int i = 0; i < hosts.Length; i++) {
					if (GUILayout.Button(hosts[i].gameName, GUILayout.ExpandWidth(true))) {
						//Network.Connect(hosts[i]);
						//Debug.Log("Connected!");
						NetworkManager.connectServer(hosts[i]);
					}
				}
				if (hosts.Length == 0) 
				{
					GUILayout.Label("No servers running");
				}
			}
			GUILayout.EndHorizontal();

			if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 + 50, 100, 30), "Back"))
			{
				setCurrentBtn(null);
				Debug.Log("Back BTN");
			}
			break;
		default:
			if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 75, 100, 30), "New Game")) {
				checkScene.checkMenu = true;
				Application.LoadLevel("MainGame");
				Debug.Log("New Game BTN");
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 40, 100, 30), "Load Game")) {
				checkScene.checkMenu = true;
				GOD.isLoad = true;
				Application.LoadLevel("MainGame");
				Debug.Log("Load Game BTN");
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 5, 100, 30), "Options")) {
				setCurrentBtn("Options");
				Debug.Log("Options BTN");
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 125, 100, 30), "Multiplayer")) {
				MasterServer.RequestHostList("Survival");
				setCurrentBtn("Multiplayer");
				Debug.Log("Multiplayer BTN");
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 + 30, 100, 30), "Quit"))
				Application.Quit();
			break;
		}




//		vSliderValue = GUI.VerticalSlider(new Rect(10, 170, 100, 30), vSliderValue, 10.0F, 0.0F);
//		hSValue = GUI.HorizontalScrollbar(new Rect(10, 210, 100, 30), hSValue, 1.0F, 0.0F, 10.0F);
//		vSValue = GUI.VerticalScrollbar(new Rect(10, 230, 100, 30), vSValue, 1.0F, 10.0F, 0.0F);
	}

	void setCurrentBtn(string btnName) {
		_btnName = btnName;
	}

	void refreshHostList() {
		loading = true;
		MasterServer.ClearHostList();
		MasterServer.RequestHostList("Survival");
	}

	void OnFailedToConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: " + error);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived)
			loading = false;
	}

	void OnFailedToConnectToMasterServer(NetworkConnectionError info) {
		Debug.Log("Could not connect to master server: " + info);
	}

}
