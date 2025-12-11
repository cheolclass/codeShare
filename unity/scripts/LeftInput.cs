/// LeftInput.cs

using UnityEngine;
using UnityEngine.InputSystem;

public class LeftInput : MonoBehaviour
{
    public InputActionProperty myActionValue;
    public InputActionProperty myActionButton;

    void Update()
    {
        /// 누르거나 뗄 때 모두 출력
        float value = myActionValue.action.ReadValue<float>();        
        Debug.Log("Left Grip Button Value: " + value);

        bool buttonPressed = myActionButton.action.IsPressed();        
        Debug.Log("Left Grip Button Pressed: " + buttonPressed);
    }
}
