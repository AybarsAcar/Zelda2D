using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
  [SerializeField] private AudioClip coinPickupSFX;
  [SerializeField] private int coinValue = 100;

  private bool isCoinActivated = false;
  
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (isCoinActivated)
    {
      return;
    }

    isCoinActivated = true;
    
    FindObjectOfType<GameSession>().AddToScore(coinValue);
    
    AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position); // play at the camera poision
    
    Destroy(gameObject);
  }
}