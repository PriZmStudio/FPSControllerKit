/*
 * Script to perform basic movements of the First Person Controller (The Player)
 * Done by Rudra Nil Basu, PriZm Studios
*/
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	//falling
	[HideInInspector]
	public float airTime = 0.0f;//total time that player is airborn
	private bool airTimeState = false;
	public float fallingDamageThreshold = 5.5f;//Units that player can fall before taking damage
	private float fallStartLevel;//the y coordinate that the player lost grounding and started to fall
	[HideInInspector]
	public float fallingDistance;//total distance that player has fallen
	private bool falling = false;//true when player is losing altitude
	[HideInInspector]
	public bool grounded = false;// true when the capsule hits the ground
	private bool airTimeState = false;
	//jumping
	//jumping
	public float antiBunnyHopFactor = 0.35f;//to limit the time between player jumps
	[HideInInspector]
	public bool jumping = false;//true when player is jumping
	private float jumpTimer = 0.0f;//track the time player began jump
	private bool jumpfxstate = true;
	private bool jumpBtn = true;//to control jump button behavior
	[HideInInspector]
	public float landStartTime = 0.0f;//time that player landed from jump
	//track player input
	[HideInInspector]
	public float inputXSmoothed = 0.0f;//binary inputs smoothed using lerps
	[HideInInspector]
	public float inputYSmoothed = 0.0f;
	[HideInInspector]
	public int inputX = 0;//1 = button pressed 0 = button released
	[HideInInspector]
	public int inputY = 0;

	private Transform playerTransform; // reference of the transform of the player
	private Rigidbody rb; // reference to the Rigidbody of the player
	[HideInInspector]
	public Vector3 velocity = Vector3.zero;  // velocity component of the player
	[HideInInspector]
	public Transform initialParent; //store original parent for use when unparenting player rigidbody from moving platforms

	private CapsuleCollider capsule; // reference to the capsule collider of the player

	public float backwardSpeedPercentage = 0.6f;//percentage to decrease movement speed while moving backwards
	public float crouchSpeedPercentage = 0.55f;//percentage to decrease movement speed while crouching
	private float crouchSpeedAmt = 1.0f;
	public float strafeSpeedPercentage = 0.8f;//percentage to decrease movement speed while strafing directly left or right
	public float zoomSpeedPercentage = 0.6f;//percentage to decrease movement speed while zooming

	void Awake()
	{
		// Initiallising the RigidBody components of the player
		gameObject.GetComponent<Rigidbody> ().freezeRotation = true;
		gameObject.GetComponent<Rigidbody> ().useGravity = true;
		capsule = gameObject.GetComponent<CapsuleCollider> ();
	}

	void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody> (); // reference of the Player's RigidBody
		playerTransform = transform; // reference of the transform of the player
		initialParent = playerTransform.transform.parent; // storing the original parent on level load
		//clamping the movement modifier percentage
		backwardSpeedPercentage = Mathf.Clamp01(backwardSpeedPercentage);
		crouchSpeedPercentage = Mathf.Clamp01(crouchSpeedPercentage);
		strafeSpeedPercentage = Mathf.Clamp01(strafeSpeedPercentage);
		zoomSpeedPercentage = Mathf.Clamp01(zoomSpeedPercentage);
	}

	void FixedUpdate () 
	{
		RaycastHit hit; //

		//Setting up external script reference
		Player PlayerComponent = gameObject.GetComponent<Player> ();
		CameraLook CameraLookComponent = gameObject.GetComponent<CameraLook>();
		// Setting vertical bounds of the player to detect collision
		Vector3 p1 = playerTransform.position; // bottom part
		Vector3 p2 = p1 + Vector3.up * capsule.height / 2;  // top part  (Vector3.up is simply (0,1,0))
		// storing the velocity of the body
		velocity = rb.velocity;

		// Reading the Player Input from the "Player.cs" script and setting the value of inputX and inputY likewise

		if (Input.GetKey (PlayerComponent.moveForward)){inputY = 1;}
		if (Input.GetKey (PlayerComponent.moveBack)){inputY = -1;}
		if (!Input.GetKey (PlayerComponent.moveBack) && !Input.GetKey (PlayerComponent.moveForward)){inputY = 0;}
		if (Input.GetKey (PlayerComponent.moveBack) && Input.GetKey (PlayerComponent.moveForward)){inputY = 0;}
		if (Input.GetKey (PlayerComponent.strafeLeft)){inputX = -1;}
		if (Input.GetKey (PlayerComponent.strafeRight)){inputX = 1;}
		if (!Input.GetKey (PlayerComponent.strafeLeft) && !Input.GetKey (PlayerComponent.strafeRight)){inputX = 0;}
		if (Input.GetKey (PlayerComponent.strafeLeft) && Input.GetKey (PlayerComponent.strafeRight)){inputX = 0;}

		// smooth the inputs 
		inputXSmoothed = Mathf.Lerp (inputXSmoothed,inputX,Time.deltaTime * 6.0f);
		inputYSmoothed = Mathf.Lerp (inputYSmoothed,inputY,Time.deltaTime * 6.0f);

		// Movements to be performed while on the ground

		if (grounded) 
		{
			//-----------------------------------------
			// Landing
			//-----------------------------------------
			airTimeState=true; // Resetting the airTimeState so that airTime will only be set once when player looses grounding

			if(falling)
			{
				// Reset falling state if the player has landed from a fall
				fallingDistance=0;
				landStartTime=Time.time;
				falling=false;
			}
		}
	}
}
