using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float Speed = 6.0F;
    public float JumpSpeed = 10.0F;
    public float Gravity = 20.0F;
	public bool AllowAirControl = true;
	
    private Vector3 moveDirection = Vector3.zero;
    void Update()
	{
		// Simple Character Controller Implementation
		// @see(http://docs.unity3d.com/Documentation/ScriptReference/CharacterController.Move.html) for more info
        CharacterController controller = GetComponent<CharacterController>();
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
		
		// Keep player at 0 on the Z axis at all times (can't fall forward or back. Implement your own constraints here)
		// character controller + rigid body is a little finicky sometimes, hence this
		transform.position = new Vector3(transform.position.x,transform.position.y,0);
    }
}
