using UnityEngine;
using System.Collections;



public class GameDelegate : MonoBehaviour {
	
	
	public GameObject console;
	
	public ObstacleControll obstacleControll;
	public TerrainManager terrainManager;
	public UnityInputManager unityInputManager;
	public Common common;
	public GameObject mainCharacter;
	
	
	// Use this for initialization
	void Start () {
		
		console = GameObject.Find("console");
		TerrainManager.Instance.init();
		UnityInputManager.Instance.init();
		PowerUpsManager.Instance.init();
		//terrainManager = gameObject.AddComponent("TerrainManager") as TerrainManager;
		//unityInputManager = gameObject.AddComponent("UnityInputManager") as UnityInputManager;
		//common = gameObject.AddComponent("Common") as Common;
		//obstacleControll = gameObject.AddComponent("ObstacleControll") as ObstacleControll;
		
		// GameObject person =Instantiate (Resources.Load("objects/soccerMan")) as GameObject;
		//SkinnedMeshRenderer render = person.GetComponentInChildren<SkinnedMeshRenderer>();
		//person.transform.position = new Vector3(0, render.bounds.extents.y*10, 0);
		
		
	}
	

	
	
	
	
}
