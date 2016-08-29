using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public AudioSource sfxSource;

	public GameObject MainMenuPanel;
	public GameObject QuitGamePanel;
	public RectTransform lSelector;
	public RectTransform rSelector;


	void Start(){
		//When starting the game, check to see if any save data exists.
		if(PlayerPrefs.GetInt("SaveExists") == 0){
			//Save Data does not exist initialize all data
			PlayerPrefs.SetString("HighScore1Name","None");
			PlayerPrefs.SetInt("HighScore1Score",0);

			PlayerPrefs.SetString("HighScore2Name","None");
			PlayerPrefs.SetInt("HighScore2Score",0);

			PlayerPrefs.SetString("HighScore3Name","None");
			PlayerPrefs.SetInt("HighScore3Score",0);

			PlayerPrefs.SetString("HighScore4Name","None");
			PlayerPrefs.SetInt("HighScore4Score",0);

			PlayerPrefs.SetString("HighScore5Name","None");
			PlayerPrefs.SetInt("HighScore5Score",0);

			PlayerPrefs.SetString("HighScore6Name","None");
			PlayerPrefs.SetInt("HighScore6Score",0);

			PlayerPrefs.SetString("HighScore7Name","None");
			PlayerPrefs.SetInt("HighScore7Score",0);

			PlayerPrefs.SetString("HighScore8Name","None");
			PlayerPrefs.SetInt("HighScore8Score",0);

			PlayerPrefs.SetString("HighScore9Name","None");
			PlayerPrefs.SetInt("HighScore9Score",0);

			PlayerPrefs.SetString("HighScore10Name","None");
			PlayerPrefs.SetInt("HighScore10Score",0);

			PlayerPrefs.SetString("CurrentName","None");
			PlayerPrefs.SetInt("CurrentScore",0);

			PlayerPrefs.SetInt("SkipTutorial",0);

			PlayerPrefs.SetInt("SaveExists",1);

			PlayerPrefs.Save();
		}
	}

	public void StartGame(){
		sfxSource.Play ();
		if(PlayerPrefs.GetInt("SkipTutorial") == 0){
			SceneManager.LoadScene(1);
		} else {
			SceneManager.LoadScene(2);
		}
	}

	public void HoverStart(){
		lSelector.anchorMax = new Vector2 (.38f, .52f);
		lSelector.anchorMin = new Vector2 (.38f, .52f);
		rSelector.anchorMin = new Vector2 (.62f, .52f);
		rSelector.anchorMax = new Vector2 (.62f, .52f);
		activateSelectors ();
	}

	public void HoverHighScores(){
		lSelector.anchorMax = new Vector2 (.29f, .37f);
		lSelector.anchorMin = new Vector2 (.29f, .37f);
		rSelector.anchorMin = new Vector2 (.71f, .37f);
		rSelector.anchorMax = new Vector2 (.71f, .37f);
		activateSelectors ();
	}

	public void HoverQuitGame(){
		lSelector.anchorMax = new Vector2 (.38f, .22f);
		lSelector.anchorMin = new Vector2 (.38f, .22f);
		rSelector.anchorMin = new Vector2 (.62f, .22f);
		rSelector.anchorMax = new Vector2 (.62f, .22f);
		activateSelectors ();
	}

	public void activateSelectors(){
		lSelector.anchoredPosition = Vector2.zero;
		rSelector.anchoredPosition = Vector2.zero;
		lSelector.gameObject.SetActive (true);
		rSelector.gameObject.SetActive (true);
	}

	public void HoverEnd(){
		lSelector.gameObject.SetActive (false);
		rSelector.gameObject.SetActive (false);
	}

	public void HighScoresMenu(){
		sfxSource.Play ();
		SceneManager.LoadScene(4);
	}

	public void QuitGameMenu(){
		sfxSource.Play ();
		QuitGamePanel.SetActive(true);
		MainMenuPanel.SetActive (false);
		HoverEnd ();
	}

	public void QuitGame(){
		sfxSource.Play ();
		Application.Quit ();
	}

	public void ReturnToGame(){
		sfxSource.Play ();
		QuitGamePanel.SetActive(false);
		MainMenuPanel.SetActive (true);
	}
}
