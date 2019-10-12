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

    public bool mobileControls;
    [HideInInspector]
    public Vector2 mobileAxis;
    [HideInInspector]
    public Vector2 mobileLookAxis;
    [HideInInspector]
    public bool shootAxis;
    [HideInInspector]
    public bool jumpAxis;
    [HideInInspector]
    public bool reloadAxis;
    private bool shooting;
    private bool jumping;

    //private MouseCursor mouseCursor;

	// Use this for initialization
	void Start ()
    {
		//shoot2 = GetComponent<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        lookRotation = playerController.GetComponent<LookRotation>();
        gun = playerController.GetComponent<Gun>();

        //mouseCursor = new MouseCursor();
        if(!mobileControls)
        {
            HideCursor();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Mover al player
        Vector2 inputAxis = Vector2.zero;
        if (!mobileControls)
        {
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");
        }
        else if(mobileControls)
        {
            inputAxis.x = mobileAxis.x;
            inputAxis.y = mobileAxis.y;
        }
        playerController.SetAxis(inputAxis);
        //LLamar al salto
        if (Input.GetButtonDown("Jump") && !mobileControls) playerController.StartJump();
        else if (jumpAxis && mobileControls && !jumping)
        {
            playerController.StartJump();
            jumping = true;
        }
        else if (!jumpAxis && mobileControls && jumping)
        {
            jumping = false;
        }
        //Camara rotación
        Vector2 mouseAxis = Vector2.zero;
        if (!mobileControls)
        {
            mouseAxis.x = Input.GetAxis("Mouse X") * sensitivity;
            mouseAxis.y = Input.GetAxis("Mouse Y") * sensitivity;
        }
        else if(mobileControls)
        {
            mouseAxis.x = mobileLookAxis.x;
            mouseAxis.y = mobileLookAxis.y;
        }
        lookRotation.SetRotation(mouseAxis);

        if (Input.GetMouseButtonDown(0) && !mobileControls) HideCursor();
        else if(Input.GetKeyDown(KeyCode.Escape)) ShowCursor();

		if (Input.GetMouseButton (0) && !mobileControls) 
		{
			gun.Shot();
            //shoot2.SetTrigger("shoot2");
		}
        if(shootAxis && mobileControls)
        {
            gun.Shot();
        }
        if (Input.GetMouseButtonDown(0) && !mobileControls)
        {
            Debug.Log("particles");
            gun.isShootingParticles = true;
            gun.ShootParticles();
        }
        if (Input.GetMouseButtonUp(0) && !mobileControls)
        {
            Debug.Log("NOTparticles");
            gun.isShootingParticles = false;
            gun.ShootParticles();
        }

        if (shootAxis && mobileControls && !shooting)
        {
            ShootAxis();
        }
        if (!shootAxis && mobileControls)
        {
            NoShootAxis();
        }

        if (Input.GetKeyDown(KeyCode.R) && !mobileControls) gun.Reload();
        else if (reloadAxis && mobileControls)
        {
            gun.Reload();
        }
    }
    void ShootAxis()
    {
        //Debug.Log("TabParticles");
        gun.isShootingParticles = true;
        gun.ShootParticles();
        shooting = true;
    }
    void NoShootAxis()
    {
        //Debug.Log("NOTTabParticles");
        gun.isShootingParticles = false;
        gun.ShootParticles();
        shooting = false;
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
