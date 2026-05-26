using UnityEngine;

public class BulletController: MonoBehaviour
{
	public float bulletVerticalSpeed = 15f; // 위로 날아가는 힘
	private Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// 플레이어의 속도를 외부에서 넣어주는 함수
	public void SetInertia(Vector2 playerVelocity)
	{
		if (rb == null) rb = GetComponent<Rigidbody2D>();

		// (플레이어의 좌우 속도) + (총알 본래의 수직 속도)
		rb.linearVelocity = new Vector2(playerVelocity.x, bulletVerticalSpeed);
	}

	void Start()
	{
		Destroy(gameObject, 2f);
	}
}
