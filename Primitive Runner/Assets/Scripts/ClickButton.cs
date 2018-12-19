using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour {

	public Button Quit;
	public Button Replay;
	// Use this for initialization
	void Start () {
		Quit.onClick.AddListener (QuitOnClick);
		Replay.onClick.AddListener (ReplayOnClick);
	}

	void QuitOnClick()
	{
		GameController.isStart = 0;
		SceneManager.LoadScene ("MainScene");
	}

	void ReplayOnClick()
	{
		GameController.isStart = 1;
		SceneManager.LoadScene ("MainScene");
	}
}
