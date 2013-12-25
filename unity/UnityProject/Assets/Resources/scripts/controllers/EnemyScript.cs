using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	// Use this for initialization
	Rigidbody[] rigids;
	Collider[] colliders;
	Animator animator;
	
	void Start () {
		
		rigids = GetComponentsInChildren< Rigidbody>();
		colliders = GetComponentsInChildren<Collider>();
		makeKinamatic(true);
		
		Animator[] objects = GetComponentsInChildren<Animator>();
		if (objects.Length >0) animator = objects[0];
	
	}
	
	// Update is called once per frame
	
	void makeKinamatic(bool kinematic) {
        foreach(Rigidbody rb  in rigids){
			if (rb==rigidbody) continue;
    		rb.isKinematic = kinematic;
  		}
		
		foreach(Collider col  in colliders){
			if (col==collider || col.isTrigger) continue;
    		col.enabled = !kinematic;
  		}
		
	//	print("recieve message");
    }
	
	
	void swipe(GameObject obj) {
		
		animator.enabled = false;
		 Vector3 direction = obj.transform.position - transform.position;
       // print("recieve message");
		rigidbody.isKinematic = true;
		collider.enabled = false;
		foreach(Collider col  in colliders){
			if (col==collider) continue;
    		col.enabled =true;
  		}
		
       	foreach(Rigidbody rb  in rigids){
		if (rb==rigidbody) continue;	
    	rb.isKinematic = false;
			
			if (rb.tag == "centerBody" ) {
				//print("swipe to center body");
				//rb.AddForceAtPosition(-10*(direction.normalized), transform.position,ForceMode.Impulse);
				rb.AddRelativeForce(new Vector3(1,0,0) * 50,ForceMode.Impulse);
				float x = Random.Range(5,20);
				int action =  Random.Range(0,3);
				/*
				switch (action) {
				case 0: rb.AddRelativeTorque(x,0,0);break;
				case 1: rb.AddRelativeTorque(0,x,0);break;
				case 2: rb.AddRelativeTorque(0,0,x);break;
				default: break;
				
				}
				*/
				
			}
			
			
				
  		}
		
		
    }
	
	
        
	
	
}
