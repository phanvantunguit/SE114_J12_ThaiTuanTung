using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static float VerticalVelo; //do cao cua cac object trong game mac dinh la 0
	public static float GameSpeed; //toc do camera va sphere di chuyen
	public static float gameTime; //tong thoi gian game da choi
	public static float gameScore; //tong diem cua nguoi choi
	public static int TotalCoin; //tong so coin da thu thap
	public static int TotalCap; //tong so capsule thu duoc
	public static int isFail; //game co ket thuc chua
	public float waitforload; //doi vai giay sau khi game ket thuc thi hien thi ket qua
	public static float GreedSec;
	public static float InvulSec; 
	public static int isStart = 0;

	public GameObject StartBtn;
	public GameObject Exit;
	public Transform CameraZ;
	public Text Invultext;
	public Text Greedtext;
	public Text Scoretext; //text hien thi diem cua nguoi choi (thoi gian choi * toc do game)
	public Text Cointext; //text hien thi tong so coin da thu thap
	public Text Capsuletext; //text hien thi tong so capsule da thu thap
	public GameObject PitBlock; //san co ho' (co do dai la 6f)
	public GameObject Block; //san ko co ho' (co do dai la 5f)
	public Transform Ramp; //san chi co 1 lane giua (co do dai la 10f)
	public Transform Coin; //dong tien
	public Transform Capsule; //capsule tang toc do game + tang score
	public Transform Obstacle; //vat can
	public Transform Greed; //x2 coin trong 30s
	public Transform Invul; //bat tu trong 15s
	private float ZGamePos = 21f; //vi tri de tao floor

	void Start()
	{
		if (isStart == 0) {
			StartBtn.SetActive (true);
			Exit.SetActive (true);
			isFail = 0;
			TotalCoin = 0;
			TotalCap = 0;
			GameSpeed = 0f;
			VerticalVelo = 0.0f;
			gameTime = 0.0f;
			gameScore = 0;
			waitforload = 0;
			GreedSec = 0;
			InvulSec = 0;
		} else {
			StartBtn.SetActive (false);
			Exit.SetActive (false);
			isFail = 0;
			TotalCoin = 0;
			TotalCap = 0;
			GameSpeed = 4f;
			VerticalVelo = 0.0f;
			gameTime = 0.0f;
			gameScore = 0;
			waitforload = 0;
			GreedSec = 0;
			InvulSec = 0;
		}
	}
	void Update ()
	{
		if (isStart == 1) {
			//kiem tra xem game con hoat dong khong
			if (isFail != 1) {
				gameTime += Time.deltaTime; //cap nhat thoi gian game
				gameScore = gameTime * GameSpeed;
				if (GreedSec > 0) {
					Greedtext.text = "Greedy";
					GreedSec -= Time.deltaTime;
					if (GreedSec <= 0) {
						GreedSec = 0;
						Greedtext.text = "";
					}
				}
				if (InvulSec > 0) {
					InvulSec -= Time.deltaTime;
					if (InvulSec < 0)
						InvulSec = 0;
				}
			}
			Invultext.text = "Invulnerable time: " + InvulSec;
			Scoretext.text = "Score: " + Mathf.Round (gameScore);
			Capsuletext.text = "Capsules: " + TotalCap;
			Cointext.text = "Coins: " + TotalCoin;
			//tang toc cho game neu muon
			//	if (gameTime > 3)
			//		GameSpeed += 0.05f;

			//neu game ket thuc thi doi vai giay roi load man hinh ket qua
			if (isFail == 1)
				waitforload += Time.deltaTime;
			if (waitforload > 3) {
				SceneManager.LoadScene ("SceneComp");
			}

			//duoi day la cac cau lenh tao cac vat khi game dang chay
			if (ZGamePos - CameraZ.position.z <= 30) {
				int Randomfloor = Random.Range (0, 5);
				int limitObsM = 0;
				int limitObsR = 0;
				if (Randomfloor < 2) {
					Instantiate (Block, new Vector3 (0, 0, ZGamePos), Block.transform.rotation);

					//tao ngau nhien cho lane trai
					int RandomUp1 = Random.Range (0, 40);
					if (RandomUp1 <= 39 && RandomUp1 > 36) {
						Instantiate (Capsule, new Vector3 (-1, 1, ZGamePos), Capsule.rotation);
					}
					if (RandomUp1 <= 36 && RandomUp1 > 33) {
						Instantiate (Greed, new Vector3 (-1, 1, ZGamePos), Greed.rotation);
					}
					if (RandomUp1 == 32) {
						Instantiate (Invul, new Vector3 (-1, 1, ZGamePos), Invul.rotation);
					}
					if (RandomUp1 <= 31 && RandomUp1 > 20) {
						Instantiate (Coin, new Vector3 (-1, 1, ZGamePos), Coin.rotation);
					}
					if (RandomUp1 < 15) {
						Instantiate (Obstacle, new Vector3 (-1, 1, ZGamePos + 2), Obstacle.rotation);
						limitObsM = 15;
						limitObsR = 15;
					}

					//tao ngau nhien cho lane giua
					int RandomUp2 = Random.Range (limitObsM, 40);
					if (RandomUp2 <= 39 && RandomUp2 > 36) {
						Instantiate (Capsule, new Vector3 (0, 1, ZGamePos), Capsule.rotation);
					}
					if (RandomUp2 <= 36 && RandomUp2 > 33) {
						Instantiate (Greed, new Vector3 (0, 1, ZGamePos), Greed.rotation);
					}
					if (RandomUp2 == 32) {
						Instantiate (Invul, new Vector3 (0, 1, ZGamePos), Invul.rotation);
					}
					if (RandomUp2 <= 31 && RandomUp2 > 20) {
						Instantiate (Coin, new Vector3 (0, 1, ZGamePos), Coin.rotation);
					}
					if (RandomUp2 < 15) {
						Instantiate (Obstacle, new Vector3 (0, 1, ZGamePos + 2), Obstacle.rotation);
						limitObsR = 15;
					}

					//tao ngau nhien cho lane phai
					int RandomUp3 = Random.Range (limitObsR, 40);
					if (RandomUp3 <= 39 && RandomUp3 > 36) {
						Instantiate (Capsule, new Vector3 (1, 1, ZGamePos), Capsule.rotation);
					}
					if (RandomUp3 <= 36 && RandomUp3 > 33) {
						Instantiate (Greed, new Vector3 (1, 1, ZGamePos), Greed.rotation);
					}
					if (RandomUp3 == 32) {
						Instantiate (Invul, new Vector3 (1, 1, ZGamePos), Invul.rotation);
					}
					if (RandomUp3 <= 31 && RandomUp3 > 20) {
						Instantiate (Coin, new Vector3 (1, 1, ZGamePos), Coin.rotation);
					}
					if (RandomUp3 < 15) {
						Instantiate (Obstacle, new Vector3 (1, 1, ZGamePos + 2), Obstacle.rotation);
					}

					ZGamePos += 6;
				}
				if (Randomfloor == 4) {
					Instantiate (Ramp, new Vector3 (0, 0, ZGamePos), Ramp.rotation);
					ZGamePos += 10;

				}
				if (Randomfloor >= 2 && Randomfloor < 4) {
					Instantiate (PitBlock, new Vector3 (0, 0, ZGamePos), PitBlock.transform.rotation);

					//tao ngau nhien cho lane trai
					int RandomUp1 = Random.Range (0, 40);

					if (RandomUp1 <= 39 && RandomUp1 > 36) {
						Instantiate (Capsule, new Vector3 (-1, 1, ZGamePos), Capsule.rotation);
					}
					if (RandomUp1 <= 36 && RandomUp1 > 33) {
						Instantiate (Greed, new Vector3 (-1, 1, ZGamePos), Greed.rotation);
					}
					if (RandomUp1 == 32) {
						Instantiate (Invul, new Vector3 (-1, 1, ZGamePos), Invul.rotation);
					}
					if (RandomUp1 <= 31 && RandomUp1 > 20) {
						Instantiate (Coin, new Vector3 (-1, 1, ZGamePos), Coin.rotation);
					}
					if (RandomUp1 < 15) {
						Instantiate (Obstacle, new Vector3 (-1, 1, ZGamePos + 2), Obstacle.rotation);
						limitObsM = 15;
						limitObsR = 15;
					}

					//tao ngau nhien cho lane giua
					int RandomUp2 = Random.Range (limitObsM, 40);
					if (RandomUp2 <= 39 && RandomUp2 > 36) {
						Instantiate (Capsule, new Vector3 (0, 1, ZGamePos), Capsule.rotation);
					}
					if (RandomUp2 <= 36 && RandomUp2 > 33) {
						Instantiate (Greed, new Vector3 (0, 1, ZGamePos), Greed.rotation);
					}
					if (RandomUp2 == 32) {
						Instantiate (Invul, new Vector3 (0, 1, ZGamePos), Invul.rotation);
					}
					if (RandomUp2 <= 31 && RandomUp2 > 20) {
						Instantiate (Coin, new Vector3 (0, 1, ZGamePos), Coin.rotation);
					}
					if (RandomUp2 < 15) {
						Instantiate (Obstacle, new Vector3 (0, 1, ZGamePos + 2), Obstacle.rotation);
						limitObsR = 15;
					}

					//tao ngau nhien cho lane phai
					int RandomUp3 = Random.Range (limitObsR, 40);
					if (RandomUp3 <= 39 && RandomUp3 > 36) {
						Instantiate (Capsule, new Vector3 (1, 1, ZGamePos), Capsule.rotation);
					}
					if (RandomUp3 <= 36 && RandomUp3 > 33) {
						Instantiate (Greed, new Vector3 (1, 1, ZGamePos), Greed.rotation);
					}
					if (RandomUp3 == 32) {
						Instantiate (Invul, new Vector3 (1, 1, ZGamePos), Invul.rotation);
					}
					if (RandomUp3 <= 31 && RandomUp3 > 20) {
						Instantiate (Coin, new Vector3 (1, 1, ZGamePos), Coin.rotation);
					}
					if (RandomUp3 < 15) {
						Instantiate (Obstacle, new Vector3 (1, 1, ZGamePos + 2), Obstacle.rotation);
					}
					ZGamePos += 6;
				}

			}
		}

	}

}
