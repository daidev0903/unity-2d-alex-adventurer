using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] AudioClip deathAudio;
    [SerializeField] AudioClip jumpAudio;

    [Header("State")]
    [SerializeField] private bool isAlive = true;

    [Header("Cached References")]
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;
    private BoxCollider2D playerBoxCollider2D;
    private CapsuleCollider2D playerCapsuleCollider2D;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBoxCollider2D = GetComponent<BoxCollider2D>();
        playerCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (!isAlive)
            return;

        Run();
        Jump();
        FlipSprite();
        Climb();
        Die();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = playerVelocity;

        bool playerMoveLeftRight = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("Run", playerMoveLeftRight);
    }

    private void Jump()
    {
        if (!playerBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        else if(playerAnimator.GetBool("Jump"))
        {
            Debug.Log("Player rounded");
            playerAnimator.SetBool("Jump", false);
        }
            
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump Called");
            playerRigidBody.velocity = Vector2.zero;
            playerRigidBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            playerAnimator.SetBool("Jump", true);
            AudioSource.PlayClipAtPoint(jumpAudio, Camera.main.transform.position);
        }
    }

    private void FlipSprite()
    {
        bool playerMoveLeftRight = Mathf.Abs(playerRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerMoveLeftRight)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidBody.velocity.x), 1f);
        }
    }

    private void Climb()
    {
        if (!playerBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerAnimator.SetBool("Climb", false);
            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(playerRigidBody.velocity.x, controlThrow * climbSpeed);
        playerRigidBody.velocity = climbVelocity;

        bool playerMoveUpDown = Mathf.Abs(playerRigidBody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("Climb", playerMoveUpDown);
    }

    private void Die()
    {
        if(IsTouchingMonster() || IsTouchingSpike() || IsFalling())
        {
            isAlive = false;
            playerAnimator.SetTrigger("Die");
            playerRigidBody.velocity = deathKick;
            _ = DelayReset();
            AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position);
        }
    }

    private bool IsFalling()
    {
        if (playerBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Water")))
            return true;
        else
            return false;
    }

    private bool IsTouchingSpike()
    {
        if (playerBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Spikes")))
            return true;
        else
            return false;
    }

    private bool IsTouchingMonster()
    {
        if(playerCapsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            return true;
        else
            return false;
    }

    private async Task DelayReset()
    {
        await Task.Delay(500);
        FindObjectOfType<GameSession>()?.ProcessPlayerDeath();
    }
}