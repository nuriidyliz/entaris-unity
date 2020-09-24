using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	//public GameObject pieceAsset;
	public Sprite sprite;
	public Text scoreText;
	public Text lastScoreText;
	public GameObject gameOverPanel;
	public Button retryButton;
	public Button backToMenuButton;
	public Button backToMenuButton2;
	public GameObject settingsPanel;
	public Button retryButton2;
	public Button settingsButton;
	public Toggle toggle;

	public GameObject bottomPanel;
	public Button leftButton;
	public Button rightButton;
	public Button rightRotateButton;
	public Button leftRotateButton;
	public Button resumeButton;
	public bool leftButtonClicked = false;
	public bool rightButtonClicked = false;
	public bool rightRotateButtonClicked = false;
	public bool leftRotateButtonClicked = false;
	public bool buttonClicked = false;

	private GameObject piece;
	private int[,] pieceArray;
	private float xOff, yOff;

	private Rigidbody2D rb2D;

	private int[,] area = new int[20,10];
	private GameObject[,] subPieces = new GameObject[20,10];

	private float downTime = 0.2f;
	private float time = 0f;
	private float time2 = 0f;
	private float downTime2 = 0.08f;

	private Touch touch;

	private float minDistanceForSwipe = 20f;
	private Vector2 fingerDownPosition;
	private Vector2 fingerUpPosition;


	
	private Piece pieceManager= new Piece();

	private float lerpDuration = 3;

	private int score = 0;

	private bool acceptInput = true;

	private MoveManager moveManager;
	// Start is called before the first frame update
	void Start()
	{
		moveManager = new MoveManager();
		FillAreaArray();

		CreatePiece();
		//Debug.Log(piece.transform.childCount);

		retryButton.onClick.AddListener(RetryButtonListener);
		retryButton2.onClick.AddListener(RetryButtonListener);
		backToMenuButton.onClick.AddListener(BackToMenuButtonListener);
		backToMenuButton2.onClick.AddListener(BackToMenuButtonListener);
		settingsButton.onClick.AddListener(SettingsButtonListener);
		resumeButton.onClick.AddListener(ResumeButtonListener);
		leftButton.onClick.AddListener(delegate
		{
			BottomButtonsListener("left");
		});
		rightButton.onClick.AddListener(delegate {
			BottomButtonsListener("right");
		});
		leftRotateButton.onClick.AddListener(delegate
		{
			BottomButtonsListener("leftRotate");
		});
		rightRotateButton.onClick.AddListener(delegate
		{
			BottomButtonsListener("rightRotate");
		});
		toggle.onValueChanged.AddListener(delegate
		{
			ToggleValueChanged();
		});

		toggle.isOn = false;


		InvokeRepeating("Drop", 1, 1);

	}

	void Update()
	{

		if (acceptInput & (Input.anyKey || Input.touchCount > 0 || buttonClicked))
		{

			//Move();
			//touch = Input.GetTouch(0);
			//MoveMobil();
		}
		else
		{

			time = 0;
		}
		buttonClicked = false;
	}

	void ArenaSweep()
	{
		bool isFull;
		for(int y = area.GetLength(0)-1; y>=0; y--)
		{
			isFull = true;

			for(int x = 0; x < area.GetLength(1); x++)
			{
				if(area[y,x] == 0)
				{
					isFull = false;
					break;
				}
			}
			if (isFull)
			{
				score += 10;
				Sweep(y);
			}
		}
	}

	void Sweep(int y)
	{

		for(int x=0; x < area.GetLength(1); x++)
		{
			area[y, x] = 0;
			Destroy(subPieces[y, x]);
		}
		UpdateArea(y);
	}

	void UpdateArea(int sweepLine)
	{

		for (int y = sweepLine + 1; y < area.GetLength(0) - 1; y++)
		{
			for (int x = 0; x < area.GetLength(1); x++)
			{
				if(area[y,x] != 0)
				{

					area[y - 1, x] = 1;
					area[y, x] = 0;
					subPieces[y, x].transform.position = new Vector3(x, y-1,-1);
					subPieces[y - 1, x] = subPieces[y, x].gameObject;
					subPieces[y, x] = null;
				}
			}
		}
	}

	bool Collide4(int dirX = 0, int dirY = 0, int rotate = 0)
	{
		int x, y;
		for (int i = 0; i < piece.transform.childCount; i++)
		{
			if (piece.transform.GetChild(i).name.Contains("eSub"))   //eSubPiece .. empty Sub piece
				continue;
			else
			{
				if(rotate == 0)
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
					bool isCollide = Collide4();
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

	public bool Collide3()
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
	bool Collide2()
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
	string Collide(string type = "", int dirX = 0)
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
				y = (int)(Mathf.Round(piece.transform.GetChild(i).transform.position.y) ) ;



				if (type.Equals("side"))
					
					result = (x < 0) ? "isLeftSide" : (x > 9) ? "isRightSide" : "false";		//collide side walls
				else 
					result = (y < 1 - Mathf.Epsilon ) || (area[y - 1 , x] != 0) ? "collision" : "false";	//	collide bottom



				if (!result.Equals("false"))
					return result;
				else
					if (area[y , x] != 0)
						return "isLeftRightSide";				//while move right or left, collide with another tetromino
			}
	
		}
		return result;		//false
	}



	void CreatePiece()
	{
		piece = pieceManager.CreatePiece(sprite);
		rb2D = piece.AddComponent<Rigidbody2D>();
		rb2D.isKinematic = true;
		pieceArray = pieceManager.pieceArray;
		xOff = pieceManager.xOff;
		yOff = pieceManager.yOff;

		scoreText.text = "Score: " + score  ;

		if (Collide2())
		{
			lastScoreText.text = "Your score is " + score;
			gameOverPanel.SetActive(true);
			CancelInvoke("Drop");
			acceptInput = false;

		}
	}


	void Drop()
	{
		if (Collide2())
			return;
		if(Collide().Equals("collision"))
		{

			Merge();
			ArenaSweep();
			CreatePiece();
		}
		else
		{

			moveManager.MoveDown(piece);
		}
	}

	void FillAreaArray()
	{
		for (int x = 0; x < area.GetLength(0); x++)
		{
			for (int y = 0; y < area.GetLength(1); y++)
			{
				area[x, y] = 0;
			}
		}
	}

	void Merge()
	{
		int x, y;
		for (int i = 0; i < piece.transform.childCount; i++)
		{
			if (piece.transform.GetChild(i).name.Contains("eSub")) //eSubPiece .. empty Sub piece
			{
				continue;
			}


			x = (int)Mathf.Round(piece.transform.GetChild(i).transform.position.x);
			y = (int)Mathf.Round(piece.transform.GetChild(i).transform.position.y);
			area[y, x] = 1;
			subPieces[y, x] = piece.transform.GetChild(i).gameObject;

		}
	}

	void Move()
	{
		int rotateOffset = 1;
		float angle = piece.transform.rotation.eulerAngles.z;
		Vector3 pos;
		if (Input.GetKeyDown("right") || Input.GetKeyDown("d") || rightButtonClicked)
		{
			rightButtonClicked = false;

			moveManager.MoveHorizontal(piece, Direction.Right);

			if (Collide3())
			{
				moveManager.MoveHorizontal(piece, Direction.Left);
			}

		}
		else if (Input.GetKeyDown("left") || Input.GetKeyDown("a") || leftButtonClicked)
		{
			leftButtonClicked = false;
			moveManager.MoveHorizontal(piece, Direction.Left);

			if (Collide3())
			{
				moveManager.MoveHorizontal(piece, Direction.Right);
			}

		}
		else if (Input.GetKey("down") || Input.GetKeyDown("s") )
		{
			time = time + Time.deltaTime;
			if(time > downTime)
			{
				time2 += Time.deltaTime;

				if (time2 > downTime2)
				{

					Drop();
					time2 = 0;

				}

			}
			
		}
		else if (Input.GetKeyDown("q") || leftRotateButtonClicked)
		{
			leftRotateButtonClicked = false;
			pos = piece.transform.position;

			if(!Collide4(rotate: 90))
			{
				StartCoroutine(moveManager.Rotate(piece,90));

			}
			else
			{
				while (Collide4(rotate: 90))                                                      //calisan
				{

					piece.transform.position = piece.transform.position + new Vector3(rotateOffset, 0, 0);
					rotateOffset = -(rotateOffset + (rotateOffset > 0 ? 1 : -1));

					if (rotateOffset > pieceArray.GetLength(0))
					{
						piece.transform.position = pos;
						break;
					}

					if (!Collide4(rotate: 90))
					{
						StartCoroutine(moveManager.Rotate(piece,90));
						break;
					}
				}
			}


			angle = piece.transform.rotation.eulerAngles.z;


		}
		else if (Input.GetKeyDown("e") || rightRotateButtonClicked)
		{
			#region CALISAN DONUS

			//pos = piece.transform.position;

			//piece.transform.rotation = Quaternion.Euler(0f, 0f, angle-90f);
			//angle = piece.transform.rotation.eulerAngles.z;

			//while (Collide3())
			//{
			//	piece.transform.position = piece.transform.position + new Vector3(rotateOffset, 0, 0);
			//	rotateOffset = -(rotateOffset + (rotateOffset > 0 ? 1 : -1));

			//	if (rotateOffset > pieceArray.GetLength(0))
			//	{
			//		piece.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
			//		piece.transform.position = pos;
			//		break;
			//	}

			//}
			#endregion
			rightRotateButtonClicked = false;

			pos = piece.transform.position;

			if (!Collide4(rotate: -90))
			{

				StartCoroutine(moveManager.Rotate(piece,-90));

			}
			else
			{
				while (Collide4(rotate: -90))                                                     
				{

					piece.transform.position = piece.transform.position + new Vector3(rotateOffset, 0, 0);
					rotateOffset = -(rotateOffset + (rotateOffset > 0 ? 1 : -1));

					if (rotateOffset > pieceArray.GetLength(0))
					{
						piece.transform.position = pos;
						break;
					}

					if (!Collide4(rotate: -90))
					{
						StartCoroutine(moveManager.Rotate(piece,-90));
						break;
					}
				}
			}
		}
	}

	IEnumerator DropMove(Vector3 current, Vector3 target)
	{
		float timeElapsed = 0;

		while (timeElapsed < 0.20f)
		{

			piece.transform.position = Vector3.Lerp(piece.transform.position, target, 60 * Time.deltaTime);
			timeElapsed += Time.deltaTime;
			acceptInput = false;
			yield return null;
		}
		acceptInput = true;
		yield return null;
	}
	IEnumerator Rotate2(float angle)
	{
		float step = 15;
		if(angle < 0)
		{
			angle = -1 * angle;
			step = -1 * step;
		}

		while(angle > 0)
		{
			piece.transform.rotation = Quaternion.Euler(0, 0, piece.transform.rotation.eulerAngles.z + step);
			angle -= 15;

			yield return null;
		}
		yield return null;


	}


	void MoveMobil() {
#region SwipeMove
		Vector2  pos;
		int rotateOffset = 1;
		time += Time.deltaTime;
		float angle = piece.transform.rotation.eulerAngles.z;

		if (touch.phase == TouchPhase.Began)
		{
			fingerDownPosition = touch.position;
			fingerUpPosition = touch.position;
		}

		if (touch.phase == TouchPhase.Ended)
		{
			fingerUpPosition = touch.position;
		}

		if ((SwipeDistanceCheckMet() && !IsVerticalSwipe()) || leftButtonClicked || rightButtonClicked)      //is swipe? and move right or left
		{
			if (SwipeDirection().x > 0 || rightButtonClicked)
			{
				rightButtonClicked = false;
				if (!Collide(type: "side", dirX: 1).Contains("Right"))
				{
					piece.transform.position = piece.transform.position + new Vector3(1, 0, 0);
				}

			}
			else
			{
				leftButtonClicked = false;
				if (!Collide("side", dirX: -1).Contains("Left"))
				{
					piece.transform.position = piece.transform.position + new Vector3(-1, 0, 0);
				}
			}

		}
		else if((SwipeDistanceCheckMet() && IsVerticalSwipe()) || rightRotateButtonClicked ||leftRotateButtonClicked)
		{
			if (IsRotateLeft() || leftRotateButtonClicked)
			{
				leftRotateButtonClicked = false;
				#region CALISAN MOBIL SAG
				//piece.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
				//angle = piece.transform.rotation.eulerAngles.z;

				//if (Collide2())
				//{
				//	piece.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
				//}
				#endregion
				pos = piece.transform.position;

				if (!Collide4(rotate: 90))
				{
					StartCoroutine(Rotate2(90));
				}
				else
				{
					while (Collide4(rotate: 90))
					{

						piece.transform.position = piece.transform.position + new Vector3(rotateOffset, 0, 0);
						rotateOffset = -(rotateOffset + (rotateOffset > 0 ? 1 : -1));

						if (rotateOffset > pieceArray.GetLength(0))
						{
							piece.transform.position = pos;
							break;
						}

						if (!Collide4(rotate: 90))
						{
							StartCoroutine(Rotate2(90));
							break;
						}
					}
				}
			}
			else
			{
				rightRotateButtonClicked = false;
				#region CALISAN MOBIL
				//piece.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
				//angle = piece.transform.rotation.eulerAngles.z;

				//if (Collide2())
				//{
				//	piece.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
				//}

				#endregion
				pos = piece.transform.position;

				if (!Collide4(rotate: -90))
				{
					StartCoroutine(Rotate2(-90));
				}
				else
				{
					while (Collide4(rotate: -90))
					{

						piece.transform.position = piece.transform.position + new Vector3(rotateOffset, 0, 0);
						rotateOffset = -(rotateOffset + (rotateOffset > 0 ? 1 : -1));

						if (rotateOffset > pieceArray.GetLength(0))
						{
							piece.transform.position = pos;
							break;
						}

						if (!Collide4(rotate: -90))
						{
							StartCoroutine(Rotate2(-90));
							break;
						}
					}
				}
			}

		}

#endregion

		if (touch.position.y > 200 && time > downTime)							//touch to drop piece
		{
			time2 += Time.deltaTime;
			if(time2 > downTime2)
			{
				Drop();
				time2 = 0;
			}
		}



	}

	public bool IsVerticalSwipe()
	{
		return VerticalMovementDistance() > HorizontalMovementDistance();
	}

	public bool IsRotateLeft()
	{
		return (fingerUpPosition.x > Screen.width / 2 && SwipeDirection().y > 0) || (fingerUpPosition.x < Screen.width / 2 && SwipeDirection().y < 0);
	}

	public bool SwipeDistanceCheckMet()
	{
		return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
	}

	public float VerticalMovementDistance()
	{
		return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
	}

	public float HorizontalMovementDistance()
	{
		return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);

	}

	public Vector2 SwipeDirection()
	{
		return fingerUpPosition - fingerDownPosition;
	}

	void RetryButtonListener()
	{
		SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
	}

	void BackToMenuButtonListener()
	{
		SceneManager.LoadScene("Start", LoadSceneMode.Single);
	}

	void SettingsButtonListener()
	{
		CancelInvoke("Drop");
		settingsPanel.SetActive(true);
		acceptInput = false;
	}

	void ResumeButtonListener()
	{
		settingsPanel.SetActive(false);
		InvokeRepeating("Drop", 1, 1);
		StartCoroutine(WaitInput(0.05f));
	}

	void ToggleValueChanged()
	{
		if (toggle.isOn)
			bottomPanel.SetActive(true);
		else
			bottomPanel.SetActive(false);

	}

	void BottomButtonsListener(string type)
	{
		buttonClicked = true;
		switch (type)
		{
			case "left":
				leftButtonClicked = true;
				break;
			case "right":
				rightButtonClicked= true;
				break;
			case "rightRotate":
				rightRotateButtonClicked = true;
				break;
			case "leftRotate":
				leftRotateButtonClicked = true;
				break;

		} 
	}

	IEnumerator WaitInput(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		acceptInput = true;
	}
}
