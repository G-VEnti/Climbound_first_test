using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restart_level : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Level restart
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Building_level");
        }
    }
}
