using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_game : MonoBehaviour
{    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedButton()
    {
        SceneManager.LoadScene("Building_level");
    }

}
