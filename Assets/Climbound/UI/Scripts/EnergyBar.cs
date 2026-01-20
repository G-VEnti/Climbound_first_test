using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    public Image energyBarSeg1;
    public Image energyBarSeg2;
    public Image energyBarSeg3;

    public Sprite energyBarSegFull1;
    public Sprite energyBarSegFull2;
    public Sprite energyBarSegFull3;

    public Sprite energyBarSegEmpty1;
    public Sprite energyBarSegEmpty2;
    public Sprite energyBarSegEmpty3;

    public GameObject player;

    private Player_movement playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = player.GetComponent<Player_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerScript.energyCounter)
        {
            case >= 3:
                energyBarSeg3.sprite = energyBarSegFull3;
                break;
            case 2:
                energyBarSeg2.sprite = energyBarSegFull2;
                break;
            case 1:
                energyBarSeg1.sprite = energyBarSegFull1;
                break;
            case 0:
                energyBarSeg1.sprite = energyBarSegEmpty1;
                energyBarSeg2.sprite = energyBarSegEmpty2;
                energyBarSeg3.sprite = energyBarSegEmpty3;
                break;
            default:
                break;
        }
    }
}
