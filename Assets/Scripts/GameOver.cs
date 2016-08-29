using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public GameObject HighScorePrompt;
	public GameObject PrimaryMenuButtons;
	public Text UserInput;
	public Text UserScore;

	private int replaceScore = 10;
	private int[] Scores = new int[10];
	private string[] Names = new string[10];
	private int currentScore;

	public RectTransform lSelector;
	public RectTransform rSelector;

	private AudioSource mManager;
	public AudioSource sfxSource;

	void Start () {

		mManager = GameObject.Find ("MusicSource").GetComponent<AudioSource> ();
		mManager.clip = mManager.GetComponentInParent<MusicManager> ().musicList [0];
		mManager.volume = .15f;
		mManager.Play ();

		//Set current score and load all high scores and names into theire respective arrays
		currentScore = PlayerPrefs.GetInt("CurrentScore");
		UserScore.text = currentScore.ToString();

		Scores[0] = PlayerPrefs.GetInt("HighScore1Score");
		Scores[1] = PlayerPrefs.GetInt("HighScore2Score");
		Scores[2] = PlayerPrefs.GetInt("HighScore3Score");
		Scores[3] = PlayerPrefs.GetInt("HighScore4Score");
		Scores[4] = PlayerPrefs.GetInt("HighScore5Score");
		Scores[5] = PlayerPrefs.GetInt("HighScore6Score");
		Scores[6] = PlayerPrefs.GetInt("HighScore7Score");
		Scores[7] = PlayerPrefs.GetInt("HighScore8Score");
		Scores[8] = PlayerPrefs.GetInt("HighScore9Score");
		Scores[9] = PlayerPrefs.GetInt("HighScore10Score");

		Names[0] = PlayerPrefs.GetString("HighScore1Name");
		Names[1] = PlayerPrefs.GetString("HighScore2Name");
		Names[2] = PlayerPrefs.GetString("HighScore3Name");
		Names[3] = PlayerPrefs.GetString("HighScore4Name");
		Names[4] = PlayerPrefs.GetString("HighScore5Name");
		Names[5] = PlayerPrefs.GetString("HighScore6Name");
		Names[6] = PlayerPrefs.GetString("HighScore7Name");
		Names[7] = PlayerPrefs.GetString("HighScore8Name");
		Names[8] = PlayerPrefs.GetString("HighScore9Name");
		Names[9] = PlayerPrefs.GetString("HighScore10Name");

		//Compare scores to check for high scores
		for(int i=0; i<10; ++i){
			if (currentScore > Scores[i]){
				replaceScore = i;
				break;
			}
		}
		//If a high score exists open the highscore sub menu
		if (replaceScore != 10) {
			HighScorePrompt.SetActive(true);
		} else {
			PrimaryMenuButtons.SetActive (true);
		}
	}

	public void StartGame(){
		sfxSource.Play ();
		//Goto or skip the tutorial
		if(PlayerPrefs.GetInt("SkipTutorial") == 0){
			SceneManager.LoadScene(1);
		} else {
			SceneManager.LoadScene(2);
		}
	}

	public void HighScoresMenu(){
		sfxSource.Play ();
		SceneManager.LoadScene(4);
	}

	public void MainMenu(){
		sfxSource.Play ();
		SceneManager.LoadScene(0);
	}

	public void SubmitHighScore(){
		sfxSource.Play ();
		//Enter the player name and save all highscores
		if(UserInput.text.Length != 0){
			for(int i=9; i> replaceScore; --i){
				Scores[i] = Scores[i-1];
				Names[i] = Names[i-1];
			}
			Names[replaceScore] = UserInput.text;
			Scores [replaceScore] = currentScore;

			PlayerPrefs.SetInt("HighScore1Score", Scores[0]);
			PlayerPrefs.SetInt("HighScore2Score", Scores[1]);
			PlayerPrefs.SetInt("HighScore3Score", Scores[2]);
			PlayerPrefs.SetInt("HighScore4Score", Scores[3]);
			PlayerPrefs.SetInt("HighScore5Score", Scores[4]);
			PlayerPrefs.SetInt("HighScore6Score", Scores[5]);
			PlayerPrefs.SetInt("HighScore7Score", Scores[6]);
			PlayerPrefs.SetInt("HighScore8Score", Scores[7]);
			PlayerPrefs.SetInt("HighScore9Score", Scores[8]);
			PlayerPrefs.SetInt("HighScore10Score", Scores[9]);

			PlayerPrefs.SetString("HighScore1Name", Names[0]);
			PlayerPrefs.SetString("HighScore2Name", Names[1]);
			PlayerPrefs.SetString("HighScore3Name", Names[2]);
			PlayerPrefs.SetString("HighScore4Name", Names[3]);
			PlayerPrefs.SetString("HighScore5Name", Names[4]);
			PlayerPrefs.SetString("HighScore6Name", Names[5]);
			PlayerPrefs.SetString("HighScore7Name", Names[6]);
			PlayerPrefs.SetString("HighScore8Name", Names[7]);
			PlayerPrefs.SetString("HighScore9Name", Names[8]);
			PlayerPrefs.SetString("HighScore10Name", Names[9]);

			PlayerPrefs.Save();

			HighScorePrompt.SetActive(false);
			PrimaryMenuButtons.SetActive (true);
		}
	}

	public void HoverRetry(){
		lSelector.anchorMax = new Vector2 (.38f, .41f);
		lSelector.anchorMin = new Vector2 (.38f, .41f);
		rSelector.anchorMin = new Vector2 (.62f, .41f);
		rSelector.anchorMax = new Vector2 (.62f, .41f);
		activateSelectors ();
	}

	public void HoverHighScores(){
		lSelector.anchorMax = new Vector2 (.24f, .31f);
		lSelector.anchorMin = new Vector2 (.24f, .31f);
		rSelector.anchorMin = new Vector2 (.76f, .31f);
		rSelector.anchorMax = new Vector2 (.76f, .31f);
		activateSelectors ();
	}

	public void HoverMainMenu(){
		lSelector.anchorMax = new Vector2 (.38f, .21f);
		lSelector.anchorMin = new Vector2 (.38f, .21f);
		rSelector.anchorMin = new Vector2 (.62f, .21f);
		rSelector.anchorMax = new Vector2 (.62f, .21f);
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

}
