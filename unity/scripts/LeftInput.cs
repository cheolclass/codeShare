// LeftInput.cs

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class LeftInput : MonoBehaviour
{
    public InputActionProperty myActionValue;
    public InputActionProperty myActionButton;

    public XRDirectInteractor leftHand;   // 왼손 Direct Interactor

    float myTime = 0f;
    int frame = 0;

    int frameCount = 0;
    float elapsedTime = 0f;

    void Start()
    {
    }
    
    void Update()
    {
        frameCount++;
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            float fps = frameCount / elapsedTime;
            Debug.Log("FPS: " + fps);

            frameCount = 0;
            elapsedTime = 0f;
        }

        //////////////////////////

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

    /// Grip 한 오브젝트 삭제 

    void OnEnable()
    {
        if(leftHand != null) 
            leftHand.selectEntered.AddListener(OnSelectEntered);
    }

    void OnDisable()
    {
        if (leftHand != null)
            leftHand.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // 왼손이 잡은 오브젝트 가져오기
        var interactable = args.interactableObject;

        if (interactable != null)
        {
            // 실제 GameObject 얻기
            GameObject target = interactable.transform.gameObject;

            // Cube 삭제
            Destroy(target);
        }
    }
}
