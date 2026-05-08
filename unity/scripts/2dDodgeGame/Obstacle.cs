using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private ObstacleSpawner spawner;

	public void Setup(ObstacleSpawner spawner, Vector3 start, Vector3 end)
	{
		this.spawner = spawner;
		StartCoroutine(Process(start, end));
	}

	private IEnumerator Process(Vector3 start, Vector3 end)
	{
		// 회전과 이동을 동시에 시작
		StartCoroutine(OnRotate());
		yield return StartCoroutine(OnMove(start, end));

		// 이동이 끝나면 회전 중지
		StopAllCoroutines();   // 또는 StopCoroutine(OnRotate()) 

		// 크기 축소 시작 (이전 코드에서 이어서)
		StartCoroutine(OnScale());
	}

	// ==================== 회전 ====================
	private IEnumerator OnRotate()
	{
		Vector3 rotateAxis = Random.value > 0.4f ? Vector3.forward : Vector3.back;
		float rotateSpeed = Random.Range(10f, 720f);

		while (true)
		{
			transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
			yield return null;
		}
	}

	// ==================== 이동 ====================
	private IEnumerator OnMove(Vector3 start, Vector3 end)
	{
		float moveTime = 2f;     // 이동하는데 걸리는 시간 (초)
		float percent = 0f;

		while (percent < 1f)
		{
			percent += Time.deltaTime / moveTime;
			transform.position = Vector3.Lerp(start, end, percent);
			yield return null;
		}
	}

	// ==================== 크기 축소 ====================
	private IEnumerator OnScale()
	{
		Vector3 start = Vector3.one;
		Vector3 end = Vector3.zero;
		float scaleTime = 0.5f;
		float percent = 0f;

		while (percent < 1f)
		{
			percent += Time.deltaTime / scaleTime;
			transform.localScale = Vector3.Lerp(start, end, percent);
			yield return null;
		}
	}
}