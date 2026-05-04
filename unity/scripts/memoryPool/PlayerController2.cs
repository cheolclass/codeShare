using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
	[SerializeField]
	private GameObject projectilePrefab;
	private MemoryPool	memoryPool;  ///

	private float angle = 0f;

	private void Awake()  ///
	{
		memoryPool = new MemoryPool(projectilePrefab); 
	}

	private void OnApplicationQuit()  /// 
	{
		Debug.Log("Destroy all objects");
		memoryPool.DestroyObjects();
	}
	
	private void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Space))   /// 연사 방지
		if (Keyboard.current.spaceKey.wasPressedThisFrame)  /// 간단한 새로운 입력 시스템을 이용 
		{
			GameObject	clone = memoryPool.ActivatePoolItem();
			clone.transform.position = transform.position;			
			
			// 발사 방향 계산 (degree => radian)
			float rad = angle * Mathf.Deg2Rad;  
			float x = Mathf.Cos(rad);
			float y = Mathf.Sin(rad);

			// 발사체 방향 설정
			clone.GetComponent<Projectile>()?.Setup(new Vector2(x, y));

			// 다음 발사 각도 증가 (5도씩 회전하며 발사)
			angle += 5f;
		}
		/// Esc 키
		if (Keyboard.current.escapeKey.wasPressedThisFrame)  /// 
			memoryPool.DeactivatePoolItem();
	}
}
