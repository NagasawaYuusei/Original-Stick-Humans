using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMode : MonoBehaviour
{

    public GameObject ChangeButton;
    public int attackmode = 0;
    public int passivemode = 0;
    public int activemode = 0;

    public void Attack()
    {
        if (attackmode == 0)
        {
            attackmode = 1;
            ChangeButton.GetComponentInChildren<Text>().text = "Bow";
        }
        else
        {
            attackmode = 0;
            ChangeButton.GetComponentInChildren<Text>().text = "Sword";
        }
    }

    public void Passive()
    {
        if (passivemode == 0)
        {
            passivemode = 1;
            ChangeButton.GetComponentInChildren<Text>().text = "Flow";
        }
        else if(passivemode == 1)
        {
            passivemode = 2;
            ChangeButton.GetComponentInChildren<Text>().text = "Health";
        }
        else if (passivemode == 2)
        {
            passivemode = 3;
            ChangeButton.GetComponentInChildren<Text>().text = "Speed";
        }
        else
        {
            passivemode = 0;
            ChangeButton.GetComponentInChildren<Text>().text = "Double";
        }
    }

    public void Active()
    {
        if (activemode == 0)
        {
            activemode = 1;
            ChangeButton.GetComponentInChildren<Text>().text = "Wall";
        }
        else if (activemode == 1)
        {
            activemode = 2;
            ChangeButton.GetComponentInChildren<Text>().text = "Healing";
        }
        else if (activemode == 2)
        {
            activemode = 3;
            ChangeButton.GetComponentInChildren<Text>().text = "Stealth";
        }
        else
        {
            activemode = 0;
            ChangeButton.GetComponentInChildren<Text>().text = "Blink";
        }
    }


}
