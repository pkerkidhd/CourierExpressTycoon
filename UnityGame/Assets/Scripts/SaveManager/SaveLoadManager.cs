using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class SaveLoadManager : MonoBehaviour {

	public PlayerData playerData;
	private Economy_HUD ecoHUD;
	private Clock clock;

	public bool isPause = false;

	private string _playerName;
	
	void Start () {

		ecoHUD = GetComponent<Economy_HUD>();
		clock = GetComponent<Clock>();

	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			isPause = !isPause;
		}
	}

	void OnGUI() {
		if (isPause) {
			GUI.Box(new Rect(Screen.width / 2, Screen.height / 2,150,90), "Options");
			if(GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 + 30,80,20), "Save")) {
				SaveFunction();
			}
			if(GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 + 55,80,20), "Load")) {
				LoadFunction();
			}
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
	}
	
	void LoadFunction() {

		// Create an XmlReader
		using (XmlReader reader = XmlReader.Create(@"Assets\Res\SaveData.xml"))
		{
			while (reader.Read()) {
				if (reader.IsStartElement()){
					// Get element name and switch on it.
					switch (reader.Name)
					{
					case "PlayerName":
						if (reader.Read())
						{
							_playerName = reader.Value;
						}
						break;
					case "Money":
						if (reader.Read())
						{
								float.TryParse(reader.Value, out ecoHUD.playerMoney);
						}
						break;
					case "Seconds":
						if (reader.Read())
						{
							float.TryParse(reader.Value, out clock.seconds);
						}
						break;
					case "Minutes":
						if (reader.Read())
						{
							float.TryParse(reader.Value, out clock.minutes);
						}
						break;
					case "Hours":
						if (reader.Read())
						{
							float.TryParse(reader.Value, out clock.hours);
						}
						break;
					case "OldHours":
						if (reader.Read())
						{
							float.TryParse(reader.Value, out clock.oldHours);
						}
						break;
					case "IsAM":
						if (reader.Read())
						{
							clock.isAm = bool.Parse(reader.Value);
						}
						break;
					}
				}
			}
		}

		Debug.Log("Load Complete!");
	}

	void SaveFunction() {
		playerData = new PlayerData("PkerkidHD", ecoHUD.playerMoney);
		
		XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
		{
			Indent = true,
			IndentChars = "\t",
			NewLineOnAttributes = true
		};
		
		using (XmlWriter xml = XmlWriter.Create(@"Assets\Res\SaveData.xml", xmlWriterSettings)) {
			
			//ROOT
			xml.WriteStartDocument();
			xml.WriteStartElement("PlayerData");

			//Player Data
			xml.WriteStartElement("Player");
			xml.WriteElementString("PlayerName", playerData.PlayerName);
			xml.WriteElementString("Money", playerData.Money.ToString());
			xml.WriteEndElement();

			//Clock Data
			xml.WriteStartElement("Clock");
			xml.WriteElementString("Seconds", clock.seconds.ToString());
			xml.WriteElementString("Minutes", clock.minutes.ToString());
			xml.WriteElementString("Hours", clock.hours.ToString());
			xml.WriteElementString("OldHours", clock.oldHours.ToString());
			xml.WriteElementString("IsAM", clock.isAm.ToString());
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