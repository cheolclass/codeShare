using UnityEngine;
using UnityEngine.InputSystem;  ///

public class PlayerController : MonoBehaviour {
	[SerializeField]  /// 
	private GameController gameController;  /// 

	[SerializeField]
	private Transform left, right;

	[SerializeField]
	private float moveSpeed = 4;

	[SerializeField]
	private Vector3 moveDirection = Vector3.right;

	private new Collider2D collider2D;  ///
	private SpriteRenderer spriteRenderer;  ///

	private ParticleSystem dieEffect;  /// 

	private void Awake()  ///
	{
		collider2D = GetComponent<Collider2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		dieEffect = GetComponentInChildren<ParticleSystem>();  ///
		dieEffect.Stop();  ///
	}

	private void Update()
	{
		///if(gameController.IsGameStart == false)	return;	 

		if (gameController.IsGameStart == false ||
			gameController.IsGameOver == true) return;  /// 


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
			gameController.Score += 2;      // 플레이어가 양쪽 끝에 도달할 때마다 점수 +2  ///
		}

		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collision) { ///
		if (collision.CompareTag("myObstacle"))
		{
			collider2D.enabled = false;         // 플레이어 오브젝트의 충돌체크 비활성화
			spriteRenderer.enabled = false;     // 플레이어 오브젝트가 보이지 않도록 설정
			dieEffect.Play();	/// 플러이어 사망효과 재생

			gameController.GameOver();          // 게임오버 처리
		}
	}
}
