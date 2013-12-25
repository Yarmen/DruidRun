using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	
	public int healthValue;
	private GameObject healthObject;
	private int testHealthChange = 1;
	private GUITexture healthTexture;
	// Use this for initialization
	void Start () {
		
		healthObject = GameObject.Find("GUI/g_health");
		healthTexture = healthObject.GetComponent<GUITexture>();
		
		healthValue = 100;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		 
		if (healthValue>100 || healthValue<0) {
			testHealthChange = -testHealthChange;	
			
		}
		healthValue+=testHealthChange;
		Rect  frame = healthTexture.pixelInset;
		frame.height = healthValue*(Screen.height/100.0f);
		healthTexture.pixelInset = frame;
		
	
	
	}
	
	void OnGUI() {
		int height = Common.Instance.getScreenHeight();
		int width = Common.Instance.getScreenWidth();
		
        if (GUI.Button(new Rect(width-100, 0, 100, 50), "restart"))
            TerrainManager.Instance.restart();
        
    }
	
}
