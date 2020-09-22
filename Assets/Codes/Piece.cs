using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
	
	string[] pieceTypes = new string[] { "T", "O", "L", "J", "I", "S", "Z" };
	
	SpriteRenderer spriteRenderer;
	public int[,] pieceArray;
	public float xOff, yOff;

	private GameObject piece;
	private GameObject subPiece;

	float[,] colorCodes = new float[7,3] {	//canadian palette
	{238,82,83},		//rgb(238, 82, 83) kırmızı
	{72,219,251},			//rgb(72, 219, 251) açık mavi
	{95,39,205},			//rgb(95, 39, 205) koyu mavi
	{255,159,243},			//rgb(255, 159, 243) açık pembe
	{29,209,161},			//rgb(29, 209, 161) açık yeşil
	{254,202,87},			//rgb(254, 202, 87) sarı
	{131,149,167}			//rgb(131, 149, 167) gri
	};

	public GameObject CreatePiece(Sprite sprite)
	{
		piece = new GameObject("piece");
		int randInt = Random.Range(0, 7);
		pieceArray = CreatePieceArray(pieceTypes[randInt]);
		xOff = (pieceArray.GetLength(0) -1) / 2.0f;
		yOff = (pieceArray.GetLength(1) -1) / 2.0f;
		piece.transform.position = new Vector3(5f - xOff, 19f - yOff, -1);

		float h, s, v;
		int i = 1;
		for (int y = 0; y < pieceArray.GetLength(0); y++)
		{
			for (int x = 0; x < pieceArray.GetLength(1); x++)
			{
				if(pieceArray[y,x] != 0)
				{
					subPiece = new GameObject("subPiece"+i);
					spriteRenderer = subPiece.AddComponent<SpriteRenderer>();
					spriteRenderer.sprite = sprite;

					Color c = new Color(colorCodes[randInt,0], colorCodes[randInt, 1], colorCodes[randInt, 2], 1);
					Color.RGBToHSV(c, out h, out s, out v);
					spriteRenderer.color = new Color(h, s, v);


				}
				else
				{
					subPiece = new GameObject("eSubPiece" + i);

				}
				subPiece.transform.position = new Vector3(5f - x , 19f - y , -1);
				subPiece.transform.SetParent(piece.transform);
				i++;
			}
		}

		//GameObject[] subPieces = new GameObject[4];
		//subPieces[0] = Instantiate(new GameObject("subpiece"), new Vector3(1, 2), Quaternion.identity);
		//SpriteRenderer renderer = subPieces[0].AddComponent<SpriteRenderer>();
		//renderer.sprite = sprite;
		//subPieces[0].transform.SetParent(piece.transform);

		return piece;
	}

	public GameObject CreateClone()
	{
		return null;
	}
	private int[,] CreatePieceArray(string type)
	{
		if (type.Equals("T"))
			return new int[3, 3] {
				{0,0,0 },
				{1,1,1 },
				{0,1,0 },
			};
		else if (type.Equals("O"))
			return new int[2,2] {
				{2,2 },
				{2,2 },
			};
		else if (type.Equals("L"))
			return new int[3,3] {
				{0,3,0 },
				{0,3,0 },
				{0,3,3 },
			};
		else if (type.Equals("J"))
			return new int[3, 3] {
				{0,4,0 },
				{0,4,0 },
				{4,4,0 },
			};
		else if (type.Equals("I"))
			return new int[4, 4] {
				{0,5,0,0 },
				{0,5,0,0 },
				{0,5,0,0 },
				{0,5,0,0 },
			};
		else if (type.Equals("S"))
			return new int[3, 3] {
				{0,6,6 },
				{6,6,0 },
				{0,0,0 },
			};
		else if (type.Equals("Z"))
			return new int[3, 3] {
				{7,7,0 },
				{0,7,7 },
				{0,0,0 },
			};
		return null;
    }
}
