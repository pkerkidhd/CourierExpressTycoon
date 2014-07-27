using UnityEngine;
using System.Collections;

public class CalendarSystem : MonoBehaviour {

	public GUIText calendarText;
	public string dayTitleText;
	public string monthTitleText;
	public int dayTitle;
	public int day;
	public int month;
	public int year;

	// Use this for initialization
	void Start () {
		dayTitleText = "Sunday";
		dayTitle = 1;
		day = 1;
		monthTitleText = "January";
		month = 1;
		year = 2014;

		calendarText.text = dayTitleText + ", " + monthTitleText + " " + day + ", " + year;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateDate() {
		//Jan, Mar, May, Jul, Aug, Oct, Dec
		if(month == 1 && day == 31 || month == 3 && day == 31 || month == 5 && day == 31 || month == 7 && day == 31 || month == 8 && day == 31 || month == 10 && day == 31 || month == 12 && day == 31) { 
			month++;
			dayTitle++;
			day = 1;
			GameManager.currencySys.updatePlyMoney(-200.0f);
		} else if (month == 2 && day == 30 || month == 4 && day == 30 || month == 6 && day == 30 || month == 9 && day == 30 || month == 11 && day == 30) {
			month++;
			dayTitle++;
			day = 1;
			GameManager.currencySys.updatePlyMoney(-200.0f);
		} else {
			dayTitle++;
			day++;
		}

		if (dayTitle == 8) {
			dayTitle = 1;
		}

		if (month == 13) {
			month = 1;
			year++;
		}

		switch(dayTitle) {
		case 1:
			dayTitleText = "Sunday";
			break;
		case 2:
			dayTitleText = "Monday";
			break;
		case 3:
			dayTitleText = "Tuesday";
			break;
		case 4:
			dayTitleText = "Wednesday";
			break;
		case 5:
			dayTitleText = "Thursday";
			break;
		case 6:
			dayTitleText = "Friday";
			break;
		case 7:
			dayTitleText = "Saturday";
			break;
		}

		switch(month) {
		case 1:
			monthTitleText = "January";
			break;
		case 2:
			monthTitleText = "Feburary";
			break;
		case 3:
			monthTitleText = "March";
			break;
		case 4:
			monthTitleText = "April";
			break;
		case 5:
			monthTitleText = "May";
			break;
		case 6:
			monthTitleText = "June";
			break;
		case 7:
			monthTitleText = "July";
			break;
		case 8:
			monthTitleText = "August";
			break;
		case 9:
			monthTitleText = "September";
			break;
		case 10:
			monthTitleText = "October";
			break;
		case 11:
			monthTitleText = "November";
			break;
		case 12:
			monthTitleText = "December";
			break;
		}

		calendarText.text = dayTitleText + ", " + monthTitleText + " " + day + ", " + year;
	}
}
