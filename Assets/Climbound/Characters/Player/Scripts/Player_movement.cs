using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_movement : MonoBehaviour
{
    public float playerSpeed;
    public float jumpForce;
    public float reboundForce;
    public float attackRadius;

    private float horizontalLimit = 9;

    public int killsCounter;

    private Animator playerAnimator;

    private bool isRunning = false;
    private bool isJumping = false;
    private bool isAttacking = false;

    private bool onAir;
    private enum playerDirections { Right, Left };
    playerDirections playerDirection;

    public SpriteRenderer playerSprite;
    public GameObject attackPoint;
    public LayerMask enemiesLayer;
    private ContactFilter2D contactFilter = new ContactFilter2D();

    private List<Collider2D> enemiesColliders = new List<Collider2D>();
    private Vector3 originalScale;

    private Rigidbody2D playerRb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
         
        playerDirection = playerDirections.Right;

        playerRb = GetComponent<Rigidbody2D>();

        enemiesColliders = new List<Collider2D>();

        contactFilter.layerMask = enemiesLayer;
        contactFilter.useLayerMask = true;

        originalScale = transform.localScale;

        onAir = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Movement
        Vector3 playerMovement = new Vector3(0, 0, 0);

        if (Keyboard.current.aKey.isPressed)
        {
            playerMovement.x = -1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            playerMovement.x = 1;
        }

        // Jump condition
        if (Keyboard.current.spaceKey.wasPressedThisFrame && onAir == false)
        {
            playerRb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }



        //Changing animation state
        //Running
        if (Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        //Jumping
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        //Attacking
        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        //Changing animation direction
        switch (playerDirection)
        {
            case playerDirections.Right:
                if (Keyboard.current.aKey.isPressed)
                {
                    transform.localScale = new Vector3 (-originalScale.x, originalScale.y, originalScale.z);
                    playerDirection = playerDirections.Left;
                }
                break;
            case playerDirections.Left:
                if (Keyboard.current.dKey.isPressed)
                {
                    transform.localScale = originalScale;
                    playerDirection = playerDirections.Right;
                }
                break;
            default:
                break;
        }

        playerAnimator.SetBool("isRunning", isRunning);
        playerAnimator.SetBool("isJumping", isJumping);
        playerAnimator.SetBool("isAttacking", isAttacking);

        transform.position += playerMovement * playerSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, 0);

        // Horizontal teleport
        if (transform.position.x > horizontalLimit)
        {
            transform.position = new Vector3 (-horizontalLimit, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -horizontalLimit)
        {
            transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
        }

        if (enemiesColliders.Count > 0)
        {
            playerRb.linearVelocity = new Vector2(0,0);
            playerRb.AddForce(transform.up * reboundForce, ForceMode2D.Impulse);
            killsCounter++;            
            enemiesColliders.Clear();
        }

        
        Debug.Log(killsCounter);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onAir = false;
            killsCounter = 0;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onAir = true;
        }
    }

    public void attack()
    {
        int enemies = Physics2D.OverlapCircle(attackPoint.transform.position, attackRadius, contactFilter, enemiesColliders);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRadius);
    }
}
