using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
	[SerializeField]
	private GameController gameController;  ///

	[SerializeField]
	private GameObject obstaclePrefab;    
            
	[SerializeField]
	private float currentSpawnTime = 2f;        // 장애물 생성 주기

	[SerializeField]
	private float minX = -2f, maxX = 2f;        // 오브젝트 생성/목표 x 위치 범위

	[SerializeField]
	private float minY = -5f, maxY = 5.25f; // 오브젝트 목표, 생성 y 위치

	private MemoryPool memoryPool;   /// 

	private float lastSpawnTime = 0f;

	private void Awake()	///
	{
		memoryPool = new MemoryPool(obstaclePrefab);
	}

	private void Update()
	{
		///if(gameController.IsGameStart == false)	return;	/// 
		if( gameController.IsGameStart == false || 
			gameController.IsGameOver == true ) return;  /// 

		// 생성 주기(currentSpawnTime) 시간마다 오브젝트 생성
		if (Time.time - lastSpawnTime > currentSpawnTime)
		{
			lastSpawnTime = Time.time;
			SpawnObject();
		}
	}

	private void SpawnObject()
	{
		Vector3 start = new Vector3(Random.Range(minX, maxX), maxY, 0f);
		Vector3 end = new Vector3(Random.Range(minX, maxX), minY, 0f);

        ///GameObject clone = Instantiate(obstaclePrefab, start, Quaternion.identity);
		GameObject clone = memoryPool.ActivatePoolItem(start);
		///clone.GetComponent<Obstacle2>().Setup(this, start, end);  /// 
		if (clone.TryGetComponent<Obstacle2>(out var obstacle))  /// check: 객체 null & 컴포넌트 존재 여부
		{
			obstacle.Setup(this, start, end);
		}
		else
		{
			Debug.LogError($"Obstacle 컴포넌트 없음! Prefab: {obstaclePrefab.name}");
			Destroy(clone);
		}
	}

	public void DeactivateObject(GameObject clone)  ///
	{
		gameController.Score++;	// 플레이어가 회피한 장애물이 사라질 때 점수 +1  ///
		memoryPool.DeactivatePoolItem(clone);
	}
}