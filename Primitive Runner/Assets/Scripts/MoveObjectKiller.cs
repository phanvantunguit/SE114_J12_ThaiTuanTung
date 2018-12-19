using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectKiller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody> ().velocity = new Vector3 (0, GameController.VerticalVelo, GameController.GameSpeed);
	}
	void OnTriggerEnter(Collider other)
	{
		Destroy (other.gameObject);
	}
	void OnCollisionEnter(Collision other)
	{
		Destroy (other.gameObject);
	}
}
