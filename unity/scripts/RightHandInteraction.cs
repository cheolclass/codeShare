/// RightHandInteraction.cs  

using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class RightHandInteraction : MonoBehaviour
{
    public XRRayInteractor  rightHandRay;   /// 오른손 ray Interactor

    void OnEnable()
    {
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

            // Plane2 또는 텔레포트 앵커이면 삭제하지 않음
            if (target.CompareTag("Plane2Tag") || target.CompareTag("TeleportAnchorTag"))
                return;

            // Cube 삭제
            Destroy(target);
        }
    }

}
