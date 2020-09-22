using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{



	public void MoveRight(GameObject piece)
	{
		//Piece newPiece;
		piece.transform.position = piece.transform.position + new Vector3(1, 0, 0);

	}

	//void MoveLeft()
	//{
	//	piece.transform.position = piece.transform.position + new Vector3(-1, 0, 0);
	//	if (Collide3())
	//	{
	//		piece.transform.position = piece.transform.position + new Vector3(1, 0, 0);

	//	}
	//}
}
