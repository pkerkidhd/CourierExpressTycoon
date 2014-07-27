using UnityEngine;
using System.Collections;

public class UpdateSytem : MonoBehaviour {

	private int _speedStatus;

	// Use this for initialization
	void Start () {
		_speedStatus = 0;
	}
	
	// Update is called once per frame
	void Update () {

//		if(Input.GetMouseButtonDown(0))
//		{
//			//Time.timeScale = 100.0f;
//			_speedStatus = 1;
//		} else if(Input.GetMouseButtonDown(1)) {
//			_speedStatus = 2;
//		} else if(Input.GetMouseButtonDown(2)) {
//			_speedStatus = 3;
//		}
	}

	public int SpeedStatus {
		get { return _speedStatus; } 
		set { _speedStatus = value; }
	}
}
