using UnityEngine;
using System.Collections;

public class InputSamurai : MonoBehaviour {
	
	private float verticalSpeed = 0.0f;
	private float moveSpeed = 10;
	private float inputSpeed = 5;
	private float jumpValue = 2;
	private Vector3 moveDirection = Vector3.zero;
	private bool isControllable = true;
	private Vector3 inAirVelocity =  Vector3.zero;
	private Vector3 touchCenter =  Vector3.zero;
	CollisionFlags collisionFlags;
	private bool isJump = false;
	private int roadWidth = 8;
	
	static int runState  = Animator.StringToHash("Base Layer.run");
	static int jumpStartState  = Animator.StringToHash("Base Layer.jumpStart");
	static int jumpProcessState  = Animator.StringToHash("Base Layer.jumpProcess");
	static int kesagiri  = Animator.StringToHash("Base Layer.attack");
	
	private MovingPersonController movingPersonController;
	
	private GameDelegate gameDelegate;
	
	GameObject attackTarget;
	// Use this for initialization
	void Start () {
		
		movingPersonController = gameObject.GetComponent<MovingPersonController>();
		Camera.main.transform.parent = gameObject.transform;
		Vector3 localPos = Camera.main.transform.localPosition;
		localPos.y = 10/gameObject.transform.localScale.y;
		localPos.z = -10/gameObject.transform.localScale.y;
		localPos.x = 0;
		Vector3 localAngles = Camera.main.transform.localEulerAngles;
		localAngles.x = 30;
		Camera.main.transform.localPosition = localPos;
		Camera.main.transform.localEulerAngles = localAngles;
		
		gameDelegate = Camera.main.GetComponent<GameDelegate>();
		
	
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
				verticalSpeed += 0.5f*Physics.gravity.y * Time.fixedDeltaTime;
			}
		
		if (isJump) {
				 verticalSpeed = jumpValue;
				isJump = false;
				print("jumping");
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
		//print(moveDirection);
		//moveDirection =new Vector3(10,0,0);
		//moveDirection = moveDirection.normalized;
		Vector3 movingVector =movingPersonController.nextPosition+transform.rotation*moveDirection - transform.position;
		Vector3 rotVector =new Vector3(movingVector.x,0,movingVector.z);
		Quaternion rotation = Quaternion.LookRotation(rotVector);
		transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.deltaTime);
		
		Vector3 movement =new Vector3(movingVector.x,0,movingVector.z)+ new Vector3 (0, verticalSpeed, 0);
		//movement *= Time.fixedDeltaTime;
		//transform.position =movingPersonController.nextPosition;
		//transform.eulerAngles =movingPersonController.nextRotation;
		
		// Move the controller
		CharacterController controller = GetComponent<CharacterController>();
		collisionFlags = controller.Move(movement);
		//print(isJump);

	}

	
	
	void iphoneInput() {
		//moveDirection = Vector3.zero;
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
		float translation = 0;
		int acceleration = 0;
		bool strike = false;
		if (Input.GetKey("d")) translation+=.1f;
		if (Input.GetKey("a")) translation-=.1f;
		if (Input.GetKey("w")) moveSpeed=40; else moveSpeed = 20;
		if (Input.GetKey("e")) strike = true;
		//		print(translation);
		moveDirection =  new Vector3(moveDirection.x+translation,0,0);
		moveDirection = new Vector3(Mathf.Clamp(moveDirection.x,-roadWidth*0.5f,roadWidth*0.5f),0,0);
        //    moveDirection = movement ; 
		//if (Input.GetKey("space"))  jump();
		Animator animator = GetComponent<Animator>() as Animator;
		AnimatorStateInfo cureentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		
		if (cureentBaseState.nameHash == runState ) {
			
			

			
			if (Input.GetKey("space") && isJump==false && IsGrounded ()) {
			animator.SetBool("jump",true);
			isJump = true;
			print("jump");	
			} else {
				if (strike) {
					animator.SetBool("swipe",true);
					print("strike");	
					
				}	
			}
		} else if (cureentBaseState.nameHash == jumpProcessState) {
			
			if (IsGrounded() ) {
				animator.SetBool("jump",false);
			}
			
		} else if (cureentBaseState.nameHash == kesagiri ) {
			animator.SetBool("swipe",false);
			
			if (cureentBaseState.normalizedTime >0.6f ) {
				attackTarget.SendMessageUpwards("swipe",gameObject,SendMessageOptions.DontRequireReceiver);
			}
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
		if (cureentBaseState.nameHash == runState) {
			animator.SetBool("swipe",true);
			attackTarget = other.gameObject;
		}
    }
	
	
}
