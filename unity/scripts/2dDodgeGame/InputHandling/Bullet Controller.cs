using UnityEngine;

public class BulletController: MonoBehaviour
{
	public float bulletVerticalSpeed = 15f; // 위로 날아가는 힘
	private Rigidbody2D mRB;

	void Awake()
	{
		mRB = GetComponent<Rigidbody2D>();
	}

	// 플레이어의 속도를 외부에서 넣어주는 함수
	public void SetInertia(Vector2 playerVelocity)
	{
		if (mRB == null) mRB = GetComponent<Rigidbody2D>();

		// (플레이어의 좌우 속도) + (총알 본래의 수직 속도)
		mRB.linearVelocity = new Vector2(playerVelocity.x, bulletVerticalSpeed);
	}

	void Start()
	{
		Destroy(gameObject, 2f);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 부딪힌 대상이 장애물("myObstacle") 태그를 가지고 있다면
		if (collision.CompareTag("myObstacle"))
		{
			// 1. 장애물 오브젝트에서 방금 만든 ObstacleController 스크립트를 안전하게 캡처
			if (collision.TryGetComponent<ObstacleController>(out var obstacle))  /// obstacle의 생명주기: 메소드 전체
			{
				// 2. 장애물의 파괴 효과 및 프로세스 가동!
				obstacle.DestroyObstacle();
			}

			// 3. 장애물과 부딪힌 총알 자신도 그 자리에서 즉시 삭제
			Destroy(gameObject);
		}
	}
}