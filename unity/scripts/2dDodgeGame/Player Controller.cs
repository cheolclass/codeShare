using UnityEngine;
using UnityEngine.InputSystem;  ///

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private	GameController	gameController;  /// 

	[SerializeField]
	private Transform left, right;
	
	[SerializeField]
	private float moveSpeed = 3;  /// 
	
	[SerializeField]
	private Vector3 moveDirection = Vector3.right;

	private void Update()
	{
		if(gameController.IsGameStart == false)	return;	/// 

		// 마우스 클릭 or 화면 터치로 방향 전환
		//if (Input.GetMouseButtonDown(0))  /// legacy Input System 
		if (Mouse.current.leftButton.wasPressedThisFrame)  /// 새로운 Input System 사용
		{
			moveDirection *= -1f; 
		}

		// 이동 방향이 오른쪽일 때 오른쪽 끝에 도달하거나 ||
		// 이동 방향이 왼쪽일 때 왼쪽 끝에 도달하면 방향 전환
		if ((moveDirection == Vector3.right && transform.position.x >= right.position.x) ||
			 (moveDirection == Vector3.left && transform.position.x <= left.position.x))
		{
			moveDirection *= -1f;
			gameController.Score += 2;		// 플레이어가 양쪽 끝에 도달할 때마다 점수 +2  ///
		}

		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}
}
