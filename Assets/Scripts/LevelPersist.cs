using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPersist : MonoBehaviour
{
  private int startSceneIndex;

  private void Awake()
  {
    if (FindObjectsOfType<LevelPersist>().Length > 1)
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
    startSceneIndex = SceneManager.GetActiveScene().buildIndex;
  }

  // Update is called once per frame
  void Update()
  {
    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    if (startSceneIndex != currentSceneIndex)
    {
      Destroy(gameObject);
    }
  }
}