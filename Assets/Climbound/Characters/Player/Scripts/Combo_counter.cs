using TMPro;
using UnityEngine;

public class Combo_counter : MonoBehaviour
{

    public GameObject player;
    public GameObject comboTextPos;
    public TMP_Text killsCounterText;
    private int killsCounter;
    private Player_movement playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<Player_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = comboTextPos.transform.position;

        killsCounter = playerScript.killsCounter;

        killsCounterText.text = killsCounter.ToString();

        if (killsCounter == 0)
        {
            killsCounterText.gameObject.SetActive(false);
        }
        else
        {
            killsCounterText.gameObject.SetActive(true);
        }
    }
}
