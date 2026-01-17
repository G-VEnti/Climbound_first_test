using UnityEngine;

public class Camera_movement : MonoBehaviour
{

    Vector3 cameraMovement = new Vector3(0, 0, 0);

    private Rigidbody2D cameraRb;

    public float cameraMovementSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += cameraMovement * cameraMovementSpeed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cameraMovement.y = 1;
            Debug.Log("Camera should move");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cameraMovement.y = 0;
            Debug.Log("Camera exit");
        }
    }

}
