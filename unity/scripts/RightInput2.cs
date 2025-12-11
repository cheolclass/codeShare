/// RightInput.cs <= 기존 코드 확장

using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class RightInput : MonoBehaviour
{
    public InputActionProperty myActionValue;
    public InputActionProperty myActionButton;

    public XRRayInteractor rightHandRay;   // 오른손 Ray Interactor

    void Start()
    {
    }
    void Update()
    {
        float value = myActionValue.action.ReadValue<float>();
        if (value > 0f)
            Debug.Log("Right Grip Button Value: " + value);     

        bool buttonPressed = myActionButton.action.IsPressed();
        if (buttonPressed)
            Debug.Log("Right Grip Button Pressed: " + buttonPressed);
    }

    /// Ray에 닿은 오브젝트 삭제 

    void OnEnable()
    {
        //rightHand.selectEntered.AddListener(OnSelectEntered);
        rightHandRay.hoverEntered.AddListener(OnHoverEntered);
    }

    void OnDisable()
    {
        rightHandRay.hoverEntered.RemoveListener(OnHoverEntered);
    }
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        // 레이가 닿은 Interactable 오브젝트 가져오기
        var interactable = args.interactableObject;

        if (interactable != null)
        {
            GameObject target = interactable.transform.gameObject;

            // Cube 삭제
            Destroy(target);
        }
    }
}
