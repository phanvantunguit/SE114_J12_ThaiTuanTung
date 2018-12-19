using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

	public Button StartBtn;
	public Button Exit;

	void Start()
	{
		StartBtn.onClick.AddListener (StartGame);
		Exit.onClick.AddListener (ExitGame);
	}
		
	void StartGame()
	{
		GameController.isStart = 1;
		StartBtn.gameObject.SetActive (false);
		Exit.gameObject.SetActive (false);
		GameController.GameSpeed = 4f;
	}

	void ExitGame()
	{
		Application.Quit ();
	}
}
