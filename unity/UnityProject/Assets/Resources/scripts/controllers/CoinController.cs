using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.Rotate(0,0,90);
		
		RaycastHit hit;
        Ray ray = new Ray(transform.position, -Vector3.up*50);
        if (Physics.Raycast(ray, out hit)) {
			
			transform.position  = hit.point+new Vector3(0,gameObject.renderer.bounds.size.y/transform.localScale.x,0);
			//print("hit");
			
		}
               
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(5,0,0);
	
	}
}
