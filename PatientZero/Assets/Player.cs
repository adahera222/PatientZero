using UnityEngine;
using System.Collections;
using System.Timers;

public class Player : MonoBehaviour {

	public float Speed = 6.0F;
    public float JumpSpeed = 10.0F;
    public float Gravity = 20.0F;
	public bool AllowAirControl = true;
	
    private Vector3 moveDirection = Vector3.zero;
	private Vector2 startLocation;
	
	private LineRenderer line;
	private float initialGravity = 0;
	
	private Timer delayGravity;
	CharacterController controller;
	public GameObject BrokenUpPrefab;
	private GameObject brokenUpPrefab;
	
	void Awake()
	{
		line = GetComponent<LineRenderer>();
		delayGravity = new Timer();
		delayGravity.Elapsed+=new ElapsedEventHandler(GravityDelay);
        delayGravity.Interval=500;
		initialGravity = Gravity;
		controller = GetComponent<CharacterController>();
	}
	
    void Update()
	{
		HandleBasicMovement();
		StartCoroutine(ShowLine());
		
		if(Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.LeftShift))
		{
			Vector3 end = Camera.mainCamera.ScreenToWorldPoint(Input.mousePosition);
			transform.position = new Vector3(end.x,end.y,0);
			delayGravity.Enabled = true;
			gameObject.renderer.enabled = true;
			Gravity = 0;
		}
		else if(Input.GetKey(KeyCode.LeftShift))
		{
			gameObject.renderer.enabled = false;
			if(brokenUpPrefab == null)
				brokenUpPrefab = (GameObject)Instantiate(BrokenUpPrefab,gameObject.transform.position,gameObject.transform.rotation);
			brokenUpPrefab.transform.parent = gameObject.transform;
		}
		else
		{
			gameObject.renderer.enabled = true;
			if(brokenUpPrefab)
				Destroy(brokenUpPrefab);
		}
		
    	// Keep player at 0 on the Z axis at all times (can't fall forward or back. Implement your own constraints here)
    	// character controller + rigid body is a little finicky sometimes, hence this
    	transform.position = new Vector3(transform.position.x,transform.position.y,0);
    }
	
	IEnumerator ShowLine()
	{
		if(Input.GetMouseButton(0))
		{
			if(line != null)
			{
				line.SetPosition(0,transform.position);
				line.SetPosition(1, Camera.mainCamera.ScreenToWorldPoint(Input.mousePosition));
			}
		}
		else
		{
			line.SetPosition(0,Vector3.zero);
			line.SetPosition(1,Vector3.zero);
		}
		yield return null;
	}
	
	void GravityDelay(object source, ElapsedEventArgs e)
	{
		Gravity = initialGravity;
		delayGravity.Enabled = false;
	}
	
	void HandleBasicMovement()
	{
		// Simple Character Controller Implementation
    	// @see(http://docs.unity3d.com/Documentation/ScriptReference/CharacterController.Move.html) for more info
    	 	      if (controller.isGrounded) {
    	          moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    	          moveDirection = transform.TransformDirection(moveDirection);
    	          moveDirection *= Speed;
    		// no multi jump for now
    	          if (Input.GetButton("Jump"))
    	              moveDirection.y = JumpSpeed;
    	      }
    	
    	// Allow the player to control themselves while in the air
    	if(AllowAirControl && !controller.isGrounded)
    	{
    		float tempy = moveDirection.y;
    		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    	          moveDirection = transform.TransformDirection(moveDirection);
    	          moveDirection *= Speed;
    		moveDirection.y = tempy;
    	}
      	moveDirection.y -= Gravity * Time.deltaTime;
      	controller.Move(moveDirection * Time.deltaTime);
	}
	
}
