using UnityEngine;
using System.Collections;

public class BuildingPlacement : MonoBehaviour {

	private PlaceableBuilding placeableBuilding;
	private Transform currentBuilding;
	private bool hasPlaced;
	private PlaceableBuilding placeableBuildingOld;

	public LayerMask buildingsMask;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 m = Input.mousePosition;
		m = new Vector3(m.x, m.y, transform.position.y);
		Vector3 p = camera.ScreenToWorldPoint(m);

		if (currentBuilding != null && !hasPlaced) {
			currentBuilding.position = new Vector3(p.x, 0.2f, p.z);

			if (Input.GetMouseButtonDown(0)) {
				if(IsLeagalPosition()) {
					hasPlaced = true;
				}
			}
		} else {
			if (Input.GetMouseButtonDown(0)) {
				RaycastHit hit = new RaycastHit();
				Ray ray = new Ray(new Vector3(p.x, 9, p.z), Vector3.down);
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingsMask)) {
					if(placeableBuildingOld != null) {
						placeableBuildingOld.SetSelected(false);
					}
					hit.collider.gameObject.GetComponent<PlaceableBuilding>().SetSelected(true);
					placeableBuildingOld = hit.collider.gameObject.GetComponent<PlaceableBuilding>();
				} else {
					if(placeableBuildingOld != null) {
						placeableBuildingOld.SetSelected(false);
					}
				}
			}
		}
	}

	bool IsLeagalPosition() {
		if (placeableBuilding.colliders.Count > 0) {
			return false;
		}
		return true;
	}

	public void SetItem(GameObject b) {
		hasPlaced = false;
		currentBuilding = ((GameObject)Instantiate(b)).transform;
		placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
	}
}
