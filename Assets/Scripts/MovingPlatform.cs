using UnityEngine;
using System.Collections;

public class MovingPlatform : Platform {

	Transform curTran;
	float distance = 1f;
	float movementValue = .02f;
	int moveDirection;

	void Start () {
		//Use get component to get all necesary values
		pT = this.GetComponent <Transform>().position;
		Renderer = this.GetComponent<SpriteRenderer> ();
		platformCollider = gameObject.GetComponent<BoxCollider2D> ();
		SetColor (Camera.main.GetComponent<GameManager> ().getScrollDirection());

		curTran = this.transform;
		moveDirection = Camera.main.GetComponent<GameManager> ().getScrollDirection ();
		InvokeRepeating("MovePlatform", .05f, .05f);
	}

	void MovePlatform(){
		if ((pT.x + distance < curTran.position.x && movementValue > 0) || (pT.x - distance > curTran.position.x && movementValue < 0) || 
			(pT.y + distance < curTran.position.y && movementValue > 0) || (pT.y - distance > curTran.position.y && movementValue < 0)) {
			movementValue = -movementValue;
		}
		this.transform.Translate (movementValue * (moveDirection%2), movementValue *  (1 - moveDirection%2), 0);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.layer == 8){
			other.transform.position = new Vector3(other.transform.position.x + (movementValue/1.22f) * (moveDirection%2), other.transform.position.y, other.transform.position.z);
		}
	}
}
