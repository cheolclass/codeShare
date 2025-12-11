/// LeftInput0.cs  => Left 오브젝트의 컴포넌트로 추가 

using UnityEngine;
using UnityEngine.InputSystem;

public class LeftInput : MonoBehaviour
{
    public InputActionProperty myActionValue;
    public InputActionProperty myActionButton;

    float myTime = 0f;
    int   frame = 0;

    void Start()
    {
    }

    void Update()
    {
        myTime += Time.deltaTime;
        frame++;

        if (myTime >= 1f)
            { 
            Debug.Log("Frames: " + frame); 
            myTime = 0f;
            frame = 0;
        }


        float value = myActionValue.action.ReadValue<float>();
        if (value > 0f)
            Debug.Log("Left Grip Button Value: " + value);

        bool buttonPressed = myActionButton.action.IsPressed();
        if (buttonPressed)
            Debug.Log("Left Grip Button Pressed: " + buttonPressed);
    }
}
