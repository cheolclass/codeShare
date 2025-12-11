/// LeftInput.cs


using UnityEngine;
using UnityEngine.InputSystem;

public class LeftInput : MonoBehaviour
{
    public InputActionProperty myActionValue;
    public InputActionProperty myActionButton;

    void Update()
    {
        float value = myActionValue.action.ReadValue<float>();
        if(value > 0f)
            Debug.Log("Left Grip Button Value: " + value);

        bool buttonPressed = myActionButton.action.IsPressed();
        if (buttonPressed)
            Debug.Log("Left Grip Button Pressed: " + buttonPressed);
    }
}
