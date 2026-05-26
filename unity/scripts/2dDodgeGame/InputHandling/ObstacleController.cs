using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	//private Collider2D collider2D;
	private new Collider2D collider2D;  /// 부모 클래스의 동일 멤버 무시 위해 
	private SpriteRenderer spriteRenderer;
	private ParticleSystem dieEffect;

	private void Awake()
	{
		collider2D = GetComponent<Collider2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		dieEffect = GetComponentInChildren<ParticleSystem>();

		// 시작할 때 파티클이 자동으로 나오는 것을 방지
		if (dieEffect != null)
		{
			dieEffect.Stop();
		}
	}

	// 총알과 부딪혔을 때 외부(Bullet)에서 호출 
	public void DestroyObstacle()
	{
		// 1. 이미 파괴 진행 중이라면 중복 실행 방지
		if (collider2D != null && collider2D.enabled == false) return;

		// 2. 물리 충돌과 이미지를 꺼서 화면에서 숨김 (Player 판정과 동일)
		if (collider2D != null) collider2D.enabled = false;
		if (spriteRenderer != null) spriteRenderer.enabled = false;

		// 3. 파티클 이펙트 재생
		if (dieEffect != null)
		{
			dieEffect.Play();

			// 4. 이펙트가 다 재생된 후(예: 1~2초 뒤)에 오브젝트를 완전히 메모리에서 삭제
			// dieEffect.main.duration을 쓰면 파티클 세팅 시간에 맞춰 자동으로 맞춰집니다.
			Destroy(gameObject, dieEffect.main.duration);
		}
		else
		{
			// 혹시 파티클을 안 넣었다면  즉시 삭제
			Destroy(gameObject);
		}
	}
}