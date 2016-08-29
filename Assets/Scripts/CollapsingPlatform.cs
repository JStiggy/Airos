using UnityEngine;
using System.Collections;

public class CollapsingPlatform : Platform {

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.layer == 8){
			this.GetComponent<Animator> ().SetTrigger ("Flash");
			this.GetComponent<AudioSource> ().Play ();
			Invoke ("Collapse", .75f);
		}
	}

	void Collapse(){
		Destroy (this.gameObject);
		CancelInvoke ();
	}

	void PlatformShake(){
		this.transform.position = new Vector3(pT.x + Random.Range(-15f,15f)/100f,
			this.transform.position.y, this.transform.position.z);
	}

}
