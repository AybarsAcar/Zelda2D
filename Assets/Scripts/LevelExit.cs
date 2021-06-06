using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
  [SerializeField] private float levelLoadDelay = 0.6f;
  [SerializeField] private float levelExitSlowMotionFactor = 0.2f;

  private void OnTriggerEnter2D(Collider2D other)
  {
    StartCoroutine(LoadNextScene());
  }

  private IEnumerator LoadNextScene()
  {
    Time.timeScale = levelExitSlowMotionFactor;

    yield return new WaitForSeconds(levelLoadDelay);

    Time.timeScale = 1f;

    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex + 1);
  }
}