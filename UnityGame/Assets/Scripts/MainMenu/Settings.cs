using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

	private static float _screenHeight;
	private static float _screenWidth;

	public static float _musicVol;
	public static float _soundVol;

	public static GUISkin _currentSkin;

	public GUISkin _skin;

	public static bool enableFullScreen = true;


	// Use this for initialization
	void Start () {
		_currentSkin = _skin;
		Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
	}
	
	// Update is called once per frame
	void Update () {
		_screenHeight = Screen.height;
		_screenWidth = Screen.width;
		//Debug.Log("Screen Height: " + _screenHeight + " Screen Width: " + _screenWidth);
	}
	
	public static void AutoResize()
	{
		Vector2 resizeRatio = new Vector2((float)Screen.width / _screenWidth, (float)Screen.height / _screenHeight);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}

	public static void setGUISkin() {
		GUI.skin = _currentSkin;
	}

	public static void setVolume(float musicVol, float soundVol) {
		_musicVol = musicVol;
		_soundVol = soundVol;
		Debug.Log("Music Volume: " + _musicVol + " Sound Volume: " + _soundVol);
	}

	public static void setResolution(int width, int height, int refreshRate) {
		Screen.SetResolution(width, height, enableFullScreen, refreshRate);
	}

}
