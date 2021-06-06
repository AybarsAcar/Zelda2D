using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Enemy moves right and left
 * and turns as it reaches to the edge
 */
public class EnemyMovement : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 1f;

  private Rigidbody2D _rigidBody;


  void Start()
  {
    _rigidBody = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    if (IsFacingRight())
    {
      Move(moveSpeed);
    }
    else
    {
      Move(-moveSpeed);
    }
  }

  private bool IsFacingRight()
  {
    return transform.localScale.x > 0f;
  }

  private void Move(float moveSpeed)
  {
    _rigidBody.velocity = new Vector2(moveSpeed, 0f);
  }

  /**
   * changes the direction of the localScale on an exit event
   */
  private void OnTriggerExit2D(Collider2D other)
  {
    // y velocity is consistent, x is flipped
    transform.localScale = new Vector2(-(Mathf.Sign(_rigidBody.velocity.x)), 1f);
  }
}