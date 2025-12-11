/// AnimateHandOnInput.cs

using UnityEngine;
using UnityEngine.InputSystem;


public class AnimateHandOnInput : MonoBehaviour
{
    public  InputActionProperty     gripActionValue; 
    public  InputActionProperty     triggerActionValue;

    public Animator handAnimator;

    void Update()
    {
        float gripValue = gripActionValue.action.ReadValue<float>();
        float triggerValue = triggerActionValue.action.ReadValue<float>();

        handAnimator.SetFloat("Grip", gripValue);
        handAnimator.SetFloat("Trigger", triggerValue);
    }
}
