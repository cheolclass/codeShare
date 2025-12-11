/// LeftHandInteraction.cs 

using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class LeftHandInteraction : MonoBehaviour
{
    public XRDirectInteractor   leftHandDirectInter;   /// 왼손 Direct Interactor

    /// Ray에 닿은 오브젝트 삭제 

    void OnEnable()
    {
        leftHandDirectInter.selectEntered.AddListener(OnSelectEntered);
    }

    void OnDisable()
    {
        leftHandDirectInter.selectEntered.RemoveListener(OnSelectEntered);
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
