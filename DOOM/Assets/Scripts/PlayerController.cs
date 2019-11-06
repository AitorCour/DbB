using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private GlitchEffect glitchEffect;
    private Vector2 axis;
    public float speed;
    public Vector3 moveDirection;
    private float forceToGround = Physics.gravity.y;
    public float jumpSpeed;
    public bool jump;
    public float gravityMag;
    public float iniLife;
    public float playerLife;
    //public Animator animacion;

	// Use this for initialization
	void Start ()
    {
       // moveDirection.x = axis.x * speed;
      //  moveDirection.z = axis.y * speed;
        controller = GetComponent<CharacterController>();
        glitchEffect = GetComponentInChildren<GlitchEffect>();
        playerLife = iniLife;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(controller.isGrounded && !jump)
        {
            moveDirection.y = forceToGround;
        }

        else
        {
            jump = false;
            moveDirection.y += Physics.gravity.y * gravityMag * Time.deltaTime;
        }

        Vector3 transformDirection = axis.x * transform.right + axis.y * transform.forward;


        moveDirection.x = transformDirection.x * speed;
        moveDirection.z = transformDirection.z * speed;
        

        controller.Move(moveDirection * Time.deltaTime);
	}

    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
        if(axis.x != 0 || axis.y !=0)
            {
            //animacion.SetBool("isWalking", true);
            }
        else
        {
            //animacion.SetBool("isWalking", false);
        }
    }

    public void StartJump()
    {
        if(!controller.isGrounded) return;

        moveDirection.y = jumpSpeed;
        jump = true;
    }
    public void LoseLife(float damage)
    {
        playerLife -= damage;
        Handheld.Vibrate();
        glitchEffect.intensity = 1;
        glitchEffect.flipIntensity = 1;
        glitchEffect.colorIntensity = 1;
        StartCoroutine(BugCamera());
    }
    private IEnumerator BugCamera() //Usar corutinas para contar tiempo
    {
        yield return new WaitForSeconds(1);
        if(playerLife <= iniLife/1.5)
        {
            glitchEffect.intensity = 0.15f;
            glitchEffect.flipIntensity = 0.15f;
            glitchEffect.colorIntensity = 0.15f;
            Debug.Log("CameraBug_1");
        }
        if (playerLife <= iniLife / 2)
        {
            glitchEffect.intensity = 0.30f;
            glitchEffect.flipIntensity = 0.30f;
            glitchEffect.colorIntensity = 0.30f;
            Debug.Log("CameraBug_2");
        }
        if (playerLife <= iniLife / 2.5)
        {
            glitchEffect.intensity = 0.50f;
            glitchEffect.flipIntensity = 0.50f;
            glitchEffect.colorIntensity = 0.50f;
            Debug.Log("CameraBug_3");
        }
        if (playerLife <= iniLife / 3)
        {
            glitchEffect.intensity = 0.75f;
            glitchEffect.flipIntensity = 0.75f;
            glitchEffect.colorIntensity = 0.75f;
            Debug.Log("CameraBug_4");
        }
        if (playerLife > iniLife / 1.5)
        {
            glitchEffect.intensity = 0;
            glitchEffect.flipIntensity = 0;
            glitchEffect.colorIntensity = 0;
        }

        // yield return null;//cierra la corutina
    }
}
