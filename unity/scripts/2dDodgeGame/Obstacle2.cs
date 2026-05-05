using System.Collections;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
	private ObstacleSpawner2 spawner;

	public void Setup(ObstacleSpawner2 spawner, Vector3 start, Vector3 end)
	{
		this.spawner = spawner;
		StartCoroutine(Process(start, end));
	}

	private IEnumerator Process(Vector3 start, Vector3 end)
	{
		float moveTime = 2f;
		float rotateAngle = Random.Range(10f, 720f) * moveTime;

		// 회전(OnRotate) + 이동(OnMove) 시작
		//StartCoroutine(OnRotate());		
		StartCoroutine(TransformEffect.OnRotate(transform, Vector3.zero, Vector3.forward * rotateAngle, moveTime));
		//yield return StartCoroutine(OnMove(start, end));
		yield return StartCoroutine(TransformEffect.OnMove(transform, start, end, moveTime));

		// 이동이 끝나면 회전 중지
		//StopAllCoroutines();   // 또는 StopCoroutine(OnRotate()) 

		// 크기 축소(OnScale) 시작
		//StartCoroutine(OnScale());
		StartCoroutine(TransformEffect.OnScale(transform, Vector3.one, Vector3.zero, 0.5f));  /// 
	}
}
