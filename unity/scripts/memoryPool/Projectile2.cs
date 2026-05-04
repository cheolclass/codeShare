using UnityEngine;

public class Projectile2 : MonoBehaviour
{
	private Movement2D movement2D;
	private MemoryPool memoryPool;  ///
	private Vector2 limitMin = new Vector2(-8, -5);
	private Vector2 limitMax = new Vector2(8, 5);

	private void Awake()
	{
		movement2D = GetComponent<Movement2D>();
	}

	public void Setup(Vector3 direction, MemoryPool memoryPool)
	{
		this.memoryPool = memoryPool;  ///
		movement2D.MoveTo(direction);
	}

	private void Update()
	{
		// 게임 오브젝트의 x좌표가 화면 바깥으로 나가면
		if (transform.position.x < limitMin.x || transform.position.x > limitMax.x)
		{
			// 발사체 오브젝트 삭제
			///Destroy(gameObject);
			memoryPool.DeactivatePoolItem(gameObject);  ///
		}

		// 게임 오브젝트의 y좌표가 화면 바깥으로 나가면
		if (transform.position.y < limitMin.y || transform.position.y > limitMax.y)
		{
			// 발사체 오브젝트 삭제
			///Destroy(gameObject);
			memoryPool.DeactivatePoolItem(gameObject);   ///
		}
	}
}
