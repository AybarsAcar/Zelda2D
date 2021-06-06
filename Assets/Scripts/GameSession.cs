using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Singleton GameSession
 */
public class GameSession : MonoBehaviour
{
  [SerializeField] private int playerLives = 3;
  [SerializeField] private Text playerLivesText;
  [SerializeField] private Text playerScoreText;

  private int playerScore = 0;

  private void Awake()
  {
    var numGameSessions = FindObjectsOfType<GameSession>().Length;

    if (numGameSessions > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }

  // Start is called before the first frame update
  void Start()
  {
    playerLivesText.text = playerLives.ToString();
    playerScoreText.text = playerScore.ToString();
  }

  public void AddToScore(int scoreValue)
  {
    playerScore += scoreValue;
    playerScoreText.text = playerScore.ToString();
  }

  public void OnPlayerDeath()
  {
    if (playerLives <= 0)
    {
      ResetGameSession();
    }
    else
    {
      TakeLife();
    }
  }

  private void ResetGameSession()
  {
    SceneManager.LoadScene("Main Menu");
    Destroy(gameObject); // destroy the game session
  }

  private void TakeLife()
  {
    playerLives--;
    playerLivesText.text = playerLives.ToString();

    StartCoroutine(WaitAndLoadScene());
  }

  private IEnumerator WaitAndLoadScene()
  {
    yield return new WaitForSeconds(1);

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reload the current scene
  }
}