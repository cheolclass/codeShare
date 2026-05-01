using System.Collections;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private	GameObject		panelGameStart;
	[SerializeField]
	private	TextMeshProUGUI	textCurrentScore;
	[SerializeField]
	private	GameObject		panelGameOver;
	[SerializeField]
	private	TextMeshProUGUI	textBestScore;

	private	int				score = 0;

	public	bool			IsGameStart { get; private set; } = false;
	public	bool			IsGameOver	{ get; private set;	} = false;
	public	int				Score
	{
		get => score;
		set
		{
			if ( IsGameOver == true ) return;

			score = value;
			textCurrentScore.text = score.ToString();
		}
	}

	private IEnumerator Start()
	{
		while ( true )
		{
			if ( Input.GetMouseButtonDown(0) )
			{
				GameStart();

				yield break;
			}

			yield return null;
		}
	}

	public void GameStart()
	{
		IsGameStart = true;
		panelGameStart.SetActive(false);
		textCurrentScore.gameObject.SetActive(true);
	}

	public void GameOver()
	{
		if ( IsGameOver == true ) return;

		IsGameOver = true;
		panelGameOver.SetActive(true);

		int bestScore = PlayerPrefs.GetInt(Constants.BestScore);
		if ( score > bestScore )
		{
			PlayerPrefs.SetInt(Constants.BestScore, score);
			textBestScore.text = $"<size=75>NEW</size>\n{score}";
		}
		else
		{
			textBestScore.text = $"<size=75>Best</size>\n{bestScore}";
		}

		StartCoroutine(nameof(OnGameOver));
	}

	private IEnumerator OnGameOver()
	{
		while ( true )
		{
			if ( Input.GetMouseButtonDown(0) )
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(0);

				yield break;
			}

			yield return null;
		}
	}

	[ContextMenu("Reset Data")]
	private void ResetData()
	{
		PlayerPrefs.DeleteAll();
	}
}

