using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour {

	public Text HighScoreList;
	public RectTransform lSelector;
	public RectTransform rSelector;

	private int[] Scores = new int[10];
	private string[] Names = new string[10];

	public AudioSource sfxSource;

	void Start () {

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

		for(int i=0; i<10; ++i){
			HighScoreList.text = HighScoreList.text + Names [i].PadRight (21) + Scores [i].ToString ().PadLeft (10) + "\n";
		}
	}

	public void HoverMain(){
		lSelector.gameObject.SetActive (true);
		rSelector.gameObject.SetActive (true);
	}

	public void HideSelectors(){
		lSelector.gameObject.SetActive (false);
		rSelector.gameObject.SetActive (false);
	}

	public void MainMenu(){
		sfxSource.Play ();
		SceneManager.LoadScene(0);
	}
}
