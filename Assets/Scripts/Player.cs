using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private float runSpeed = 5f;
  [SerializeField] private float jumpSpeed = 5f;
  [SerializeField] private float climbSpeed = 2f;
  [SerializeField] private Vector2 deathKick = new Vector2(25f, 25f);

  private bool isAlive = true;

  private Rigidbody2D _playerBody;
  private Animator _animator;
  private CapsuleCollider2D _colliderBody;
  private BoxCollider2D _colliderFeet;
  private GameSession _gameSession;

  private float gravityScaleAtStart;

  void Start()
  {
    _playerBody = GetComponent<Rigidbody2D>();
    _animator = GetComponent<Animator>();
    _colliderBody = GetComponent<CapsuleCollider2D>();
    _colliderFeet = GetComponent<BoxCollider2D>();
    _gameSession = FindObjectOfType<GameSession>();

    gravityScaleAtStart = _playerBody.gravityScale;
  }

  void Update()
  {
    if (!isAlive)
    {
      return;
    }

    Run();
    FlipSprite();
    Jump();
    Climb();
    Die();
  }


  /*
   * method to run
   * we preserve our Y velocity
   */
  private void Run()
  {
    var controlThrow = Input.GetAxis("Horizontal");

    _playerBody.velocity = new Vector2(controlThrow * runSpeed, _playerBody.velocity.y);

    var playerHorizontalSpeed = Mathf.Abs(_playerBody.velocity.x) > Mathf.Epsilon; // false if 0
    // set the run animation
    _animator.SetBool("IsRunning", playerHorizontalSpeed);
  }

  private void Jump()
  {
    if (IsGrounded() && Input.GetButtonDown("Jump"))
    {
      _playerBody.velocity += new Vector2(0f, jumpSpeed);
    }
  }

  private void Climb()
  {
    if (!IsClimbing())
    {
      _animator.SetBool("IsClimbing", false);
      _playerBody.gravityScale = gravityScaleAtStart;
      return;
    }

    _playerBody.gravityScale = 0;

    var controlThrow = Input.GetAxis("Vertical");

    _playerBody.velocity = new Vector2(_playerBody.velocity.x, controlThrow * climbSpeed);

    var playerHasVerticalSpeed = Mathf.Abs(_playerBody.velocity.y) > Mathf.Epsilon;

    _animator.SetBool("IsClimbing", playerHasVerticalSpeed);
  }

  private void FlipSprite()
  {
    var playerHorizontalSpeed = Mathf.Abs(_playerBody.velocity.x) > Mathf.Epsilon;

    if (playerHorizontalSpeed)
    {
      transform.localScale = new Vector2(Mathf.Sign(_playerBody.velocity.x), 1f);
    }
  }

  private void Die()
  {
    if (IsTouchingEnemy() || IsTouchingHazard())
    {
      isAlive = false;
      _animator.SetTrigger("Die");
      GetComponent<Rigidbody2D>().velocity = deathKick;
      
      _gameSession.OnPlayerDeath();
    }
  }

  private bool IsTouchingHazard()
  {
    return _colliderBody.IsTouchingLayers(LayerMask.GetMask("Hazard"));
  }

  private bool IsTouchingEnemy()
  {
    return _colliderBody.IsTouchingLayers(LayerMask.GetMask("Enemy"));
  }

  private bool IsGrounded()
  {
    var ground = LayerMask.GetMask("Ground");

    return _colliderFeet.IsTouchingLayers(ground);
  }

  private bool IsClimbing()
  {
    return _colliderFeet.IsTouchingLayers(LayerMask.GetMask("Climbing"));
  }
}