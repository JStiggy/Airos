using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

	//The platforms renderer and collider
	[HideInInspector] public SpriteRenderer Renderer;
	[HideInInspector] public BoxCollider2D platformCollider;
	[HideInInspector] public Vector3 pT;

	//Layer of all objects considered the player
	public LayerMask playerMask;

	void Start () {
		//Use get component to get all necesary values
		pT = this.GetComponent <Transform>().position;
		Renderer = this.GetComponent<SpriteRenderer> ();
		platformCollider = gameObject.GetComponent<BoxCollider2D> ();
		SetColor (Camera.main.GetComponent<GameManager> ().getScrollDirection());
	}

	void FixedUpdate () {
		//Check if the player is within a rectangular area above the platform
		//If the player is in the area, enable collision for the platform
		if (Physics2D.OverlapArea (new Vector2(gameObject.transform.position.x-.60f, gameObject.transform.position.y+1.5f), new Vector2(gameObject.transform.position.x+.60f, gameObject.transform.position.y+2f), playerMask) && !platformCollider.enabled) {
			platformCollider.enabled = true;
			CancelInvoke ("DisableCollsion");
		} else {
			Invoke ("DisableCollsion", .1f);
		}

		//When the platform moves a certain distance from the camera, delete it
		if (Vector3.Distance (gameObject.transform.position, Camera.main.transform.position) > 17) {
			Destroy (gameObject);
		}
	}

	void DisableCollsion(){
		platformCollider.enabled = false;
	}

	//Set the color of the platform
	public void SetColor(int c) {
		if (c == 3) {
			Renderer.color = new Color (255, 255, 255);
		} else if (c == 2) {
			Renderer.color = new Color (0, 255, 0);
		} else if (c == 1) {
			Renderer.color = new Color (0, 0, 255);
		} else {
			Renderer.color = new Color (14f/255f, 1f, 252f/255f);
		}
	}
}
