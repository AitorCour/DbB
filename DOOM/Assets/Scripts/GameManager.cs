using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private InputManager inputManager;
    public DynamicJoystick dynamicJoystick;
    public FixedTouchField fixedTouchField;
    public FixedButton shootButton;
    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        inputManager.mobileAxis = dynamicJoystick.Direction;
        inputManager.mobileLookAxis = fixedTouchField.TouchDist;
        inputManager.shootAxis = shootButton.Pressed;
    }
}
