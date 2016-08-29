using UnityEngine;
using System.Collections;

public class BackgroundObject : MonoBehaviour {

	private GameManager gManager;
	private SpriteRenderer Renderer;
	private  int initialDirection;

	// Use this for initialization
	void Start () {
		Renderer = this.GetComponent<SpriteRenderer> ();
		gManager = Camera.main.GetComponent<GameManager> ();
		if(this.gameObject.tag.Equals("Arrow")){
			SetColor (gManager.getScrollDirection());
			SetRotation (gManager.getScrollDirection ());
		}
		initialDirection = gManager.getScrollDirection ();
	}
	
	// Update is called once per frame
	void Update () {
		if(initialDirection != gManager.getScrollDirection() || Vector3.Distance (gameObject.transform.position, Camera.main.transform.position) > 30f){
			Destroy (this.gameObject);
		}
	}

	//Set the color of the platform
	void SetColor(int c) {
		if (c == 3) {
			Renderer.color = new Color (151f/255f, 151f/255f, 151f/255f);
		} else if (c == 2) {
			Renderer.color = new Color (0f, 0f, 151f/255f);
		} else if (c == 1) {
			Renderer.color = new Color (0f, 151/255f, 0f);
		} else {
			Renderer.color = new Color ( 151f/255f, 0f, 0f);
		}
	}

	void SetRotation(int c){
		if (c % 2 == 0){
			this.transform.Rotate( 0f,0f,c * 90f);
		} else {
			this.transform.Rotate(0f,0f,c * -90f);
		}


	}

	public void AddVelocity(Vector3 v3){
		this.GetComponent<Rigidbody2D> ().velocity = v3 * Random.Range(100,125) * .01f;
	}
}
