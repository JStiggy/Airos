using UnityEngine;
using System.Collections;

public class OffscreenIndicator : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.y > this.transform.position.y) {
			this.transform.position = new Vector3 (player.transform.position.x, Camera.main.transform.position.y+ 4.5f);
			this.gameObject.GetComponent <SpriteRenderer>().enabled = true;
		} else {
			this.gameObject.GetComponent <SpriteRenderer>().enabled = false;
		}
	}
}
