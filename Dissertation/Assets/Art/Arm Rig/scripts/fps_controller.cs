using UnityEngine;
using System.Collections;

public class fps_controller : MonoBehaviour {

    CharacterController controller;
    float verticalVelocity;
    public float speed = 5.0F;
    public float runSpeed = 8.0F;
    public float gravity = 15.0F;
    public float jumpForce = 5.0F;
    bool CursorLockedVar;
    

    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CursorLockedVar = true;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        float walk = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        walk *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, walk);

        if (Input.GetKey("w") && Input.GetButton("Run"))
        {
            float run = Input.GetAxis("Vertical") * runSpeed;
            run *= Time.deltaTime;
            transform.Translate(strafe, 0, run);
        }


        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveVector * Time.deltaTime);

        if (Input.GetButtonDown("escape") && !CursorLockedVar)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);
            CursorLockedVar = (true);
        }

        else if (Input.GetButtonDown("escape") && CursorLockedVar)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);
            CursorLockedVar = (false);
        }
    }
}