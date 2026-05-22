using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MyPlayerInputHandler : MonoBehaviour
{
	//private @MyInputActions controls;
	private @MyTestActions controls;  ///  

	private SpriteRenderer playerRenderer;
	private Color originalColor;

	[Header("발사 설정")]
	public GameObject bulletPrefab;
	public Transform firePoint;
	public float fireRate = 0.1f;

	private Coroutine shootingCoroutine;
	private Rigidbody2D playerRb; // 플레이어의 리지드바디 참조

	private void Awake()
	{
		// Player 오브젝트에서 Rigidbody2D를 가져옴 
		GameObject gameObj = GameObject.FindWithTag("Player");  ///
		if (gameObj)
		{
			playerRb = gameObj.GetComponent<Rigidbody2D>(); // 플레이어 속도 파악용

			// 기존 Renderer 및 originalColor 설정 로직...
			Transform rendererTransform = gameObj.transform.Find("Renderer");
			if (rendererTransform)
			{
				playerRenderer = rendererTransform.GetComponent<SpriteRenderer>();
				if (playerRenderer) originalColor = playerRenderer.color;
			}
			if (firePoint == null) firePoint = gameObj.transform;
		}
	}

	private void OnEnable()
	{
		// 2. controls가 null이면 생성하고 이벤트를 연결 
		if (controls == null)
		{
			controls = new @MyTestActions();
			SetupInputEvents(); // 여기서 함수를 호출 
		}
		controls.Enable();
	}

	// 3. 입력 이벤트를 관리하는 전용 함수
	private void SetupInputEvents()
	{
		// 키를 눌렀을 때
		controls.MyPlayer.MyAttack.started += ctx => {  /// callback context. 콜백 메서드 등록 
			Debug.Log("난사 시작!!");
			if (playerRenderer) playerRenderer.color = Color.red;
			if (shootingCoroutine == null)
				shootingCoroutine = StartCoroutine(ShootRoutine());  /// 추후 중지를 위해 변수에 저장 
		};

		// 키를 뗐을 때
		controls.MyPlayer.MyAttack.canceled += ctx => { 
			Debug.Log("난사 중지");
			if (playerRenderer) playerRenderer.color = originalColor;
			if (shootingCoroutine != null)
			{
				StopCoroutine(shootingCoroutine);
				shootingCoroutine = null;
			}
		};
	}

	private void OnDisable()
	{
		controls?.Disable();
	}

	IEnumerator ShootRoutine()
	{
		while (true)
		{
			if (bulletPrefab)
			{
				// 1. 총알 생성
				GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

				// 2. 총알의 스크립트를 가져와서 플레이어의 현재 속도를 전달
				BulletController bulletScript = bulletGo.GetComponent<BulletController>();
				if (bulletScript != null && playerRb != null)
				{
					// 플레이어의 현재 속도(이동 방향과 힘)를 총알에 주입
					bulletScript.SetInertia(playerRb.linearVelocity);
				}
			}
			yield return new WaitForSeconds(fireRate);
		}
	}
}