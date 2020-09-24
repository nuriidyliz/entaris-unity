using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Controller
{
	public GameObject bottomPanel;
	public Button leftButton;
	public Button rightButton;
	public Button rightRotateButton;
	public Button leftRotateButton;

	private bool ButtonClicked { get; set; } = false;
	private bool LeftButtonClicked { get; set; } = false;
	private bool RightButtonClicked { get; set; } = false;
	private bool RightRotateButtonClicked { get; set; } = false;
	private bool LeftRotateButtonClicked { get; set; } = false;

	// Start is called before the first frame update
	void Start()
	{
		SetButtonListeners();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void TryMove()
	{

	}

	void BottomButtonsListener(string type)
	{
		ButtonClicked = true;
		switch (type)
		{
			case "left":
				LeftButtonClicked = true;
				break;
			case "right":
				RightButtonClicked = true;
				break;
			case "rightRotate":
				RightRotateButtonClicked = true;
				break;
			case "leftRotate":
				LeftRotateButtonClicked = true;
				break;

		}
	}

	void SetButtonListeners()
	{
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
	}
}
