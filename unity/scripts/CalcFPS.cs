/// CalcFPS.cs

using UnityEngine;
using UnityEngine.InputSystem;

public class CalcFPS : MonoBehaviour
{
    public InputActionProperty myActionValue;
    public InputActionProperty myActionButton;

    int frameCount = 0;
    float elapsedTime = 0f;

    void Start()
    {
    }
    
    void Update()
    {
        /// frame 율 계산 
        frameCount++;
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            float fps = frameCount / elapsedTime;
            Debug.Log("FPS: " + fps);

            frameCount = 0;
            elapsedTime = 0f;
        }
        
        float value = myActionValue.action.ReadValue<float>();
        if (value > 0f)
            Debug.Log("Left Grip Button Value: " + value);

        bool buttonPressed = myActionButton.action.IsPressed();
        if (buttonPressed)
            Debug.Log("Left Grip Button Pressed: " + buttonPressed);
    }    
}
