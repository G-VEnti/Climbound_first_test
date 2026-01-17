using UnityEngine;
using UnityEngine.InputSystem;

public class Death : MonoBehaviour
{
    private bool isAttacked = false;

    private Animator enemyAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimator.SetBool("isAttacked", isAttacked);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") && Keyboard.current.shiftKey.isPressed)
        {
            isAttacked = true;
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
