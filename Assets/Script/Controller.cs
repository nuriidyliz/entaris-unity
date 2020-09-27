using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Controller : MonoBehaviour
{
	public bool acceptInput = true;

	private MoveManager moveManager;
	private CollisionManager collisionManager;
	private float time;
	private float downTime;
	private float time2;
	private float downTime2;

	void Start()
	{
		Debug.Log("controller bu");
		moveManager = new MoveManager();	
		
	}

	void Update()
	{
		
	}

	public void TryMoveRight(GameObject piece)
	{
		moveManager.MoveHorizontal(piece, Direction.Right);

		if (collisionManager.Collide3(piece))
		{
			moveManager.MoveHorizontal(piece, Direction.Left);
		}
	}

	public void TryMoveLeft(GameObject piece)
	{
		moveManager.MoveHorizontal(piece, Direction.Left);

		//if (Collide3())
		//{
		//	moveManager.MoveHorizontal(piece, Direction.Right);
		//}
	}

	public void TryMoveDown(GameObject piece)
	{
		time = time + Time.deltaTime;
		if (time > downTime)
		{
			time2 += Time.deltaTime;

			if (time2 > downTime2)
			{

				//Drop();
				time2 = 0;

			}

		}
	}

	public void TryRotateLeft(GameObject piece)
	{
		Vector3 pos = piece.transform.position;
		int rotateOffset = 1;

		//if (!Collide4(rotate: 90))
		//{
		//	StartCoroutine(moveManager.Rotate(piece, 90));

		//}
		//else
		//{
		//	while (Collide4(rotate: 90))                                                      //calisan
		//	{

		//		piece.transform.position = piece.transform.position + new Vector3(rotateOffset, 0, 0);
		//		rotateOffset = -(rotateOffset + (rotateOffset > 0 ? 1 : -1));

		//		if (rotateOffset > pieceArray.GetLength(0))
		//		{
		//			piece.transform.position = pos;
		//			break;
		//		}

		//		if (!Collide4(rotate: 90))
		//		{
		//			StartCoroutine(moveManager.Rotate(piece, 90));
		//			break;
		//		}
		//	}
		//}
	}
	public void TryRotateRight(GameObject piece)
	{
		Vector3 pos = piece.transform.position;
		int rotateOffset = 1;

		//if (!Collide4(rotate: -90))
		//{

		//	StartCoroutine(moveManager.Rotate(piece, -90));

		//}
		//else
		//{
		//	while (Collide4(rotate: -90))
		//	{

		//		piece.transform.position = piece.transform.position + new Vector3(rotateOffset, 0, 0);
		//		rotateOffset = -(rotateOffset + (rotateOffset > 0 ? 1 : -1));

		//		if (rotateOffset > pieceArray.GetLength(0))
		//		{
		//			piece.transform.position = pos;
		//			break;
		//		}

		//		if (!Collide4(rotate: -90))
		//		{
		//			StartCoroutine(moveManager.Rotate(piece, -90));
		//			break;
		//		}
		//	}
		//}
	}
}

