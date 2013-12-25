using UnityEngine;
using System.Collections;

public class UnityInputManager : Singleton<UnityInputManager> {
	
	protected UnityInputManager () {}

	// Use this for initialization
	private GameDelegate gameDelegate;
	private MovingPersonController  mainCharacterController;
	void Start () {
		
		gameDelegate =Camera.main.gameObject.GetComponent<GameDelegate>();
		mainCharacterController = gameDelegate.mainCharacter.GetComponent<MovingPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Input.GetKey("d")) mainCharacterController.changePosition(0.3f);
		if (Input.GetKey("a")) mainCharacterController.changePosition(-0.3f);
		//if (Input.GetKey("e")) strike = true;
		
		if (Input.GetKey("space")) mainCharacterController.jump();
		if (Input.GetKey("1")) mainCharacterController.transformDruid();
		if (Input.GetKey("2")) mainCharacterController.transformCat();
		if (Input.GetKey("3")) mainCharacterController.transformBear();
		
		
		
	
	}
}
