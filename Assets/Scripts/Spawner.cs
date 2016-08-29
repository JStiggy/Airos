using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameManager gManager;
	public GameObject[] platforms;
	public GameObject[] backgrounds;
	public GameObject player;
	public int horiz = 0;
	public int vert = 0;
	private float invokeCount = 0f;

	void OnEnable(){
		//Start spawning platforms after a minor delay, decreasing as the number of shifts increases
		Invoke ("SpawnPlatform",Mathf.Max(1.5f- (float)gManager.getShiftCount()*.1f,0f));
		invokeCount = Mathf.Min(1f,(float)gManager.getShiftCount()*.1f);
		InvokeRepeating ("SpeedUp", 10f, 10f);
	}

	void OnDisable(){
		//Stop spawmning platforms
		CancelInvoke ("SpawnPlatform");
		CancelInvoke ("SpeedUp");
	}
		
	void SpeedUp(){
		invokeCount += .1f;
	}

	void SpawnPlatform(){
		//These vectors will determin which vertical and horizontal section of the screen
		//the player is on based on the current camera position and player position
		Vector3 edgeTopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 1f, 0f));
		Vector3 edgeBotLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, 0f));
		Vector3 spawnLocation; // Location to spawn a platform
		float spawnCiel; //Max value for a platform to spawn at
		int platRoll;

		//For Left and right spawning
		if (horiz != 0) {
			float spawnX = this.gameObject.transform.position.x;
			float spawnY = 0;
			if (Random.Range (0, 100) > 30) {
				spawnY= Random.Range (edgeBotLeft.y + .5f, edgeTopRight.y - .5f);
				for (int i=0; i<2; ++i){
					if (Random.Range (0, 100) > 50) {
						GameObject tmp = (GameObject) Instantiate (backgrounds [0], new Vector3 (spawnX , spawnY, 10), Quaternion.identity);
						tmp.transform.SetParent (Camera.main.transform);
						tmp.transform.localPosition = new Vector3 (-tmp.transform.localPosition.x, tmp.transform.localPosition.y, tmp.transform.localPosition.z);
						tmp.transform.localScale = tmp.transform.localScale * Random.Range (70, 130) * .01f;
						tmp.transform.parent = null;
						tmp.GetComponent<BackgroundObject> ().AddVelocity (100*gManager.GetSpeedVector());
						spawnY -= Random.Range (300, 700) * .01f;
					}
				}
					
				if (Random.Range(0,100) > 50){
					spawnY = Random.Range (edgeBotLeft.y + .5f, edgeTopRight.y - .5f);
					spawnY = Random.Range (edgeBotLeft.y + .5f, edgeTopRight.y - .5f);
					GameObject tmp = (GameObject) Instantiate (backgrounds [1], new Vector3 (spawnX, spawnY, 10), Quaternion.identity);
					tmp.transform.localScale = tmp.transform.localScale * Random.Range (70, 130) * .01f;
				}
			}

			spawnCiel = Mathf.Min (edgeTopRight.y - 1f, player.transform.position.y + 7.5f); 					//The spawn cap is either offset from the players max jump height or the camera cieling													//If at the top of the screen											//If at the bottom of the screen
			spawnY = Random.Range ((player.transform.position.y + 1f) * 100f, spawnCiel * 100f) * .01f;			// Spawn platforms starting at a location above the player to the cap
			for (int i = 0; i < 3; ++i) {																		//Spawn at most 3 platforms
				spawnX = this.gameObject.transform.position.x + Random.Range (0f, 120f) * .01f * horiz;  		//Spawn at the spawners x position + some variance based on direction
				spawnLocation = new Vector3 (spawnX, spawnY);													//Set the spawn location for the vector
				if (i == 0 && Random.Range (0, 100) > 85) {
					spawnY -= Random.Range (200f, 700f) * .01f;
					spawnLocation = new Vector3 (spawnX, spawnY);
					Instantiate (platforms [3], spawnLocation, Quaternion.identity);							//Only spawn a moving platform as the first and only platfrm
					break;
				}
				if (spawnY > edgeBotLeft.y + .5f) {																//If the camera is within a certain vertical tolerance, spawn it
					platRoll = Random.Range (0, 100);
					if (platRoll <= 60) {
						Instantiate (platforms [0], spawnLocation, Quaternion.identity);
					} else if (platRoll > 60 && platRoll < 95) {
						Instantiate (platforms [2], spawnLocation, Quaternion.identity);
					} else {
						Instantiate (platforms [1], spawnLocation, Quaternion.identity);
					}
				}
				spawnY -= Random.Range (200, 500) * .01f;															
			}
		}

		if (vert !=0) {
			float spawnY = this.gameObject.transform.position.y;
			float spawnX = 0;
			if (Random.Range (0, 100) > 30) {
				spawnX = Random.Range (edgeBotLeft.x + .5f, edgeTopRight.x - .5f);
				for (int i=0; i<2; ++i){
					if (Random.Range (0, 100) > 50) {
						GameObject tmp = (GameObject) Instantiate (backgrounds [0], new Vector3 (spawnX , spawnY, 10), Quaternion.identity);
						tmp.transform.SetParent (Camera.main.transform);
						tmp.transform.localPosition = new Vector3 (tmp.transform.localPosition.x, -tmp.transform.localPosition.y, tmp.transform.localPosition.z);
						tmp.transform.localScale = tmp.transform.localScale * Random.Range (70, 130) * .01f;
						tmp.transform.parent = null;
						tmp.GetComponent<BackgroundObject> ().AddVelocity (100*gManager.GetSpeedVector());
						spawnX -= Random.Range (300, 700) * .01f;
					}
				}
					
				if (Random.Range(0,100) > 50){
					spawnX = Random.Range (edgeBotLeft.x + .5f, edgeTopRight.x - .5f);
					GameObject tmp = (GameObject) Instantiate (backgrounds [1], new Vector3 (spawnX, spawnY, 10), Quaternion.identity);
					tmp.transform.localScale = tmp.transform.localScale * Random.Range (70, 130) * .01f;
				}
			}

	
			spawnX = Random.Range (edgeTopRight.x * 100f - 100f, edgeTopRight.x * 100f - 300f) * .01f;						//Random x variance
			for (int i = 0; i < 5 ; ++i){																		//Spawn 4 platforms
				spawnY = this.gameObject.transform.position.y + Random.Range (0f, 175f) * .01f * vert;			//Random y variance
				spawnLocation = new Vector3 (spawnX,spawnY);													//Spawn
				if(i == 0 && Random.Range(0,100) > 90){
					Instantiate (platforms [3], spawnLocation, Quaternion.identity);							//Only spawn a moving platform as the first and only platfrm
					i += 2;
					spawnX -= Random.Range (700, 900) * .01f;
					spawnLocation = new Vector3 (spawnX,spawnY);
				}

				if(spawnX > edgeBotLeft.x+ .5f){																//Horizontal tolerance
					platRoll = Random.Range(0,100);
					if (platRoll <= 80){
						Instantiate (platforms [0], spawnLocation, Quaternion.identity);
					} else if (platRoll > 80 && platRoll< 95){
						Instantiate (platforms [2], spawnLocation, Quaternion.identity);
					} else {
						Instantiate (platforms [1], spawnLocation, Quaternion.identity);
					}
				}
				spawnX -= Random.Range(225,650)*.01f;															//Spawn next platform
			} 
		}

		Invoke ("SpawnPlatform", Mathf.Max(1.5f-invokeCount, .50f)); //Spawn next set of platforms after a delay
		
	}

	public void SpawnPlatformOnce(){
		//These vectors will determin which vertical and horizontal section of the screen
		//the player is on based on the current camera position and player position
		Vector3 edgeTopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 1f, 0f));
		Vector3 edgeBotLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0f, 0f));
		Vector3 spawnLocation; // Location to spawn a platform
		float spawnCiel; //Max value for a platform to spawn at
		int platRoll;

		//For Left and right spawning
		if (horiz != 0) {
			float spawnX = this.gameObject.transform.position.x;
			float spawnY = 0;
			if (Random.Range (0, 100) > 30) {
				spawnY= Random.Range (edgeBotLeft.y + .5f, edgeTopRight.y - .5f);
				for (int i=0; i<2; ++i){
					if (Random.Range (0, 100) > 50) {
						GameObject tmp = (GameObject) Instantiate (backgrounds [0], new Vector3 (spawnX , spawnY, 10), Quaternion.identity);
						tmp.transform.SetParent (Camera.main.transform);
						tmp.transform.localPosition = new Vector3 (-tmp.transform.localPosition.x, tmp.transform.localPosition.y, tmp.transform.localPosition.z);
						tmp.transform.localScale = tmp.transform.localScale * Random.Range (70, 130) * .01f;
						tmp.transform.parent = null;
						tmp.GetComponent<BackgroundObject> ().AddVelocity (100*gManager.GetSpeedVector());
						spawnY -= Random.Range (300, 700) * .01f;
					}
				}

				if (Random.Range(0,100) > 50){
					spawnY = Random.Range (edgeBotLeft.y + .5f, edgeTopRight.y - .5f);
					spawnY = Random.Range (edgeBotLeft.y + .5f, edgeTopRight.y - .5f);
					GameObject tmp = (GameObject) Instantiate (backgrounds [1], new Vector3 (spawnX, spawnY, 10), Quaternion.identity);
					tmp.transform.localScale = tmp.transform.localScale * Random.Range (70, 130) * .01f;
				}
			}

			spawnCiel = Mathf.Min (edgeTopRight.y - 1f, player.transform.position.y + 7.5f); 					//The spawn cap is either offset from the players max jump height or the camera cieling													//If at the top of the screen											//If at the bottom of the screen
			spawnY = Random.Range ((player.transform.position.y + 1f) * 100f, spawnCiel * 100f) * .01f;			// Spawn platforms starting at a location above the player to the cap
			for (int i = 0; i < 3; ++i) {																		//Spawn at most 3 platforms
				spawnX = this.gameObject.transform.position.x + Random.Range (0f, 120f) * .01f * horiz;  		//Spawn at the spawners x position + some variance based on direction
				spawnLocation = new Vector3 (spawnX, spawnY);													//Set the spawn location for the vector
				if (i == 0 && Random.Range (0, 100) > 85) {
					spawnY -= Random.Range (200f, 700f) * .01f;
					spawnLocation = new Vector3 (spawnX, spawnY);
					Instantiate (platforms [3], spawnLocation, Quaternion.identity);							//Only spawn a moving platform as the first and only platfrm
					break;
				}
				if (spawnY > edgeBotLeft.y + .5f) {																//If the camera is within a certain vertical tolerance, spawn it
					platRoll = Random.Range (0, 100);
					if (platRoll <= 60) {
						Instantiate (platforms [0], spawnLocation, Quaternion.identity);
					} else if (platRoll > 60 && platRoll < 95) {
						Instantiate (platforms [2], spawnLocation, Quaternion.identity);
					} else {
						Instantiate (platforms [1], spawnLocation, Quaternion.identity);
					}
				}
				spawnY -= Random.Range (200, 500) * .01f;															
			}
		}

		if (vert !=0) {
			float spawnY = this.gameObject.transform.position.y;
			float spawnX = 0;
			if (Random.Range (0, 100) > 30) {
				spawnX = Random.Range (edgeBotLeft.x + .5f, edgeTopRight.x - .5f);
				for (int i=0; i<2; ++i){
					if (Random.Range (0, 100) > 50) {
						GameObject tmp = (GameObject) Instantiate (backgrounds [0], new Vector3 (spawnX , spawnY, 10), Quaternion.identity);
						tmp.transform.SetParent (Camera.main.transform);
						tmp.transform.localPosition = new Vector3 (tmp.transform.localPosition.x, -tmp.transform.localPosition.y, tmp.transform.localPosition.z);
						tmp.transform.localScale = tmp.transform.localScale * Random.Range (70, 130) * .01f;
						tmp.transform.parent = null;
						tmp.GetComponent<BackgroundObject> ().AddVelocity (100*gManager.GetSpeedVector());
						spawnX -= Random.Range (300, 700) * .01f;
					}
				}

				if (Random.Range(0,100) > 50){
					spawnX = Random.Range (edgeBotLeft.x + .5f, edgeTopRight.x - .5f);
					GameObject tmp = (GameObject) Instantiate (backgrounds [1], new Vector3 (spawnX, spawnY, 10), Quaternion.identity);
					tmp.transform.localScale = tmp.transform.localScale * Random.Range (70, 130) * .01f;
				}
			}


			spawnX = Random.Range (edgeTopRight.x * 100f - 100f, edgeTopRight.x * 100f - 300f) * .01f;						//Random x variance
			for (int i = 0; i < 5 ; ++i){																		//Spawn 4 platforms
				spawnY = this.gameObject.transform.position.y + Random.Range (0f, 175f) * .01f * vert;			//Random y variance
				spawnLocation = new Vector3 (spawnX,spawnY);													//Spawn
				if(i == 0 && Random.Range(0,100) > 90){
					Instantiate (platforms [3], spawnLocation, Quaternion.identity);							//Only spawn a moving platform as the first and only platfrm
					i += 2;
					spawnX -= Random.Range (700, 900) * .01f;
				}

				if(spawnX > edgeBotLeft.x+ .5f){																//Horizontal tolerance
					platRoll = Random.Range(0,100);
					if (platRoll <= 80){
						Instantiate (platforms [0], spawnLocation, Quaternion.identity);
					} else if (platRoll > 80 && platRoll< 95){
						Instantiate (platforms [2], spawnLocation, Quaternion.identity);
					} else {
						Instantiate (platforms [1], spawnLocation, Quaternion.identity);
					}
				}
				spawnX -= Random.Range(225,650)*.01f;															//Spawn next platform
			} 
		}
	}
}
