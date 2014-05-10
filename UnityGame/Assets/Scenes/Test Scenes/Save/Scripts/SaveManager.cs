using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class SaveManager : MonoBehaviour {

	public PlayerData1 playerData;

	// Use this for initialization
	void Start () {

		playerData = new PlayerData1("PkerkidHD", 10.0f);

		XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
		{
			Indent = true,
			IndentChars = "\t",
			NewLineOnAttributes = true
		};

		using (XmlWriter xml = XmlWriter.Create("PlayerData.xml", xmlWriterSettings)) {

			//ROOT
			xml.WriteStartDocument();
			xml.WriteStartElement("PlayerData");
				
			xml.WriteStartElement("Player");
			xml.WriteElementString("PlayerName", playerData.PlayerName);
			xml.WriteElementString("Money", playerData.Money.ToString());
			xml.WriteEndElement();

			// End.
			xml.WriteEndElement();
			xml.WriteEndDocument();
			xml.Flush();
		}

		Debug.Log ("DONE");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class PlayerData1 {
	private string _playerName;
	private float _money;

	public PlayerData1(string playerName, float money) {
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
