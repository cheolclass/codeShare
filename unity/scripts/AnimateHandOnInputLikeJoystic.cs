/// AnimateHandOnInputLikeJoystic.cs
/// Grip/Trigger 이벤트 처리 

using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty gripActionValue;
    public InputActionProperty triggerActionValue;

    public Animator handAnimator;

    float cumGripValue = 0f;
    float cumTriggerValue = 0f;

    public event System.Action OnOneSecondElapsed; /// 나중에 연결
                                                   /// 이 이벤트를 받아 실행되는 다른 객체의 스크립트의 함수도 구현해 보자.
    
    void Update()
    {
        float gV = gripActionValue.action.ReadValue<float>();
        float tV = triggerActionValue.action.ReadValue<float>();

        if (gV > 0f)
        {
            if (cumGripValue >= 1f)
                cumGripValue = 0f;
            else
            {
                cumGripValue += Time.deltaTime;
                Debug.Log("Elapsed grip time: " + cumGripValue);
            }
            handAnimator.SetFloat("Grip", cumGripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0f);
            cumGripValue = 0f;
        }

        if (tV > 0f)
        {
            if (cumTriggerValue >= 1f)
                cumTriggerValue = 0f;
            else
            {
                cumTriggerValue += Time.deltaTime;
                Debug.Log("Elapsed trigger time: " + cumTriggerValue);
            }
            handAnimator.SetFloat("Trigger", cumTriggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0f);
            cumTriggerValue = 0f;
        }
    }
}
