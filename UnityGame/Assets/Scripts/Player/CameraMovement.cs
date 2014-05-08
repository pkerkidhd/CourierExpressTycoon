using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public int boundary = 20;
	public int speed = 15;
	
	public int theScreenWidth;
	public int theScreenHeight;
	
	public float xPos;
	public float yPos;
	public float zPos;
	
	// Use this for initialization
	void Start () {
		theScreenWidth = Screen.width;
		theScreenHeight = Screen.height;
		
		xPos = transform.position.x;
		yPos = transform.position.y;
		zPos = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = new Vector3(xPos, yPos, zPos);
		
		if (Input.GetMouseButtonDown(0)) {
		//	Debug.Log("Mouse pos X: " + Input.mousePosition.x + " Y: " + Input.mousePosition.y + " Z: " + Input.mousePosition.z);
		//	Debug.Log("Camera pos X: " + transform.position.x + " Y: " + transform.position.y + " Z: " + transform.position.z);
		}
		
		if (Input.GetAxis("Mouse ScrollWheel") > 0) {
			if (transform.position.y > 4) {
				yPos -= 10.0f * Time.fixedDeltaTime;
				//Debug.Log("Y: " + transform.position.y);
			}
		}
		
		if (Input.GetAxis("Mouse ScrollWheel") < 0) {
			if (transform.position.y < 9) {
				yPos += 10.0f * Time.fixedDeltaTime;
				//Debug.Log("Y: " + transform.position.y);
			}
		}
		
		if (Input.GetKey(KeyCode.Space)) {
			//yPos = 36.41333f;
		}
		
		if (Input.mousePosition.x > theScreenWidth - boundary) {
		//	if (transform.position.x < 188) {
			xPos += speed * Time.fixedDeltaTime;
				//Debug.Log("X: " + transform.position.x);
		//	}
		}
		
		if (Input.mousePosition.x < 0 + boundary)
		{
		//	if (transform.position.x > 9.5) {
			xPos -= speed * Time.fixedDeltaTime;
				//Debug.Log("X: " + transform.position.x);
		//	}
		} 
		
		if (Input.mousePosition.y > theScreenHeight - boundary)
		{
		//	if (transform.position.z < 84) {
			zPos += speed * Time.fixedDeltaTime;
				//Debug.Log("Z: " + transform.position.z);
		//	}
		}
		
		if (Input.mousePosition.y < 0 + boundary)
		{
		//	if (transform.position.z > 11) {
			zPos -= speed * Time.fixedDeltaTime;
				//Debug.Log("Z: " + transform.position.z);
		//	}
		}
		
	}
}
