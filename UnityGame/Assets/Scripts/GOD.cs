using UnityEngine;
using System.Collections;

public class GOD : MonoBehaviour {

	public static bool isLoad = false;

	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start() {

	}

	void Update() {

	}
}
