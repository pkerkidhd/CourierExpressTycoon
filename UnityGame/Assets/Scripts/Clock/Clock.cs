using UnityEngine;
using System.Collections;
using System;

public class Clock : MonoBehaviour {

	public GUIText time_Txt;

	private float seconds = 0;
	private float minutes = 00;
	private float hours = 01;
	private float oldHours = 00;

	private bool isAm = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		seconds += 0.02f * Time.timeScale;

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
					//New day
				}
			}
		}
		//Time.fixedDeltaTime = 0.02f * Time.timeScale;
		if (Input.GetMouseButtonDown (0)) {
			//Time.timeScale = 100.0f;
		} else if (Input.GetMouseButtonDown (1)) {
			//Time.timeScale = 0.5f;			
		}
	}	
}
