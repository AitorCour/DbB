using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;
    private float sensitivity = 2.5f;
    private LookRotation lookRotation;
	//public Animator shoot2;
    private Gun gun;

    //private MouseCursor mouseCursor;

	// Use this for initialization
	void Start ()
    {
		//shoot2 = GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        lookRotation = playerController.GetComponent<LookRotation>();
        gun = playerController.GetComponent<Gun>();

        //mouseCursor = new MouseCursor();
        HideCursor();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Mover al player
        Vector2 inputAxis = Vector2.zero;
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");
        playerController.SetAxis(inputAxis);
        //LLamar al salto
        if(Input.GetButtonDown("Jump")) playerController.StartJump();
        //Camara rotación
        Vector2 mouseAxis = Vector2.zero;
        mouseAxis.x = Input.GetAxis("Mouse X") * sensitivity;
        mouseAxis.y = Input.GetAxis("Mouse Y") * sensitivity;

        lookRotation.SetRotation(mouseAxis);

        if(Input.GetMouseButtonDown(0)) HideCursor();
        else if(Input.GetKeyDown(KeyCode.Escape)) ShowCursor();

		if (Input.GetMouseButton (0)) 
		{
			
			gun.Shot();
			//shoot2.SetTrigger("shoot2");

		}
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("particles");
            gun.isShootingParticles = true;
            gun.ShootParticles();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("NOTparticles");
            gun.isShootingParticles = false;
            gun.ShootParticles();
        }

        if (Input.GetKeyDown(KeyCode.R)) gun.Reload();
    }
    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
