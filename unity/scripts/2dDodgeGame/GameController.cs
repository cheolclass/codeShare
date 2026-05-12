using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;  /// for Mouse input
using TMPro;  ///  

public class GameController : MonoBehaviour
{
	[SerializeField]
	private GameObject panelGameStart;
	
	[SerializeField]  ///  
	private TextMeshProUGUI textCurrentScore;  ///
	
	[SerializeField]
	private GameObject panelGameOver;  ///

	[SerializeField]  ///
	private TextMeshProUGUI textBestScore;  ///


	public bool IsGameStart { get; private set; } = false;
	public bool IsGameOver { get; private set; } = false;

	private int score = 0;  ///
	public int Score  ///
	{
		get => score;
		set
		{
			if (IsGameOver == true) return;

			score = value;
			textCurrentScore.text = score.ToString();
		}
	}

	private IEnumerator Start()
	{
		while (true)
		{
			//if (Input.GetMouseButtonDown(0))  /// legacy Input System 
			if (Mouse.current.leftButton.wasPressedThisFrame)  /// 새로운 Input System 사용
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
		panelGameStart.SetActive(false);  /// 게임 시작 시 게임 시작 패널 비활성화  
		textCurrentScore.gameObject.SetActive(true);  ///
	}

	public void GameOver()
	{
		if (IsGameOver == true) return;

		IsGameOver = true;
		panelGameOver.SetActive(true);

		int bestScore = PlayerPrefs.GetInt(Constants.BestScore);
		if (score > bestScore)
		{
			PlayerPrefs.SetInt(Constants.BestScore, score);
			textBestScore.text = $"<size=75>NEW</size>\n{score}";
		}
		else
		{
			textBestScore.text = $"<size=75>Best</size>\n{bestScore}";
		}

		///StartCoroutine(nameof(OnGameOver));
        StartCoroutine(OnGameOver());
	}

	private IEnumerator OnGameOver()
	{
		while (true)
		{
            //if (Input.GetMouseButtonDown(0))  /// legacy Input System 
			if (Mouse.current.leftButton.wasPressedThisFrame)  /// 새로운 Input System 사용
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(0);

				yield break;
			}

			yield return null;
		}
	}

	[ContextMenu("Reset Data")]
	private void ResetData()  /// 에디터의 컨텍스트 메뉴에 서브메뉴로 출력 
	{
		PlayerPrefs.DeleteAll();  /// 모든 데이터 리셋
	}
}