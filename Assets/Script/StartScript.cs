using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScript : MonoBehaviour
{
	

	Button startButton;

    // Start is called before the first frame update
    void Start()
    {

		startButton = GameObject.Find("StartButton").GetComponent<Button>();

		startButton.onClick.AddListener(StartButtonClick);

	}

    // Update is called once per frame
    void Update()
    {



	}

	

	void StartButtonClick()
	{
		SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
	}

	void DevamButtonClick()
	{

	}


}
