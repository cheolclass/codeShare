using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private	ObstacleSpawner	spawner;

	private void Reset()
	{
		transform.localScale = Vector3.one;
		transform.rotation	 = Quaternion.identity;
	}

	public void Setup(ObstacleSpawner spawner, Vector3 start, Vector3 end)
	{
		this.spawner = spawner;

		Reset();
		StartCoroutine(Process(start, end));
	}

	public void OnDie()
	{
		// 오브젝트 크기가 0이 되면 오브젝트 비활성화
		spawner.DeactivateObject(gameObject);
	}

	private IEnumerator Process(Vector3 start, Vector3 end)
	{
		float moveTime = 2f;
		float rotateAngle = Random.Range(10f, 720f) * moveTime;

		// 회전(OnRotate) + 이동(OnMove) 시작
		StartCoroutine(TransformEffect.OnRotate(transform, Vector3.zero, Vector3.forward * rotateAngle, moveTime));
		yield return StartCoroutine(TransformEffect.OnMove(transform, start, end, moveTime));

		// 크기 축소(OnScale) 시작
		StartCoroutine(TransformEffect.OnScale(transform, Vector3.one, Vector3.zero, 0.5f, OnDie));
	}
}

