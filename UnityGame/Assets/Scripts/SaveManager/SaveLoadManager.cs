using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class SaveLoadManager : MonoBehaviour {

	public GameObject[] buildings;
	public GameObject[] lots;
	public bool isPause = false;

	public PlayerData playerData;
	public BuildingData buildingData;
	public LotData lotData;

	private ProxyFunctions proxyFunc;

	private string _playerName;

	private string fileLocation;
	private GameObject god;

	private float loadMoney;


	void Start () {

		proxyFunc = GetComponent<ProxyFunctions>();

		lots = GameObject.FindGameObjectsWithTag("Lot");
		//fileLocation = @"Assets\Res\SaveData.xml";
		fileLocation = "SaveData.xml";

		god = GameObject.Find("GOD");

		if (GOD.isLoad) {
			LoadFunction();
			GOD.isLoad = false;
		}
	}

	void Update() {

		if(Input.GetKeyDown(KeyCode.Escape)) {
			isPause = !isPause;
		}

	}

	void OnGUI() {
		if (isPause) {
			GameManager.clockSys.IsPause = true;
			GUI.Box(new Rect(Screen.width / 2, Screen.height / 2,150,90), "Options");
			if(GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 + 30,80,20), "Save")) {
				SaveFunction();
			}
			if(GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 + 55,80,20), "Back To Main Menu")) {
				Destroy(god);
				Application.LoadLevel("MainMenu");
			}
		} else {
			GameManager.clockSys.IsPause = false;
		}
	}
	
	void LoadFunction() {

		XmlDocument doc = new XmlDocument();
		doc.Load(fileLocation);

		//Loading Player Data
		XmlNode playerNameNode = doc.SelectSingleNode("/SaveData/PlayerData/Player/PlayerName");
		XmlNode playerMoneyNode = doc.SelectSingleNode("/SaveData/PlayerData/Player/Money");
		_playerName = playerNameNode.InnerText;
		float.TryParse(playerMoneyNode.InnerText, out loadMoney);
		GameManager.currencySys.loadPlyMoney(loadMoney);

		//Loading Clock Data
		XmlNode clockSecondsNode = doc.SelectSingleNode("/SaveData/PlayerData/Clock/Seconds");
		XmlNode clockMinutesNode = doc.SelectSingleNode("/SaveData/PlayerData/Clock/Minutes");
		XmlNode clockHoursNode = doc.SelectSingleNode("/SaveData/PlayerData/Clock/Hours");
		XmlNode clockOldHoursNode = doc.SelectSingleNode("/SaveData/PlayerData/Clock/OldHours");
		XmlNode clockIsAmNode = doc.SelectSingleNode("/SaveData/PlayerData/Clock/IsAM");
		float.TryParse(clockSecondsNode.InnerText, out GameManager.clockSys.seconds);
		float.TryParse(clockMinutesNode.InnerText, out GameManager.clockSys.minutes);
		float.TryParse(clockHoursNode.InnerText, out GameManager.clockSys.hours);
		float.TryParse(clockOldHoursNode.InnerText, out GameManager.clockSys.oldHours);
		GameManager.clockSys.isAm = bool.Parse(clockIsAmNode.InnerText);

		//Loading Lot Data
		XmlNodeList lotsListNode = doc.SelectNodes("/SaveData/LotData/Lot");
		foreach (XmlNode lln in lotsListNode) {
			string _name = lln.SelectSingleNode("Name").InnerText;
			for (int i = 0; i < lots.Length; i++) {
				if(lots[i].name == _name) {
					lots[i].GetComponent<LotSaving>().isBought = true;
				}
			}
		}

		//Loading Building Data
		XmlNodeList buildingsListNode = doc.SelectNodes("/SaveData/BuildingData/Building");

		foreach (XmlNode bln in buildingsListNode) {
			string _name = bln.SelectSingleNode("Name").InnerText;
			float _x;
			float _y;
			float _z;
			float.TryParse(bln.SelectSingleNode("X").InnerText, out _x);
			float.TryParse(bln.SelectSingleNode("Y").InnerText, out _y);
			float.TryParse(bln.SelectSingleNode("Z").InnerText, out _z);
			proxyFunc.ProxyBuilding(_name, _x, _y, _z);
		}

		Debug.Log("Load Complete!");
	}

	void SaveFunction() {
		
		buildings = GameObject.FindGameObjectsWithTag("Building");

		playerData = new PlayerData("PkerkidHD", GameManager.currencySys.getPlyMoney());
		
		XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
		{
			Indent = true,
			IndentChars = "\t",
			NewLineOnAttributes = true
		};
		
		using (XmlWriter xml = XmlWriter.Create(fileLocation, xmlWriterSettings)) {
			
			//ROOT
			xml.WriteStartDocument();
			xml.WriteStartElement("SaveData");

			//Player Data
			xml.WriteStartElement("PlayerData");
			xml.WriteStartElement("Player");
			xml.WriteElementString("PlayerName", playerData.PlayerName);
			xml.WriteElementString("Money", playerData.Money.ToString());
			xml.WriteEndElement();

			//Clock Data
			xml.WriteStartElement("Clock");
			xml.WriteElementString("Seconds", GameManager.clockSys.seconds.ToString());
			xml.WriteElementString("Minutes", GameManager.clockSys.minutes.ToString());
			xml.WriteElementString("Hours", GameManager.clockSys.hours.ToString());
			xml.WriteElementString("OldHours", GameManager.clockSys.oldHours.ToString());
			xml.WriteElementString("IsAM", GameManager.clockSys.isAm.ToString());
			xml.WriteEndElement();
			xml.WriteEndElement();

			//Building Data
			xml.WriteStartElement("BuildingData");
			for (int i = 0; i < buildings.Length; i++) {
				buildingData = new BuildingData(buildings[i].name, buildings[i].transform.position.x, buildings[i].transform.position.y, buildings[i].transform.position.z);
				xml.WriteStartElement("Building");
				xml.WriteElementString("Name", buildingData.BlgName);
				xml.WriteElementString("X", buildingData.X.ToString());
				xml.WriteElementString("Y", buildingData.Y.ToString());
				xml.WriteElementString("Z", buildingData.Z.ToString());
				xml.WriteEndElement();
			}
			xml.WriteEndElement();

			//Lot Data
			xml.WriteStartElement("LotData");

			for (int i = 0; i < lots.Length; i++) {
				if (lots[i].GetComponent<LotSaving>().isBought) {
					xml.WriteStartElement("Lot");
					lotData = new LotData(lots[i].name, true);
					xml.WriteElementString("Name", lotData.LotName);
					xml.WriteElementString("Bought", "True");
					xml.WriteEndElement();
				}
			}

			xml.WriteEndElement();
			// End.
			xml.WriteEndElement();
			xml.WriteEndDocument();
			xml.Flush();
		}
		
		Debug.Log ("Save Complete");
	}

}

public class PlayerData {

	private string _playerName;
	private float _money;
	
	public PlayerData(string playerName, float money) {
		_playerName = playerName;
		_money = money;
	}
	
	public string PlayerName {
		get 
		{
			return _playerName;
		}
	}
	
	public float Money {
		get 
		{
			return _money;
		}
	}
}

public class BuildingData {

	private string _blgName;

	private float _x;
	private float _y;
	private float _z;

	public BuildingData(string blgName, float x, float y, float z) {
		_blgName = blgName;
		_x = x;
		_y = y;
		_z = z;
	}

	public string BlgName {
		get 
		{
			return _blgName;
		}
	}
	
	public float X {
		get 
		{
			return _x;
		}
	}

	public float Y {
		get 
		{
			return _y;
		}
	}

	public float Z {
		get 
		{
			return _z;
		}
	}
}

public class LotData {
	private string _lotName;
	private bool _isBought;

	public LotData(string LotName, bool isBought) {
		_lotName = LotName;
		_isBought = isBought;
	}

	public string LotName {
		get 
		{
			return _lotName;
		}
	}

	public bool IsBought {
		get 
		{
			return _isBought;
		}
	}
}