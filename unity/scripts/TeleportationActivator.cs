/// TeleportationActivator.cs

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeleportationActivator : MonoBehaviour
{
    public XRRayInteractor teleportInteractor;  /// ray
    public InputActionProperty teleportActivatorAction;  /// input 이벤트 발생 시 action과 연결 위한 변수

    /// 오류 발생 가능
    void Start()
    {
        teleportInteractor.gameObject.SetActive(false);
        teleportActivatorAction.action.performed += Action_performed; /// 버튼 클릭 이벤트 시 함수 호출
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        teleportInteractor.gameObject.SetActive(true);
    }

    void Update()
    {
        if (teleportActivatorAction.action.WasReleasedThisFrame())  /// 사용자가 버튼을 놓았는지 체크 
            teleportInteractor.gameObject.SetActive(false);
    }
}
