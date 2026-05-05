using UnityEngine;

public class ObstacleSpawner2 : MonoBehaviour
{
	[SerializeField]
	private GameObject obstaclePrefab;  /// 

	[SerializeField]
	private float currentSpawnTime = 2f;        // 장애물 생성 주기
	[SerializeField]
	private float minX = -2f, maxX = 2f;        // 오브젝트 생성/목표 x 위치 범위
	[SerializeField]
	private float minY = -2f, maxY = 5.25f; // 오브젝트 목표, 생성 y 위치

	private MemoryPool memoryPool;
	private float lastSpawnTime = 0f;

	private void Awake()
	{
		memoryPool = new MemoryPool(obstaclePrefab);
	}

	private void Update()
	{
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

	public void DeactivateObject(GameObject clone)
	{
		memoryPool.DeactivatePoolItem(clone);
	}
}
