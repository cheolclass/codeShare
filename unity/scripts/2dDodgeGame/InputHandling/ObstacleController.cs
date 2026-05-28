using System.Collections;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
	private ObstacleSpawner spawner;

	// 컴포넌트 캐싱 변수 (부모 클래스의 동일 명칭 경고 방지를 위해 new 사용)
	private new Collider2D collider2D;   
	private SpriteRenderer spriteRenderer;
	private ParticleSystem dieEffect;

	// 총알에 맞았을 때 연출(이동/회전)을 도중에 끊기 위한 코루틴 제어권
	private Coroutine processRoutine;

	private void Awake()
	{
		// 씬 내부 자식 오브젝트(Renderer, Explosion Effect) 구조에서 안전하게 컴포넌트 캐싱
		collider2D = GetComponent<Collider2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		dieEffect = GetComponentInChildren<ParticleSystem>();

		// 시작할 때 파티클이 자동으로 나오는 것을 방지
		if (dieEffect != null)
		{
			dieEffect.Stop();
		}
	}

	// 오브젝트 풀에서 활성화(재활용)될 때마다 상태를 완벽하게 초기화하는 유니티 생명주기
	private void OnEnable()
	{
		// 이전 충돌 때 꺼놓았던 물리 판정과 이미지를 다시 원상복구 
		if (collider2D != null) collider2D.enabled = true;
		if (spriteRenderer != null) spriteRenderer.enabled = true;

		// 파티클 시스템 리셋 및 Play On Awake 이중 방어
		if (dieEffect != null) dieEffect.Stop();
	}

	// 스포너가 풀에서 오브젝트를 꺼낸 직후 호출해 주는 초기화 메서드
	public void Setup(ObstacleSpawner spawner, Vector3 start, Vector3 end)
	{
		this.spawner = spawner;

		ResetTransform();
		processRoutine = StartCoroutine(Process(start, end));
	}

	private IEnumerator Process(Vector3 start, Vector3 end)
	{
		float moveTime = 2f;
		float rotateAngle = Random.Range(10f, 720f) * moveTime;

		// 1. 회전(OnRotate)과 이동(OnMove) 연출을 멀티태스킹으로 동시 가동
		Coroutine rotateRoutine = StartCoroutine(
			TransformEffect.OnRotate(transform, Vector3.zero, Vector3.forward * rotateAngle, moveTime));
		yield return StartCoroutine(TransformEffect.OnMove(transform, start, end, moveTime));

		// 이동이 정상 종료되면 회전 코루틴도 함께 안전하게 정지
		if (rotateRoutine != null)
		{
			StopCoroutine(rotateRoutine);
		}

		// 2. [정상 회피 성공 조건] 바닥 끝 지점에 무사히 안착하면 크기를 줄이며 페이드 아웃 연출
		yield return StartCoroutine(TransformEffect.OnScale(transform, Vector3.one, Vector3.zero, 0.5f));

		// 스포너에게 정상 회피(점수 추가 대상)임을 알리며 풀 반납
		OnDie();
	}

	private void ResetTransform()
	{
		transform.localScale = Vector3.one;
		transform.rotation = Quaternion.identity;
	}

	public void OnDie()
	{
		// isEvaded: true -> 플레이어가 회피에 성공했으므로 점수를 증가 
		spawner.DeactivateObject(gameObject, isEvaded: true);
	}

	// 총알과 충돌 시 외부(BulletController)에서 이 함수를 호출
	public void DestroyObstacle()
	{
		// 이미 파괴 진행 중이라면 중복 실행 방지
		if (collider2D != null && collider2D.enabled == false) return;

		// 1. 진행 중이던 기존 이동/회전 코루틴을 강제로 중단하여 그 자리에 고정
		if (processRoutine != null)
		{
			StopCoroutine(processRoutine);
			processRoutine = null;
		}

		// 2. 물리 충돌체와 랜더러 이미지를 즉시 꺼서 씬에서 감추기 
		if (collider2D != null) collider2D.enabled = false;
		if (spriteRenderer != null) spriteRenderer.enabled = false;

		// 3. 파티클 재생 및 지연 풀 반납 프로세스 개시
		StartCoroutine(DieEffectRoutine());
	}

	private IEnumerator DieEffectRoutine()
	{
		float delayTime = 0f;

		if (dieEffect != null)
		{
			dieEffect.Play();  /// 파괴 파티클 효과 재생 시작 
			delayTime = dieEffect.main.duration; // 인스펙터창 파티클 Duration(0.5초)에 자동으로 맞춤
		}

		// 파티클 폭발 효과가 완전히 퍼지고 끝날 때까지 대기
		yield return new WaitForSeconds(delayTime);

		// 4. 연출 종료 후 메모리 풀 반납 (총알에 사살당한 것이므로 점수 증가 없음: false)
		spawner.DeactivateObject(gameObject, isEvaded: false);
	}
}
