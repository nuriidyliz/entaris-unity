using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
	bool Collide4(GameObject piece, int[,] area, int dirX = 0, int dirY = 0, int rotate = 0)
	{
		int x, y;
		for (int i = 0; i < piece.transform.childCount; i++)
		{
			if (piece.transform.GetChild(i).name.Contains("eSub"))   //eSubPiece .. empty Sub piece
				continue;
			else
			{
				if (rotate == 0)
				{
					x = (int)(Mathf.Round(piece.transform.GetChild(i).transform.position.x) + dirX);
					y = (int)(Mathf.Round(piece.transform.GetChild(i).transform.position.y) + dirY);
				}
				else
				{
					y = (int)(Mathf.Round(piece.transform.GetChild(i).transform.position.y) + dirY);

					if (y <= 1)
						return true;

					piece.transform.rotation = Quaternion.Euler(0, 0, piece.transform.rotation.eulerAngles.z + rotate);
					bool isCollide = Collide4(piece, area);
					if (isCollide)
					{
						piece.transform.rotation = Quaternion.Euler(0, 0, piece.transform.rotation.eulerAngles.z - rotate);
						return true;
					}
					piece.transform.rotation = Quaternion.Euler(0, 0, piece.transform.rotation.eulerAngles.z - rotate);

					return false;
				}

				if (x >= 0 && x <= 9 && y >= 0)
				{
					if (area[y, x] != 0)
						return true;
				}
				else
					return true;

			}

		}
		return false;
	}

	public bool Collide3(GameObject piece, int[,] area)
	{
		int x, y;
		for (int i = 0; i < piece.transform.childCount; i++)
		{
			if (piece.transform.GetChild(i).name.Contains("eSub"))   //eSubPiece .. empty Sub piece
				continue;
			else
			{
				x = (int)(Mathf.Round(piece.transform.GetChild(i).transform.position.x));
				y = (int)(Mathf.Round(piece.transform.GetChild(i).transform.position.y));

				if (x >= 0 && x <= 9 && y >= 0)
				{
					if (area[y, x] != 0)
						return true;
				}
				else
					return true;
			}
		}
		return false;
	}
	bool Collide2(GameObject piece, int[,] area)
	{
		int x, y;
		for (int i = 0; i < piece.transform.childCount; i++)
		{
			if (piece.transform.GetChild(i).name.Contains("eSub"))   //eSubPiece .. empty Sub piece
				continue;
			else
			{
				x = (int)(Mathf.Round(piece.transform.GetChild(i).transform.position.x));
				y = (int)(Mathf.Round(piece.transform.GetChild(i).transform.position.y));

				if (x >= 0 && x <= 9)
				{
					if (area[y, x] != 0)
						return true;
				}
				else
					return true;
			}
		}
		return false;
	}
	string Collide(GameObject piece, int[,] area, string type = "", int dirX = 0)
	{
		int x, y;
		string result = "";
		for (int i = 0; i < piece.transform.childCount; i++)
		{
			if (piece.transform.GetChild(i).name.Contains("eSub"))   //eSubPiece .. empty Sub piece
			{
				continue;

			}
			else
			{

				x = (int)(Mathf.Round(piece.transform.GetChild(i).transform.position.x) + dirX);
				y = (int)(Mathf.Round(piece.transform.GetChild(i).transform.position.y));



				if (type.Equals("side"))

					result = (x < 0) ? "isLeftSide" : (x > 9) ? "isRightSide" : "false";        //collide side walls
				else
					result = (y < 1 - Mathf.Epsilon) || (area[y - 1, x] != 0) ? "collision" : "false";  //	collide bottom



				if (!result.Equals("false"))
					return result;
				else
					if (area[y, x] != 0)
					return "isLeftRightSide";               //while move right or left, collide with another tetromino
			}

		}
		return result;      //false
	}
}
