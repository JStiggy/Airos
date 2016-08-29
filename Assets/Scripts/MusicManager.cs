using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip [] musicList;

	private static MusicManager instance = null;
	public static MusicManager Instance {
		get { return instance; }
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	void Update(){
		this.transform.position = Camera.main.transform.position; // Ensure the music player is at the same location of the camera
	}
}