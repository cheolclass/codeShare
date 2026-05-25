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

	private GameObject mGO_Player;  /// 추가 

	private void Awake()
	{
		//GameObject gameObj = GameObject.FindWithTag("Player");  ///
		mGO_Player = GameObject.FindWithTag("Player");  ///
		if (mGO_Player)
		{
			//playerRb = mGoPlayer.GetComponent<Rigidbody2D>(); /// 추후 플레이어 속도 설정 위해 
			mGO_Player.TryGetComponent<Rigidbody2D>(out playerRb);

			// originalColor <= 초기 색상 저장 
			Transform rTransform = mGO_Player.transform.Find("Renderer");
			if (rTransform)
			{
				if(rTransform.TryGetComponent<SpriteRenderer>(out playerRenderer))				
					originalColor = playerRenderer.color;
			}
			if (firePoint == null) firePoint = mGO_Player.transform;  /// bullet의 출발점은 플레이어의 위치로
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
		controls.MyPlayer.MyAttack.started += ctx => {  /// callback context 등록 
			///if (mGO_Player == null) return;
			if (mGO_Player == null || playerRenderer == null || playerRenderer.enabled == false) return;

			Debug.Log("난사 시작!!");
			if (playerRenderer) playerRenderer.color = Color.red;
			if (shootingCoroutine == null)
				shootingCoroutine = StartCoroutine(ShootRoutine());  /// 추후 중지를 위해 변수에 저장 
		};

		// 키를 뗐을 때
		controls.MyPlayer.MyAttack.canceled += ctx => { 
			Debug.Log("난사 중지");
			//if (playerRenderer)
			if (playerRenderer && playerRenderer.enabled == true)  /// 이미 죽어서 렌더러가 꺼진 상태라면 색상을 되돌리는 코드를 건너뜀 
				playerRenderer.color = originalColor;
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
		while (mGO_Player != null && playerRenderer != null && playerRenderer.enabled == true)  ///
		{
			if (bulletPrefab)  ///
			{
				// 1. 총알 생성
				GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

				// 2. 총알의 스크립트를 가져와서 플레이어의 현재 속도를 전달
				BulletController bulletScript = bulletGo.GetComponent<BulletController>();
				if (bulletScript != null && playerRb != null)
				{
					// 플레이어의 현재 속도(이동 방향과 힘)를 총알에 주입
					bulletScript.SetInertia(playerRb.linearVelocity);  ///
				}
			}
			yield return new WaitForSeconds(fireRate);
		}
		shootingCoroutine = null;  /// 
		Debug.Log("플레이어 사망으로 인해 발사 코루틴이 안전하게 종료되었습니다.");   ///
	}
}
