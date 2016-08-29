using UnityEngine;
using System.Collections;

public class BonusPlatform : Platform {

	bool activated = false;

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.layer == 8 && activated == false){
			activated = true;
			Camera.main.GetComponent<GameManager> ().increaseScore (200);
		}
	}

}
