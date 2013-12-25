using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	// Use this for initialization
	 Rigidbody[] rigids;
	
	void Start () {
		
		rigids = GetComponentsInChildren< Rigidbody>();
		makeKinamatic(true);
	
	}
	
	// Update is called once per frame
	
	void makeKinamatic(bool kinematic) {
        foreach(Rigidbody rb  in rigids){
    	rb.isKinematic = kinematic;
  		}
		
	//	print("recieve message");
    }
	
	
	void swipe(GameObject obj) {
		
		 Vector3 direction = obj.transform.position - transform.position;
        print("recieve message");
		
       	foreach(Rigidbody rb  in rigids){
    	rb.isKinematic = false;
			rb.AddForceAtPosition(-30*(direction.normalized+Vector3.up), transform.position,ForceMode.Impulse);
			if (rb.tag == "centerBody" ) {

				float x = Random.Range(5,20)*20;
				int action =  Random.Range(0,3);
			
				switch (action) {
				case 0: rb.AddRelativeTorque(x,0,0);break;
				case 1: rb.AddRelativeTorque(0,x,0);break;
				case 2: rb.AddRelativeTorque(0,0,x);break;
				default: break;
				
				}
				
			}
			
			
				
  		}
		
		
    }
	
	
        
	
	
}
