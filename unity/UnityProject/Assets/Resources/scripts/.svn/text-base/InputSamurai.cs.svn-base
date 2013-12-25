using UnityEngine;
using System.Collections;

public class InputSamurai : MonoBehaviour {
	
	private float verticalSpeed = 0.0f;
	private float moveSpeed = 7;
	private float inputSpeed = 5;
	private float jumpValue = 10;
	private Vector3 moveDirection = Vector3.zero;
	private bool isControllable = true;
	private Vector3 inAirVelocity =  Vector3.zero;
	private Vector3 touchCenter =  Vector3.zero;
	CollisionFlags collisionFlags;
	private bool isJump = false;
	
	static int walkState  = Animator.StringToHash("Base Layer.walk");
	static int jumpState  = Animator.StringToHash("Base Layer.jumpFly");
	static int kesagiri  = Animator.StringToHash("Base Layer.kesagiri");
	// Use this for initialization
	void Start () {
		
		Camera.main.transform.parent = gameObject.transform;
		Vector3 localPos = Camera.main.transform.localPosition;
		localPos.y = 5*8;
		localPos.z = -5*8;
		localPos.x = 0;
		Vector3 localAngles = Camera.main.transform.localEulerAngles;
		localAngles.x = 30;
		Camera.main.transform.localPosition = localPos;
		Camera.main.transform.localEulerAngles = localAngles;
	
	}
	
	void ApplyGravity ()
	{
		if (isControllable)	// don't move player at all if not controllable.
		{
		// Apply gravity
			
			
			
			if (IsGrounded ()) {
			verticalSpeed = 0.0f;	
				
			}
			else
				verticalSpeed += Physics.gravity.y * Time.deltaTime;
			}
		
		if (isJump) {
				 verticalSpeed = jumpValue;
				isJump = false;
			} 
	}
	
	bool IsGrounded () {
	return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
	}



	// Update is called once per frame
	void Update () {
		
		iphoneInput();
		UnityInput();

	}
	
	void FixedUpdate () {
		
		//return;
		ApplyGravity ();
		float h = Input.GetAxisRaw("Horizontal");
		
		//moveDirection =new Vector3(h,0,1);
		moveDirection = moveDirection.normalized;
		Vector3 movement =new Vector3(0,0,moveSpeed)+ moveDirection * inputSpeed + new Vector3 (0, verticalSpeed, 0) + inAirVelocity;
		movement *= Time.deltaTime;
	
		// Move the controller
		CharacterController controller = GetComponent<CharacterController>();
		collisionFlags = controller.Move(movement);
		//print(isJump);

	}

	
	
	void iphoneInput() {
		moveDirection = Vector3.zero;
		 if (Input.touchCount > 0 ) {
		 	
			
		 	if (Input.GetTouch(0).phase == TouchPhase.Began) {
        
            // Get movement of the finger since last frame
            touchCenter =Input.GetTouch(0).position;
            
            }	
			
			if (Input.GetTouch(0).phase == TouchPhase.Stationary) {
        
            // Get movement of the finger since last frame
            touchCenter =Input.GetTouch(0).position;
            
            }	

				
			 if (Input.GetTouch(0).phase == TouchPhase.Moved) {
        
            // Get movement of the finger since last frame
            Vector3 movement =  new Vector3(Input.GetTouch(0).deltaPosition.x,0,0);
            moveDirection = movement ; 
            
             GUIText text = GameObject.Find("console").GetComponent<GUIText>();
			text.text = ""+movement;

            
            }	
			
			if (Input.GetTouch(0).phase == TouchPhase.Ended) {
			
			
			}
		 	
		 	
		 	
		 	
		 } else {
		 	
		 	
		 }
		 	
	}
	
	void UnityInput() {
		int translation = 0;
		int acceleration = 0;
		if (Input.GetKey("d")) translation++;
		if (Input.GetKey("a")) translation--;
		if (Input.GetKey("w")) moveSpeed=20; else moveSpeed = 7;
		//		print(translation);
		moveDirection =  new Vector3(translation,0,0);
        //    moveDirection = movement ; 
		//if (Input.GetKey("space"))  jump();
		Animator animator = GetComponent<Animator>() as Animator;
		AnimatorStateInfo cureentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		
		if (cureentBaseState.nameHash == walkState) {
			
			if (Input.GetKey("space")) {
			animator.SetBool("jump",true);
			isJump = true;
			//print("jump");	
			}
		} else if (cureentBaseState.nameHash == jumpState) {
			
			if (IsGrounded() ) {
				animator.SetBool("jump",false);
			}
			
		} else if (cureentBaseState.nameHash == kesagiri) {
			animator.SetBool("swipe",false);
		}
		
	}
	
	
	void jump() {
	
		if (IsGrounded () == false) return;
		
		
		
	}
	
	void OnTriggerEnter(Collider other) {
        print("collision");
		
		
		if (other.tag == "tree") {
		print ("death");	
			return;
		}
		Animator animator = GetComponent<Animator>() as Animator;
		AnimatorStateInfo cureentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		if (cureentBaseState.nameHash == walkState) {
			animator.SetBool("swipe",true);
			other.gameObject.SendMessage("swipe",gameObject,SendMessageOptions.DontRequireReceiver);
		}
    }
}
