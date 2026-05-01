using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Obstacle : MonoBehaviour
{
	private ObstacleSpawner spawner;

	public void Setup(ObstacleSpawner spawner, Vector3 start, Vector3 end)
	{
		this.spawner = spawner;

		//Reset();
		StartCoroutine(Process(start, end));
	}

	private IEnumerator Process(Vector3 start, Vector3 end)
	{
		float moveTime = 2f;
		float rotateAngle = Random.Range(10f, 720f) * moveTime;

		// 회전(OnRotate) + 이동(OnMove) 시작
		StartCoroutine(TransformEffect.OnRotate(transform, 
			Vector3.zero, Vector3.forward * rotateAngle, moveTime));
		yield return StartCoroutine(
			TransformEffect.OnMove(transform, start, end, moveTime));
		// 크기 축소(OnScale) 시작
		StartCoroutine(TransformEffect.OnScale(
			transform, Vector3.one, Vector3.zero, 0.5f, OnDie));

		/// ------------------------------------------
		///	나중에, TransformEffect 클래스 추가시 
		///	아래 코드 모두 삭제하고 위 코드 주석 제거 	
		/// ------------------------------------------ 
		////// 회전(OnRotate) + 이동(OnMove) 시작
		//StartCoroutine(nameof(OnRotate));
		//yield return StartCoroutine(OnMove(start, end));
		////// 이동이 끝나면 회전 중지 
		//StopCoroutine(nameof(OnRotate));
		////// 크기 축소(OnScale) 시작
		//yield return StartCoroutine(nameof(OnScale));
	}

	private IEnumerator OnRotate()
	{
		/// forward: Z축 기준 CCW, back: CW
		Vector3 rotateAxis = Random.value > 0.4f ? Vector3.forward : Vector3.back;
		float rotateSpeed = Random.Range(10f, 720f);

		while (true)
		{
			transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);  /// max 초당 2바퀴 
			yield return null;
		}
	}

	private IEnumerator OnMove(Vector3 start, Vector3 end)
	{
		float moveTime = 2f;
		float percent = 0f;

		while (percent < 1f)
		{
			percent += Time.deltaTime / moveTime;
			transform.position = Vector3.Lerp(start, end, percent);
			yield return null;
		}
	}

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

	private void Reset()
	{
		transform.localScale = Vector3.one;
		transform.rotation = Quaternion.identity;
	}

	public void OnDie()
	{
		// 오브젝트 크기가 0이 되면 오브젝트 비활성화
		spawner.DeactivateObject(gameObject);
	}
}
