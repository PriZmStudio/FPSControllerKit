using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	//other objects accessed by this script
	[HideInInspector]
	public GameObject weaponCameraObj;
	[HideInInspector]
	public GameObject weaponObj;
	[HideInInspector]
	public GameObject painFadeObj;
	[HideInInspector]
	public GameObject levelLoadFadeObj;
	[HideInInspector]
	public GameObject healthGuiObj;//this object is instantiated for heath display on hud
	[HideInInspector]
	public GameObject healthGuiObjInstance;
	[HideInInspector]
	public GameObject helpGuiObj;//this object is instantiated for help text display
	[HideInInspector]
	public GameObject helpGuiObjInstance;
	[HideInInspector]
	public GameObject PickUpGuiObj;//this object is instantiated for hand pick up crosshair on hud
	[HideInInspector]
	public GameObject PickUpGuiObjGuiObjInstance;
	[HideInInspector]
	public GameObject CrosshairGuiObj;//this object is instantiated for aiming reticle on hud
	[HideInInspector]
	public GameObject CrosshairGuiObjInstance;
	[HideInInspector]
	public Projector shadow;//to access the player shadow projector 
	private AudioSource[]aSources;//access the audio sources attatched to this object as an array for playing player sound effects
	
	//player hit points
	public float hitPoints = 100.0f;
	public float maximumHitPoints = 200.0f;
	
	//Damage feedback
	private float gotHitTimer = -1.0f;
	public Color PainColor = new Color(0.75f, 0f, 0f, 0.5f);//color of pain screen flash can be selected in editor
	
	//crosshair 
	public bool crosshairEnabled = true;//enable or disable the aiming reticle
	private bool crosshairVisibleState = true;
	private bool crosshairTextureState = false;
	public Texture2D Reticle;//the texture used for the aiming crosshair
	public Texture2D Hand;//the texture used for the pick up crosshair
	private Color handColor = Color.white; 
	private Color reticleColor = Color.white; 
	[HideInInspector]
	public LayerMask rayMask = 0;//only layers to include for crosshair raycast in hit detection (for efficiency)
	
	//button and behavior states
	private bool pickUpBtnState = true;
	[HideInInspector]
	public bool restarting = false;//to notify other scripts that level is restarting
	
	//zooming
	private bool zoomBtnState = true;
	private float zoomStopTime = 0.0f;//track time that zoom stopped to delay making aim reticle visible again
	[HideInInspector]
	public bool zoomed = false;
	private float zoomStart = -2.0f;
	private bool zoomStartState = false;
	private float zoomEnd = 0.0f;
	private bool zoomEndState = false;
	
	//sound effects
	public AudioClip painLittle;
	public AudioClip painBig;
	public AudioClip die;

	[Header("Movement Settings:")]
	//player controls set in the inspector
	public KeyCode moveForward;
	public KeyCode moveBack;
	public KeyCode strafeLeft;
	public KeyCode strafeRight;
	public KeyCode jump;
	public KeyCode crouch;
	public KeyCode sprint;
	[Header("Weapon Settings:")]
	public KeyCode fire;
	public KeyCode zoom;
	public KeyCode reload;
	public KeyCode fireMode;
	public KeyCode holsterWeapon;
	public KeyCode selectNextWeapon;
	public KeyCode selectPreviousWeapon;
	public KeyCode selectWeapon1;
	public KeyCode selectWeapon2;
	public KeyCode selectWeapon3;
	public KeyCode selectWeapon4;
	public KeyCode selectWeapon5;
	public KeyCode selectWeapon6;
	public KeyCode selectWeapon7;
	public KeyCode selectWeapon8;
	public KeyCode selectWeapon9;
	public KeyCode selectWeapon10;
	[Header("Misc Settings:")]
	public KeyCode use;
	public KeyCode moveObject;
	public KeyCode throwObject;
	public KeyCode showHelp;
	public KeyCode restartScene;
	public KeyCode exitGame;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
