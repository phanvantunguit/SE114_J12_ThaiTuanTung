using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour {

	void Start () {

	}

	//di chuyen Camera song song voi Sphere voi toc do bang GameController.GameSpeed (mac dinh la 4)
	void Update () {
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, GameController.VerticalVelo, GameController.GameSpeed);
	}
}
