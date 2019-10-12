using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private InputManager inputManager;
    private DynamicJoystick dynamicJoystick;
    private FixedTouchField fixedTouchField;
    private FixedMoveCameraButton shootButton;
    private FixedButton jumpButton;
    private FixedButton reloadButton;
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        dynamicJoystick = GameObject.FindGameObjectWithTag("DynamicJoystick").GetComponent<DynamicJoystick>();
        fixedTouchField = GameObject.FindGameObjectWithTag("TouchField").GetComponent<FixedTouchField>();
        shootButton = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<FixedMoveCameraButton>();
        jumpButton = GameObject.FindGameObjectWithTag("JumpButton").GetComponent<FixedButton>();
        reloadButton = GameObject.FindGameObjectWithTag("ReloadButton").GetComponent<FixedButton>();
    }

    // Update is called once per frame
    void Update()
    {
        inputManager.mobileAxis = dynamicJoystick.Direction;
        inputManager.mobileLookAxis = fixedTouchField.TouchDist;
        inputManager.shootAxis = shootButton.Pressed;
        inputManager.jumpAxis = jumpButton.Pressed;
        inputManager.reloadAxis = reloadButton.Pressed;
    }
}
