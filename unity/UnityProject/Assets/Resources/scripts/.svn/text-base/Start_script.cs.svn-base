using UnityEngine;
using System.Collections;



public class Start_script : MonoBehaviour {
	
	float lastPoint = 0;
	GameObject lastObject;
	GameObject samurai;
	public GameObject console;
	private int id = 0;
	ObstacleControll obstacleControll;
	
	// Use this for initialization
	void Start () {
		
		console = GameObject.Find("console");
		obstacleControll = Camera.main.GetComponent<ObstacleControll>();
		for(int i = 0;i<5;i++) {
			
			createTerrainObject();			
			
		}
		
		
		
		// GameObject person =Instantiate (Resources.Load("objects/soccerMan")) as GameObject;
		//SkinnedMeshRenderer render = person.GetComponentInChildren<SkinnedMeshRenderer>();
		//person.transform.position = new Vector3(0, render.bounds.extents.y*10, 0);
		GameObject person = GameObject.Find ("samurai");
		person.transform.position = new Vector3(0, 20, 0);

		samurai = person;
		
	}
	
	void createTerrainObject(){
		
		GameObject road = Instantiate(GameObject.Find("road&tree")) as GameObject ;
		road.name = "road"+id;
		GameObject meshObject = (GameObject.Find(road.name+"/mesh")) as GameObject ;
		road.transform.position = new Vector3(0, 0, lastPoint);
			// go.renderer.material = Resources.Load("materials/terrain") as Material;
		Mesh mesh = meshObject.GetComponent<MeshFilter>().mesh;
		lastPoint += road.transform.position.x+mesh.bounds.size.y*road.transform.localScale.y;
		lastObject = road;
		id++;
		float x = Random.Range(0,30);
		/*
		int direction =  Random.Range(0,2);
			
				switch (direction) {
				case 0: lastObject.transform.eulerAngles = new Vector3(x,0,0);break;
				case 1: lastObject.transform.eulerAngles = new Vector3(-x,0,0);break;
				default: break;
		}
		*/
		
		
		if (obstacleControll !=null) obstacleControll.createEnemyAtPosition(lastObject.transform.position+new Vector3(0,8.55f,0));
		//print(mesh.bounds.size.x);

		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		float distance = Vector3.Distance(samurai.transform.position,lastObject.transform.position);
//		GUIText text = console.GetComponent<GUIText>();
//		text.text = ""+distance;
		
		
		if (distance<30) createTerrainObject();
		//if (Vector3.Distance(samur
		
	
	}
	
	
	
}
