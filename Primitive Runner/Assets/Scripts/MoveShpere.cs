using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveShpere : MonoBehaviour {

	public AudioSource Audio;
	public AudioClip Boom;
	public AudioClip Coin;
	public AudioClip Cap;
	public ParticleSystem explode;
	/*
	//cac bien can dung khi di chuyen bang phim
	private float HorizontalMove = 0.0f; //bien di chuyen shpere khi nguoi dung bam nut

	private int LaneNumber = 0; //cho biet vi tri hien tai cua sphere voi -1, 0, 1 lan luot la lane trai, giua, phai

	private int LockControl = 0;
	*/
	[SerializeField]
	//2 bien keycode de di chuyen shpere
	public KeyCode Moveleftkey = KeyCode.LeftArrow; 
	public KeyCode Moverightkey = KeyCode.RightArrow;


	void Start () {
	}
	
	/*
	void Update () {
		//phan di chuyen bang phim khi ko dung dien thoai

		//ban dau cho shpere di chuyen song song voi camera
		GetComponent<Rigidbody> ().velocity = new Vector3 (HorizontalMove, GameController.VerticalVelo, GameController.GameSpeed);

		//kiem tra nguoi dung nhan nut
		if (Input.GetKeyDown (Moveleftkey) && LaneNumber != -1 && HorizontalMove == 0 && LockControl != 1) 
		//neu nhan nut trai thi kiem tra co phai dang nam o lane trai ko neu ko thi
		//kiem tra horizontalmove = 0 de phong truong hop nguoi dung nhan hai nut trai phai cung luc lam sphere lech vi tri
 		
		{		
			HorizontalMove = -2f; //di chuyen sang trai
			LaneNumber = LaneNumber - 1; //do di chuyen sang trai nen lane giam 1 don vi
			LockControl = 1;
			StartCoroutine (Stopslide ()); //doi 0,25s
		}

		//tuong tu doan code tren nhung chuyen huong ben phai
		if (Input.GetKeyDown (Moverightkey) && LaneNumber != 1 && HorizontalMove == 0 && LockControl != 1) {
			HorizontalMove = 2f;
			LaneNumber = LaneNumber + 1;
			LockControl = 1;
			StartCoroutine (Stopslide ());		
		}

	}

	//ham co chuc nang doi 0,25s sau do thi cho Sphere di thang tro lai
	IEnumerator Stopslide()
	{
		yield return new WaitForSeconds (0.5f); //doi 0,25s
		HorizontalMove = 0; //cho shpere di chuyen thang? tro lai
		LockControl = 0;
	}
*/


	//khi co va cham thi cho huy? Sphere
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Lethal") {
			if (GameController.InvulSec <= 0) {
				Audio.clip = Boom;
				Audio.Play ();
				Destroy (gameObject);
				GameController.GameSpeed = 0;
				Instantiate (explode, transform.position, explode.transform.rotation);
				GameController.isFail = 1;
			} else {
				Destroy (other.gameObject);
			}
		}
	}

	//xu ly cac va cham voi Trigger
	void OnTriggerEnter(Collider other)
	{

		//khi va cham chay voi cac do vat PowerUp thi xoa PowerUp truoc, roi thuc hien
		//chuc nang cua tung PowerUp tuy thuoc vao ten cua PowerUp
		if (other.gameObject.tag == "Capsule") {
			GameController.GameSpeed++;
			//doan code sau gioi han toc do choi cao nhat cua game
			//luc nay capsule chi tang score chu khong tang toc do nua
			if (GameController.GameSpeed > 10f) {
				GameController.GameSpeed = 10f;
				GameController.gameScore += 100;
			}
			Audio.clip = Cap;
			Audio.Play ();
			Destroy (other.gameObject);
			GameController.TotalCap++;
		}
		if (other.gameObject.tag == "Coin") {
			Audio.clip = Coin;
			Audio.Play ();
			Destroy (other.gameObject);
			if (GameController.GreedSec > 0)
				GameController.TotalCoin += 2;
			else
				GameController.TotalCoin++;
		}
		if (other.gameObject.tag == "Greed") {
			Audio.clip = Coin;
			Audio.Play ();
			Destroy (other.gameObject);
			GameController.GreedSec += 10f;
			if (GameController.GreedSec > 30)
				GameController.GreedSec = 30;
		}
		if (other.gameObject.tag == "Invul") {
			Audio.clip = Cap;
			Audio.Play ();
			Destroy (other.gameObject);
			GameController.InvulSec += 10f;
			if (GameController.InvulSec > 30)
				GameController.InvulSec = 30;
		}

		//ngoai ra khi va cham voi cac trigger bay~ thi huy Sphere
		if (other.gameObject.tag == "Lethal") {
			if (GameController.InvulSec <= 0) {
				Audio.clip = Boom;
				Audio.Play ();
				Destroy (gameObject);
				GameController.GameSpeed = 0;
				Instantiate (explode, transform.position, explode.transform.rotation);
				GameController.isFail = 1;
			} else {
				Destroy (other.gameObject);
			}
		

			//cham cong thi cho finish
			if (other.gameObject.tag == "Finish") {
				SceneManager.LoadScene ("SceneComp");
			}
		}
	}
}
