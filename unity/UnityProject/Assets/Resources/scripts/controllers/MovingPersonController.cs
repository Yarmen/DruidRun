using UnityEngine;
using System.Collections;

public class MovingPersonController : MonoBehaviour {

	// Use this for initialization
	public int moveSpeed = 10;
	public Vector3 nextPosition;
	public Vector3 nextRotation;
	
	public float timeCurrent = 1 ;
	private float timeDelta = 0;
	private Vector3 currentPosition;
	private Vector3 currentRotation;
	public GameObject[] waypoints;
	
	private Vector3 waypointPos0;
	private Vector3 waypointPos1;
	private Vector3 waypointPos2;
	private Vector3 waypointPos3;
	
	private Vector3 waypointRot0;
	private Vector3 waypointRot1;
	private Vector3 waypointRot2;
	private Vector3 waypointRot3;
	
	public bool move = false;
	
	private Vector3 moveDirection = Vector3.zero;
	private float verticalSpeed = 0.0f;
	private float jumpValue = 2;
	
	private int roadWidth = 6;
	public GameObject movingPerson;	
	//GameObject testObject;
	CollisionFlags collisionFlags;
	private GameDelegate gameDelegate;
	
	
	static int runState  = Animator.StringToHash("Base Layer.run");
	static int jumpState  = Animator.StringToHash("Base Layer.jump");
	
	Animator animator;
	AnimatorStateInfo cureentBaseState;
	
	float jumpStartSpeed;
	
	
	
	void Start () {
		
		if (movingPerson == null) {
			
			movingPerson = GameObject.Find("MainCharacter");
			transformDruid();
		}
		
		Camera.main.transform.parent = gameObject.transform;
		Vector3 localPos = Camera.main.transform.localPosition;
		localPos.y = 5/gameObject.transform.localScale.y;
		localPos.z = -5/gameObject.transform.localScale.y;
		localPos.x = 0;
		Vector3 localAngles = Camera.main.transform.localEulerAngles;
		localAngles.x = 25;
		Camera.main.transform.localPosition = localPos;
		Camera.main.transform.localEulerAngles = localAngles;
		
		gameDelegate = Camera.main.GetComponent<GameDelegate>();
		
		//testObject = GameObject.CreatePrimitive(PrimitiveType.Cube) as  GameObject ;
		//testObject.collider.enabled = false;
	
	}
	
	public void restart() {
		
		timeCurrent = 1;
		waypoints = null;
		move = false;
		animator.SetBool("death",false);
		transformDruid();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!move) return;
		
		float normalizedTime;
		normalizedTime =0.5f*(1-verticalSpeed/jumpStartSpeed);
			
		cureentBaseState = animator.GetCurrentAnimatorStateInfo(0);
		
		if (cureentBaseState.nameHash == jumpState && normalizedTime<0.74f ) {
			animator.SetBool("jump",false);
			animator.ForceStateNormalizedTime(normalizedTime);
			//print(normalizedTime);
			
		}
		
		 if ((collisionFlags & CollisionFlags.Sides)!=0) {
			
			death();
		}
           
		
		
