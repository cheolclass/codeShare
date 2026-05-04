using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
	[SerializeField]
	private GameObject projectilePrefab;
	private MemoryPool memoryPool;  ///

	private float angle = 0f;

	///private void Awake()  /// 컴포넌트가 inactive 상돼에서도 실행됨
	private void Start() /// active 상태일때만 실행됨
	{
		memoryPool = new MemoryPool(projectilePrefab);
	}

	private void OnApplicationQuit()  /// inactive 상돼에서도 실행됨
	{
		Debug.Log("Destroy all objects");
		memoryPool?.DestroyObjects();  /// null이면 호출 자체를 안 함. 최종 종료 시 한 번 성공적으로 호출됨 
	}

	private void Update()
	{
		///if (Input.GetKey(KeyCode.Space))   /// 연사  
		if (Keyboard.current.spaceKey.wasPressedThisFrame)  /// 간단한 새로운 입력 시스템을 이용 
		{
			if(memoryPool == null)  /// memoryPool이 null 경우: 처음 실행시 또는 Esc 키로 모든 오브젝트를 삭제한 후에 다시 Space 키를 눌렀을 때  
				memoryPool = new MemoryPool(projectilePrefab);

			GameObject clone = memoryPool?.ActivatePoolItem();  /// null check
			if (clone == null) return;

			clone.transform.position = transform.position;

			// 발사 방향 계산 (degree => radian)
			float rad = angle * Mathf.Deg2Rad;
			float x = Mathf.Cos(rad);
			float y = Mathf.Sin(rad);

			// 발사체 방향 설정
			///clone.GetComponent<Projectile>()?.Setup(new Vector2(x, y));
			clone.GetComponent<Projectile2>()?.Setup(new Vector2(x, y), memoryPool);  /// 

			// 다음 발사 각도 증가 (5도씩 회전하며 발사)
			angle += 5f;
		}
		/// Esc 키
		if (Keyboard.current.escapeKey.wasPressedThisFrame)  /// 
		{		
			///memoryPool.DeactivateAllPoolItems();  /// 메모리 풀링의 취지에 맞게 이렇게 inactive로 하는것이 맞음
			memoryPool?.DestroyObjects(); /// 굳이 삭제하고 싶다면 이렇게
			memoryPool = null;  /// 메모리 풀 자체도 null로 만들어서 다시 Space 키를 눌렀을 때 새롭게 생성되도록 함 
		}
	}
}
