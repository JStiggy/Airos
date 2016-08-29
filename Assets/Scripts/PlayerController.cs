using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private SpriteRenderer sRenderer;

	public float jumpDistance = 5f; //Potential force applied for each frame the player jumps
	public float jumpTime = .5f; //Total time the player can gain extra jump force
	private float jumpTimeCounter = .5f; //Secondary value used to keep track of jump time
	private bool jumpEnd = true; //Determines whether the player can continnue or start a jump
	public float downForce = 2f;
	private int c = 0;


	public float maxSpeed = 7f; //Max horizontal movement speed 10 originally
	bool facingRight = true; //Movement direction of the player
	private BoxCollider2D playerCol; //Players collision
	private Rigidbody2D rBody; //Players rigidbody
	private Animator anim; //Player animations

	private bool grounded = false; //Determines of the player is touching the floor
	public LayerMask groundMask; //Mask determining whether an object is the ground

	public AudioClip[] sfxList;
	public AudioSource sManager;

	void Start() {
		// Get all necessary components, set the player color
		sRenderer = gameObject.GetComponent<SpriteRenderer> ();
		rBody = gameObject.GetComponent <Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> ();
		playerCol = gameObject.GetComponent<BoxCollider2D> ();
		sRenderer.color = SetColor ();
		InvokeRepeating("SpawnTrail", 0, 0.04f);
	}

	void FixedUpdate() {
		//Check to see if the player character is grounded, create a 
		//small rectangle to check if any platforms exist under the player
		grounded = Physics2D.OverlapArea (new Vector2 (-.75f + transform.position.x,-.7f + transform.position.y), new Vector2 (.75f + transform.position.x, -1f + transform.position.y), groundMask);
		//Set animation variables for the player based on whether the player is in the air, 
		//and the current vertical speed of the players rigid body
		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("VerticalSpeed", rBody.velocity.y);
		//Set the time the player can hold the button to increase their jump time
		if(grounded)
		{
			jumpTimeCounter = jumpTime;
		}
		//Find the amount of movement inpout by the player, and save the value for the players animations
		float move = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (move));
		//Give the playerls rigidbody a velocity for movement
		rBody.velocity = new Vector2 (move * maxSpeed, rBody.velocity.y);
		//Flip the player horizontally based on the players current horizontal direction. if applicable
		if (move > 0 && !facingRight)
			Flip ();
		else if(move < 0 && facingRight)
			Flip();
	}

	void Update() {

		if (c == 3){
			jumpDistance = 5f + ((float) Camera.main.GetComponent<GameManager> ().GetSpeedMultiplier()/2f);
		}
		//Enable the player to drop through platforms if the Drop axis is held
		if (Input.GetAxis("Drop") > 0) {
			rBody.velocity = new Vector2 (rBody.velocity.x, rBody.velocity.y-downForce/30);
			//playerCol.enabled = false;
		} else if (playerCol.enabled == false) {
			//playerCol.enabled = true;
		}

		//Apply an initial force to the player, allowing them to jump
		if(Input.GetButtonDown("Vertical")){
			if(grounded){
				sManager.clip = sfxList [0];
				sManager.Play ();
				rBody.velocity = new Vector2 (rBody.velocity.x, 3.5f);
				jumpEnd = false;
			}
		}
		//Apply extra jump force to the player, while the jump button is held, after JumpTime time occurs,
		//the player can no longer increase their jump height
		if(Input.GetButton("Vertical") && !jumpEnd){
			if(jumpTimeCounter > 0){
				rBody.velocity = new Vector2 (rBody.velocity.x, jumpDistance);
				jumpTimeCounter -= Time.deltaTime;
			}
		}


		//Once the player releases the jump button, they can not increase the current jump,
		// or start a new jump until the player lands
		if(Input.GetButtonUp("Vertical")){
			jumpTimeCounter = 0;
			jumpEnd = true;
		}
	}

	void Flip(){
		facingRight = !facingRight;
		transform.localScale = new Vector3(-1 * transform.localScale.x,1,1);
		rBody.velocity = new Vector2 (0, rBody.velocity.y);
	}

	void SpawnTrail()
	{
		if ( Mathf.Abs(anim.GetFloat("VerticalSpeed")) > 1f || Mathf.Abs(anim.GetFloat("Speed")) > .1f){
			GameObject trailPart = new GameObject();
			trailPart.transform.localScale = this.transform.localScale;
			SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
			trailPartRenderer.color = SetColor ();
			trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
			trailPart.transform.position = transform.position;
			Destroy(trailPart, 0.2f); // replace 0.5f with needed lifeTime

			StartCoroutine("FadeTrailPart", trailPartRenderer);

		}
	}

	IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
	{
		Color color = trailPartRenderer.color;
		color.a -= 0.75f; // replace 0.5f with needed alpha decrement
		trailPartRenderer.color = color;

		yield return new WaitForEndOfFrame();
	}

	public Color SetColor() {
		//Check the current scrolling direction, change the coor of the player based on it
		c = Camera.main.GetComponent<GameManager> ().getScrollDirection();
		if (c == 3) {
			return new Color (255, 255, 255);
		} else if (c == 2) {
			return new Color (0, 255, 0);
		} else if (c == 1) {
			return new Color (0, 0, 255);
		} else {
			return new Color (14f/255f, 1f, 252f/255f);
		}
	}
}
