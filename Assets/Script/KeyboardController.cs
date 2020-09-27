using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : Controller
{
	private GameObject Piece{ get; set; }

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {

		if (base.acceptInput & (Input.anyKey))
		{

			TryMove();
		}
		else
		{

			//time = 0;
		}
	}

	void TryMove()
	{
		if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
			base.TryMoveRight(Piece);
		else if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
			base.TryMoveLeft(Piece);
		else if (Input.GetKey("down") || Input.GetKeyDown("s"))
			base.TryMoveDown(Piece);
		else if (Input.GetKeyDown("q"))
			base.TryRotateLeft(Piece);
		else if (Input.GetKeyDown("e"))
			base.TryRotateRight(Piece);
	}
}
