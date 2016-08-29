using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	public AudioSource sfxSource;
	public RectTransform lSelector;
	public RectTransform rSelector;

	public void toggleTutorial(){
		sfxSource.Play ();
		PlayerPrefs.SetInt ("SkipTutorial", 1 - PlayerPrefs.GetInt ("SkipTutorial"));
	}

	public void startGame(){
		sfxSource.Play ();
		PlayerPrefs.Save();
		SceneManager.LoadScene(2);
	}

	public void hideSelectors(){
		lSelector.gameObject.SetActive (false);
		rSelector.gameObject.SetActive (false);
	}

	public void showSelectors(){
		lSelector.gameObject.SetActive (true);
		rSelector.gameObject.SetActive (true);
	}

}
