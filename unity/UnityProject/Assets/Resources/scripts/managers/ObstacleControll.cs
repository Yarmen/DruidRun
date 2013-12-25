using UnityEngine;
using System.Collections;

public class ObstacleControll : MonoBehaviour {

	// Use this for initialization
	int nextTimeEnemy = 0;
	int enemyCreationSpeed = 5;
	GameObject samurai;
	GameObject[] enemys;
	GameDelegate gameDelegate;
	
	void Start () {
		
		samurai = GameObject.Find("samurai");
		gameDelegate = Camera.main.GetComponent<GameDelegate>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//print(Time.time);
		if (nextTimeEnemy<Time.time) {
			createTree();
			nextTimeEnemy+=enemyCreationSpeed;
		}
	
	}
	
	void createEnemy() {
		
		print("create enemy");
		float xOffset = -20;
		switch (Random.Range(0,3) ) {
				case 0: xOffset = 4;break;
				case 1: xOffset = 0;break;
				case 2: xOffset = -4;break;
				default: break;
				
				}
		GameObject road = gameDelegate.terrainManager.lastObject;
		GameObject waypointsParent = (GameObject.Find(road.name+"/waypoints")) as GameObject;
		GameObject[] waypoints = new GameObject[waypointsParent.transform.childCount-1];
		
		int randomPointNumber = Random.Range(0,waypoints.Length);
		
		GameObject randomPoint = GameObject.Find(road.name+"/waypoints/"+randomPointNumber) as GameObject;
		
		Vector3 localPosition = new Vector3(xOffset,0,0);
		Vector3 position = randomPoint.transform.position+randomPoint.transform.rotation*localPosition;
		
		GameObject enemy = Instantiate(GameObject.Find("enemy")) as GameObject ;
		enemy.transform.position = position;
		enemy.transform.rotation = randomPoint.transform.rotation;
	}
	
	void createTree() {
		
		print("create tree");
		float xOffset = -20;
		switch (Random.Range(0,3) ) {
				case 0: xOffset = 4;break;
				case 1: xOffset = 0;break;
				case 2: xOffset = -4;break;
				default: break;
				
				}
		GameObject road = gameDelegate.terrainManager.lastObject;
		GameObject waypointsParent = (GameObject.Find(road.name+"/waypoints")) as GameObject;
		GameObject[] waypoints = new GameObject[waypointsParent.transform.childCount-1];
		
		int randomPointNumber = Random.Range(0,waypoints.Length);
		
		GameObject randomPoint = GameObject.Find(road.name+"/waypoints/"+randomPointNumber) as GameObject;
		
		Vector3 localPosition = new Vector3(xOffset,0,0);
		Vector3 position = randomPoint.transform.position+randomPoint.transform.rotation*localPosition;
		
		GameObject obj = Instantiate(GameObject.Find("sycamoresmall")) as GameObject ;
		obj.transform.position = position;
		obj.transform.rotation = randomPoint.transform.rotation;
		
		obj.AddComponent("Rigidbody");
		obj.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
			RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ |
				RigidbodyConstraints.FreezePositionX;
			
		
	}
	
	
	public void createEnemyAtPosition(Vector3 position) {
		
		GameObject enemy = Instantiate(GameObject.Find("enemy")) as GameObject ;
		enemy.transform.position = position;
		
		
	}
}
