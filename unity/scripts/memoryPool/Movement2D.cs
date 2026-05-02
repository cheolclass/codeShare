using UnityEngine;

public class Movement2D : MonoBehaviour
{
	private Vector3 moveDirection = Vector3.zero;
	private float moveSpeed = 5.0f;

	public void MoveTo(Vector3 direction)
	{
		moveDirection = direction;
	}

	private void Update()
	{
		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}
}