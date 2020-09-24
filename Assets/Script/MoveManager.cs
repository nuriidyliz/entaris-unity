using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{


	// "Direction.Right --> 1" for move right - "Direction.Left --> -1" move left
	public void MoveHorizontal(GameObject piece, Direction dir)
	{
		piece.transform.position = piece.transform.position + new Vector3((int)dir, 0, 0);
	}

	
	public void MoveDown(GameObject piece)
	{
		piece.transform.position = piece.transform.position + new Vector3(0, -1, 0);
	}


	//rotate with angle 90 or -90 with simple animation
	public IEnumerator Rotate(GameObject piece, float angle)
	{
		float step = 15;
		if (angle < 0)
		{
			angle = -1 * angle;
			step = -1 * step;
		}

		while (angle > 0)
		{
			piece.transform.rotation = Quaternion.Euler(0, 0, piece.transform.rotation.eulerAngles.z + step);
			angle -= 15;

			yield return null;
		}
		yield return null;
	}
}
