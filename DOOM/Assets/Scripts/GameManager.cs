using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private InputManager inputManager;
    private AudioSource music;
    private DynamicJoystick dynamicJoystick;
    private FixedTouchField fixedTouchField;
    private FixedMoveCameraButton shootButton;
    private FixedButton jumpButton;
    private FixedButton reloadButton;

    private bool paused;
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        music = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        dynamicJoystick = GameObject.FindGameObjectWithTag("DynamicJoystick").GetComponent<DynamicJoystick>();
        fixedTouchField = GameObject.FindGameObjectWithTag("TouchField").GetComponent<FixedTouchField>();
        shootButton = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<FixedMoveCameraButton>();
        jumpButton = GameObject.FindGameObjectWithTag("JumpButton").GetComponent<FixedButton>();
        reloadButton = GameObject.FindGameObjectWithTag("ReloadButton").GetComponent<FixedButton>();

        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        inputManager.mobileAxis = dynamicJoystick.Direction;
        inputManager.mobileLookAxis = fixedTouchField.TouchDist;
        inputManager.shootAxis = shootButton.Pressed;
        inputManager.jumpAxis = jumpButton.pressed;
        inputManager.reloadAxis = reloadButton.pressed;
    }
    public void Pause()
    {
        if (!paused)
        {
            Time.timeScale = 0;
            inputManager.Pause(true);
            paused = true;
        }
        else
        {
            Time.timeScale = 1;
            inputManager.Pause(false);
            paused = false;
        }
    }
    public void SetTime(float newTime)
    {
        Time.timeScale = newTime;
        music.pitch = newTime;
    }
}
