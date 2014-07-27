using UnityEngine;
using System.Collections;

public class ClockSystem : MonoBehaviour {

	public GUIText time_Txt;

	public float seconds = 0;
	public float minutes = 00;
	public float hours = 01;
	public float oldHours = 00;
	
	public bool isAm = true;
	public bool _isPause = false;

	// Use this for initializat
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (!_isPause) {
			Time.timeScale = 1.0f;
			if (GameManager.updateSys.SpeedStatus == 1) {
				seconds += 30.02f;
			} else if(GameManager.updateSys.SpeedStatus == 2) {
				minutes += 1.00f;
				seconds = 60;
			} else if(GameManager.updateSys.SpeedStatus == 3) {
					minutes = 60;
					seconds = 60;
			} else { 
				seconds += 0.02f;
			}
			
			if (isAm) {
				time_Txt.text = hours + ":" + minutes.ToString ("00") + ":" + seconds.ToString ("00") + " AM";
			} else {
				time_Txt.text = hours + ":" + minutes.ToString ("00") + ":" + seconds.ToString ("00") + " PM";
			}
			
			if(seconds >= 60) {
				seconds = 0;
				minutes++;
				if(minutes >= 60) {
					minutes = 0;
					hours++;
					if(hours == 12) {
						oldHours += hours;
						isAm = !isAm;
					}
					if(hours == 13){
						hours = 01;
					}
					if(oldHours >= 24) {
						oldHours = 0;
						GameManager.calendarSys.updateDate();
					}
				}
			}
		} else {
			Time.timeScale = 0.0f;
		}
	}

	public bool IsPause {
		get { return _isPause; } 
		set { _isPause = value; }
	}
}
