using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{



	public void MoveRight(GameObject piece)
	{
		piece.transform.position = piece.transform.position + new Vector3(1, 0, 0);
	}

	void MoveLeft(GameObject piece)
	{
		piece.transform.position = piece.transform.position + new Vector3(-1, 0, 0);
	}
}
