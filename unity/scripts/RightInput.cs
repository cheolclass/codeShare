/// RightInput.cs

using UnityEngine;
using UnityEngine.InputSystem;

public class RightInput : MonoBehaviour
{
    public InputActionProperty myActionValue;
    public InputActionProperty myActionButton;

    void Update()
    {
        float value = myActionValue.action.ReadValue<float>();
        if (value > 0f) 
            Debug.Log("Right Activate Button Value: " + value);

        bool buttonPressed = myActionButton.action.IsPressed();
        if (buttonPressed)
            Debug.Log("Right Activate Button Pressed: " + buttonPressed);
    }
}
