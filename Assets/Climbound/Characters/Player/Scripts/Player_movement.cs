using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_movement : MonoBehaviour
{
    public float playerSpeed;
    public float jumpForce;
    public float reboundForce;
    public float verticalAttackForce;
    public float attackRadius;
    public float cameraSpeed;
    
    private float rayLength = 0.2f;
    private float horizontalLimit = 9;

    public int killsCounter;

    private Animator playerAnimator;

    private bool isRunning = false;
    private bool isJumping = false;
    private bool isAttacking = false;

    private bool verticalAttackActive = true;

    private enum playerDirections { Right, Left };
    playerDirections playerDirection;

    public SpriteRenderer playerSprite;
    public GameObject attackPoint;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject energyFull;
    public GameObject energyEmpty;
    public LayerMask enemiesLayer;
    public LayerMask groundLayer;
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

        energyEmpty.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded() == true)
        {
            killsCounter = 0;
        }

        Debug.DrawRay(leftFoot.transform.position, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(rightFoot.transform.position, Vector2.down * rayLength, Color.red);

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
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded() == true)
        {
            playerRb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        // Vertical attack
        if (Keyboard.current.wKey.wasPressedThisFrame && verticalAttackActive == true)
        {
            playerRb.linearVelocity = new Vector2(0, 0);
            playerRb.AddForce(transform.up * verticalAttackForce, ForceMode2D.Impulse);
            verticalAttackActive = false;

            energyFull.SetActive(false);
            energyEmpty.SetActive(true);
        }

        // Level restart

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Building_level");
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
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded() == true)
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
                    transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
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
            transform.position = new Vector3(-horizontalLimit, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -horizontalLimit)
        {
            transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
        }

        // Rebound
        if (enemiesColliders.Count > 0)
        {
            // Actives vertical attack
            verticalAttackActive = true;
            energyEmpty.SetActive(false);
            energyFull.SetActive(true);
            // Resets player rigibody velocity
            playerRb.linearVelocity = new Vector2(0, 0);
            playerRb.AddForce(transform.up * reboundForce, ForceMode2D.Impulse);
            killsCounter++;
            enemiesColliders.Clear();
        }


        // Camera following
        if (transform.position.y <= (Camera.main.transform.position.y - 3.5)|| transform.position.y >= Camera.main.transform.position.y)
        {
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, new Vector3(0, transform.position.y, Camera.main.transform.position.z), cameraSpeed * Time.deltaTime);
        }
    }



    private bool isGrounded()
    {
        RaycastHit2D floorHitLeftFoot;
        RaycastHit2D floorHitRightFoot;

        bool touchingGround;

        floorHitLeftFoot = Physics2D.Raycast(leftFoot.transform.position, Vector2.down, rayLength, groundLayer);
        floorHitRightFoot = Physics2D.Raycast(rightFoot.transform.position, Vector2.down, rayLength, groundLayer);

        if (floorHitLeftFoot == true || floorHitRightFoot == true)
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }

        return touchingGround;
    }



    public void enemiesAttacked()
    {
        int enemies = Physics2D.OverlapCircle(attackPoint.transform.position, attackRadius, contactFilter, enemiesColliders);

        foreach (var i in enemiesColliders)
        {
            Death deathScript = i.GetComponent<Death>();
            deathScript.beingAttacekd();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRadius);
    }
}
