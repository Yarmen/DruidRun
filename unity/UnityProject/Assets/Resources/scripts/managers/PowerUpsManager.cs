using UnityEngine;
using System.Collections;

public class PowerUpsManager : Singleton<PowerUpsManager> {
	protected PowerUpsManager () {} 
	// Use this for initialization
	GameObject coinObject;
	GameObject allPowerUps;
	
	void Start () {
		print("powerUps manager start");
		coinObject = GameObject.Find ("coin");
		//allPowerUps = new GameObject();
		//allPowerUps.name = "powerUps";
	
	}
	
	
	public void createPowerUpForLevelSector(GameObject levelSector) {
		
		print("create tree");
		float xOffset = -20;
		switch (Random.Range(0,3) ) {
				case 0: xOffset = 4;break;
				case 1: xOffset = 0;break;
				case 2: xOffset = -4;break;
				default: break;
				
				}
		
		GameObject powerUps = new GameObject();
		powerUps.name = "powerUps";
		powerUps.transform.parent = levelSector.transform;
		
		GameObject waypointsParent = (GameObject.Find(levelSector.name+"/waypoints")) as GameObject;
		GameObject[] waypoints = new GameObject[waypointsParent.transform.childCount-1];
		
		int firstPointNumber = Random.Range(0,waypoints.Length-1);
		int secondPointNumber = firstPointNumber+1;
		
		GameObject firstPoint = GameObject.Find(levelSector.name+"/waypoints/"+firstPointNumber) as GameObject;
		GameObject secondPoint = GameObject.Find(levelSector.name+"/waypoints/"+secondPointNumber) as GameObject;
			
		float size = 2*coinObject.renderer.bounds.size.x;
		int coinsCount = Mathf.FloorToInt(Vector3.Distance(firstPoint.transform.position,secondPoint.transform.position)/size);
		Vector3 stepPositionVector = (secondPoint.transform.position-firstPoint.transform.position)/coinsCount;
		Vector3 instantPosition = firstPoint.transform.position;
		
		Vector3 stepRotationVector = (secondPoint.transform.eulerAngles-firstPoint.transform.eulerAngles)/coinsCount;
		Vector3 instantRotation = firstPoint.transform.eulerAngles;
		Vector3 localPosition = new Vector3(xOffset,0,0);
		for (int i=0;i<coinsCount;i++) {
			GameObject coinCopy = GameObject.Instantiate(coinObject) as GameObject;
			coinCopy.transform.parent =  powerUps.transform;
			Quaternion rotation = Quaternion.Euler(instantRotation);
			coinCopy.transform.position = instantPosition+rotation*localPosition;
			instantPosition+=stepPositionVector;
			
			coinCopy.transform.eulerAngles = instantRotation;
			instantRotation+=stepRotationVector;
			
		}
		
			
		
		
	}
}
