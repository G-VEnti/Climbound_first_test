using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    public Image energyBarSeg1;
    public Image energyBarSeg2;
    public Image energyBarSeg3;

    public Image healthBarSeg1;
    public Image healthBarSeg2;
    public Image healthBarSeg3;
    public Image healthBarSeg4;
    public Image healthBarSeg5;


    public Sprite energyBarSegFull1;
    public Sprite energyBarSegFull2;
    public Sprite energyBarSegFull3;

    public Sprite healthBarSegFull1;
    public Sprite healthBarSegFull2;
    public Sprite healthBarSegFull3;


    public Sprite energyBarSegEmpty1;
    public Sprite energyBarSegEmpty2;
    public Sprite energyBarSegEmpty3;

    public Sprite healthBarSegEmpty1;
    public Sprite healthBarSegEmpty2;
    public Sprite healthBarSegEmpty3;


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


        switch (playerScript.playerHealth)
        {
            case 100:
                healthBarSeg1.sprite = healthBarSegFull1;
                healthBarSeg2.sprite = healthBarSegFull2;
                healthBarSeg3.sprite = healthBarSegFull2;
                healthBarSeg4.sprite = healthBarSegFull2;
                healthBarSeg5.sprite = healthBarSegFull3;
                break;
            case 80:
                healthBarSeg1.sprite = healthBarSegFull1;
                healthBarSeg2.sprite = healthBarSegFull2;
                healthBarSeg3.sprite = healthBarSegFull2;
                healthBarSeg4.sprite = healthBarSegFull2;
                healthBarSeg5.sprite = healthBarSegEmpty3;
                break;
            case 60:
                healthBarSeg1.sprite = healthBarSegFull1;
                healthBarSeg2.sprite = healthBarSegFull2;
                healthBarSeg3.sprite = healthBarSegFull2;
                healthBarSeg4.sprite = healthBarSegEmpty2;
                healthBarSeg5.sprite = healthBarSegEmpty3;
                break;
            case 40:
                healthBarSeg1.sprite = healthBarSegFull1;
                healthBarSeg2.sprite = healthBarSegFull2;
                healthBarSeg3.sprite = healthBarSegEmpty2;
                healthBarSeg4.sprite = healthBarSegEmpty2;
                healthBarSeg5.sprite = healthBarSegEmpty3;
                break;
            case 20:
                healthBarSeg1.sprite = healthBarSegFull1;
                healthBarSeg2.sprite = healthBarSegEmpty2;
                healthBarSeg3.sprite = healthBarSegEmpty2;
                healthBarSeg4.sprite = healthBarSegEmpty2;
                healthBarSeg5.sprite = healthBarSegEmpty3;
                break;
            case <= 0:
                healthBarSeg1.sprite = healthBarSegEmpty1;
                healthBarSeg2.sprite = healthBarSegEmpty2;
                healthBarSeg3.sprite = healthBarSegEmpty2;
                healthBarSeg4.sprite = healthBarSegEmpty2;
                healthBarSeg5.sprite = healthBarSegEmpty3;
                break;
            default:
                break;
        }
    }
}
