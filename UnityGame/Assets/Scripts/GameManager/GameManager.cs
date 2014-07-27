using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static CurrencySystem currencySys;
	public static ClockSystem clockSys;
	public static UpdateSytem updateSys;
	public static CalendarSystem calendarSys;

	//Check if building Menu is up
	public static bool isBuilding = false;
	public static bool isLotClicked = false;

	// Use this for initialization
	void Start () {
		currencySys = GetComponent<CurrencySystem>();
		clockSys = GetComponent<ClockSystem>();
		updateSys = GetComponent<UpdateSytem>();
		calendarSys = GetComponent<CalendarSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
