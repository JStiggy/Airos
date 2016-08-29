using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private int score = 0; // Total points earned by the player in the current round 
	private float speedMultiplier = 3; // Also serves as the score multiplier
	private int shiftCount = 0; //Number of times the player has shifted the scroll direction, slowly increases the games current speed;
	private int timeToShift = 0; //Value determining how long the player has been in the current shift
	private int scrollDirection = 0; //Current direction of the camera 1:Right 2:Down 3:Left 4:Up
	private Vector3 scrollVector; //Vector for scrolling the camera in a specified direction
	private int speedUpCount = 2;

	public GameObject pChar; //Player caracter object
	public GameObject[] spawners; //All 4 spawners on the map
	public GameObject[] speedUpIndicator;
	private GameObject sInd;
	public Image chargeBar;
	public Text scoreText;
	private bool endGame = false;

	public AudioSource sManager;
	public AudioClip[] sfxList;
	private AudioSource mManager;

	void Start () {
		//Start increasing the game speed, score and begin moving the camera right
		InvokeRepeating ("IncrementData",1f,1f);
		Invoke ("StartSpeedUp", 10f);
		scrollVector = new Vector3 (1,0,0);
		chargeBar.rectTransform.localScale = new Vector3 (0f, .25f, 1f);
		mManager = GameObject.Find ("MusicSource").GetComponent<AudioSource> ();
		mManager.clip = mManager.GetComponentInParent<MusicManager> ().musicList [1];
		mManager.volume = .08f;
		mManager.Play ();
	}

	void Update() {
		//Shift the camera if the game is not paused, and 35 seconds has passed and the button is pressed
		if (Input.GetButtonUp("Shift Direction") && timeToShift >= 20 && Time.timeScale != 0){
			Time.timeScale = .5f;
			Invoke ("ResetTimeScale", 1f);
			sManager.clip = sfxList [0];
			sManager.Play ();
			ShiftDirection ();
			speedMultiplier = 3f + 1f * shiftCount;
			timeToShift = 0;
			chargeBar.rectTransform.localScale = new Vector3 (0f, .25f, 0f);
		}
		//Pause the game if the pause button is pressed
		if (Input.GetButtonUp("Pause")){
			Time.timeScale = 1 - Time.timeScale;
		}
		//End the game if the player is a certain distance from the camera
		if (Vector3.Distance (gameObject.transform.position, pChar.transform.position) > 19 && !endGame) {
			sManager.clip = sfxList [1];
			sManager.Play ();
			endGame = true;
			Invoke("EndGame",1f);
		}
	}
		
	void FixedUpdate(){
		//Scroll the camera
		gameObject.transform.Translate (scrollVector / 60 * speedMultiplier);
	}
	
	void IncrementData(){
		//Increase the game speed, the player score, and the coutdown timer
		score += (int)(10 *( Mathf.Pow(1.01f, (float)timeToShift) + .1f * shiftCount));
		timeToShift ++;
		//speedMultiplier += .2f;

		if (chargeBar.rectTransform.localScale.x >= .95f) {
			chargeBar.rectTransform.localScale = new Vector3 (.95f, .25f, 0f);
		} else {
			chargeBar.rectTransform.localScale = new Vector3 (chargeBar.rectTransform.localScale.x + .0475f, .25f, 0f);
		}
		scoreText.text = score.ToString ();
	}

	void ResetTimeScale(){
		Time.timeScale = 1f;
	}

	void StartSpeedUp(){
		CancelInvoke ("StartSpeedUp");
		sInd = (GameObject)Instantiate (speedUpIndicator[scrollDirection], new Vector3(0f ,0f, 8f), Quaternion.identity);
		sInd.transform.parent = this.gameObject.transform;
		sInd.transform.localPosition = new Vector3 (0f, 0f, 20f);
		InvokeRepeating ("SpeedUp", 1f, 1f);
		speedUpCount = 2;
	}

	void SpeedUp(){
		speedUpCount -= 1;
		speedMultiplier += .6f;
		if(speedUpCount == 0){
			Destroy (sInd);
			CancelInvoke ("SpeedUp");
			Invoke ("StartSpeedUp", 10f);
		}
	}

	public void EndGame() {
		//Save the score and load the game over screen
		Time.timeScale = 1f;
		PlayerPrefs.SetInt ("CurrentScore", score);
		SceneManager.LoadScene(3);
	}

	void ShiftDirection(){
		spawners[scrollDirection].SetActive(false); //Disable the current spawner
		scrollDirection += Random.Range (0, 2) + 1; //Add 1 to 3 to the curernt scroll direction
		scrollDirection = scrollDirection % 4; //Take the remainder from 4
		spawners[scrollDirection].SetActive(true); //Enable the new direction
		spawners[scrollDirection].GetComponent<Spawner>().SpawnPlatformOnce();
		pChar.GetComponent<SpriteRenderer>().color = pChar.GetComponent<PlayerController> ().SetColor (); //Set the player color
		//Take the scroll direction and set scroll vector to represent the new direction
		if(scrollDirection == 3) {
			scrollVector = Vector3.up;
			Camera.main.backgroundColor = new Color (132/255f, 0f, 149/255f);
		} else if (scrollDirection == 2){
			scrollVector = Vector3.left;
			Camera.main.backgroundColor = new Color (1f, 166/255f , 33/255f);
		} else if (scrollDirection == 1){
			scrollVector = Vector3.down;
			Camera.main.backgroundColor = new Color (8/255f, 253/255f, 0f);
		} else {
			scrollVector = Vector3.right;
			Camera.main.backgroundColor = new Color (1f, 0f, 0f);
		}
		CancelInvoke ("SpeedUp");
		CancelInvoke("StartSpeedUp");
		Invoke ("StartSpeedUp", 10f);
	}

	public int getShiftCount(){
		return shiftCount;
	}

	public int getScrollDirection(){
		return scrollDirection;
	}

	public int getTimeToShift(){
		return timeToShift;
	}

	//Allow for external objects to modify the score(Ex: Bonus Platforms)
	public void increaseScore(int val){
		score += val;
	}

	public float GetSpeedMultiplier(){
		return speedMultiplier;
	}

	public Vector3 GetSpeedVector(){
		return scrollVector / 60 * speedMultiplier;
	}
}