		moveCenter();
		moveCharacter();

	
	}
	
	void moveCharacter() {
		
		ApplyGravity ();
		float h = Input.GetAxisRaw("Horizontal");
		
		
		
		Vector3 movingVector =nextPosition+transform.rotation*moveDirection - transform.position;
		Vector3 rotVector =new Vector3(movingVector.x,0,movingVector.z);
		Quaternion rotation = Quaternion.LookRotation(rotVector);
		transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.fixedDeltaTime);
		
		Vector3 movement =new Vector3(movingVector.x,0,movingVector.z)+ new Vector3 (0, verticalSpeed, 0);
		
		// Move the controller
		CharacterController controller = movingPerson.GetComponent<CharacterController>();
		collisionFlags = controller.Move(movement);
		
		
	}
	
	bool IsGrounded () {
	return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
	}
	
	void ApplyGravity ()
	{
		if (!IsGrounded() )
		verticalSpeed += 0.5f*Physics.gravity.y * Time.fixedDeltaTime;
		
		
		
	} 

	void moveCenter () {
	
		
		if (timeCurrent>=1)
		{
				
			timeCurrent-=1;

			GameObject[] newWaypoints = new GameObject[waypoints.Length-1];
			for (int i = 0;i<newWaypoints.Length;i++) {
				newWaypoints[i] = waypoints[i+1];
				
			}
			waypoints = newWaypoints;
			
			waypointPos0 = waypoints[0].transform.position;
			waypointPos1 = waypoints[1].transform.position;
			waypointPos2 = waypoints[2].transform.position;
			waypointPos3 = waypoints[3].transform.position;
			
			waypointRot0 = waypoints[0].transform.eulerAngles;
			waypointRot1 = waypoints[1].transform.eulerAngles;
			waypointRot2 = waypoints[2].transform.eulerAngles;
			waypointRot3 = waypoints[3].transform.eulerAngles;
	
		}
		
		timeDelta=0.1f*moveSpeed/Vector3.Distance(waypointPos1,waypointPos2);
		timeCurrent+=timeDelta;
		//print(timeCurrent);
		
			
		currentPosition =(  	(2 * waypointPos1) +
		
		 (-waypointPos0 + waypointPos2) * timeCurrent +
		 
		(2*waypointPos0 - 5*waypointPos1 + 4*waypointPos2 - waypointPos3) * timeCurrent*timeCurrent +
		  
		(-waypointPos0 + 3*waypointPos1 - 3*waypointPos2 + waypointPos3) * timeCurrent*timeCurrent*timeCurrent);
		
		currentPosition/= 2;

		nextPosition = new Vector3(currentPosition.x,currentPosition.y,currentPosition.z);
		
		currentRotation =(  	(2 * waypointRot1) +
		
		 (-waypointRot0 + waypointRot2) * timeCurrent +
		 
		(2*waypointRot0 - 5*waypointRot1 + 4*waypointRot2 - waypointRot3) * timeCurrent*timeCurrent +
		  
		(-waypointRot0 + 3*waypointRot1 - 3*waypointRot2 + waypointRot3) * timeCurrent*timeCurrent*timeCurrent);
		
		currentRotation/= 2;
		
		nextRotation = currentRotation;
		
		//testObject.transform.position = nextPosition;
	}
	
	void setNewWaypoints(GameObject[] arivedWaypoints) {
		
		//print("new waypoints arrived");
		
		if (waypoints ==null )  waypoints = new GameObject[0];
		GameObject[] newWaypoints = new GameObject[waypoints.Length+arivedWaypoints.Length];
		
			for (int i = 0;i<waypoints.Length;i++) {
				newWaypoints[i] = waypoints[i];
				
			}
			
			int lastindex =  waypoints.Length;
			for (int i = 0;i<arivedWaypoints.Length;i++) {
				newWaypoints[lastindex+i] = arivedWaypoints[i];
				
			}
		
			waypoints = newWaypoints;
			//print(waypoints.Length);
		
	}
	
	//Public Methods for character  controll
	
	public void changePosition(float translation) {
		moveDirection =  new Vector3(moveDirection.x+translation,0,0);
		moveDirection = new Vector3(Mathf.Clamp(moveDirection.x,-roadWidth*0.5f,roadWidth*0.5f),0,0);
		
	}
	
	public void jump() {
		
		if (IsGrounded ()) {
			
			verticalSpeed = jumpValue;
			animator.SetBool("jump",true);
			jumpStartSpeed = verticalSpeed;
		}
		
		
		
	}
	
	public void transformDruid() {
		CharacterController charControl = movingPerson.GetComponent<CharacterController>();
		GameObject druid = GameObject.Find("druidWoman");
		GameObject cat = GameObject.Find("cat");
		GameObject bear = GameObject.Find("bear");
		
		if (druid.transform.parent == movingPerson) return;
		
		cat.transform.parent = null;
		cat.transform.position = Vector3.zero;
		
		bear.transform.parent = null;
		bear.transform.position = Vector3.zero;
		
		druid.transform.parent = movingPerson.transform;
		druid.transform.localPosition = Vector3.zero;
		druid.transform.localEulerAngles = Vector3.zero;
		animator = druid.GetComponent<Animator>() as Animator;
		
		charControl.height = 1;
		charControl.radius = 1;
		Vector3 center = charControl.center;
		center = new Vector3(0,1,0);
		charControl.center = center;
		
		moveSpeed = 8;
		jumpValue = 1.5f;
		
		//GameObject.Find(""
		
		
	}
	
	public void transformCat() {
		CharacterController charControl = movingPerson.GetComponent<CharacterController>();
		GameObject druid = GameObject.Find("druidWoman");
		GameObject cat = GameObject.Find("cat");
		GameObject bear = GameObject.Find("bear");
		
		if (cat.transform.parent == movingPerson) return;
		
		druid.transform.parent = null;
		druid.transform.position = Vector3.zero;
		
		bear.transform.parent = null;
		bear.transform.position = Vector3.zero;
		
		cat.transform.parent = movingPerson.transform;
		cat.transform.localPosition = Vector3.zero;
		cat.transform.localEulerAngles = Vector3.zero;
		
		animator = cat.GetComponent<Animator>() as Animator;
		
		charControl.height = 1;
		charControl.radius = 1;
		Vector3 center = charControl.center;
		center = new Vector3(0,1,0);
		charControl.center = center;
		moveSpeed = 12;
		jumpValue = 2;
		//GameObject.Find(""
		
		
	}
	
	public void transformBear() {
		CharacterController charControl = movingPerson.GetComponent<CharacterController>();
		GameObject druid = GameObject.Find("druidWoman");
		GameObject cat = GameObject.Find("cat");
		GameObject bear = GameObject.Find("bear");
		
		if (cat.transform.parent == movingPerson) return;
		
		druid.transform.parent = null;
		druid.transform.position = Vector3.zero;
		
		cat.transform.parent = null;
		cat.transform.position = Vector3.zero;
		
		bear.transform.parent = movingPerson.transform;
		bear.transform.localPosition = Vector3.zero;
		bear.transform.localEulerAngles = Vector3.zero;
		
		animator = bear.GetComponent<Animator>() as Animator;
		
		charControl.height = 1;
		charControl.radius = 1;
		Vector3 center = charControl.center;
		center = new Vector3(0,1,0);
		charControl.center = center;
		moveSpeed = 8;
		jumpValue = 1;
		//GameObject.Find(""
		
		
	}
	
	
	void OnTriggerEnter(Collider other) {
        print("collision");
		
		
		if (other.tag == "pit") {
			death();
		}
		
    }
	
	void death() {
		
		print ("death");	
		move = false;
		animator.SetBool("death",true);
			//print(normalizedTime);
		
	}
	
}
