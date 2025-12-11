/// RightInput.cs

using UnityEngine;
using UnityEngine.InputSystem;

public class RightInput : MonoBehaviour
{
    public InputActionProperty myActionValue;
    public InputActionProperty myActionButton;

    void Update()
    {
        /// 누르거나 뗄 때 모두 출력
        float value = myActionValue.action.ReadValue<float>();
        Debug.Log("Right Activate Button Value: " + value);

        bool buttonPressed = myActionButton.action.IsPressed();
        Debug.Log("Right Activate Button Pressed: " + buttonPressed);
    }
}
