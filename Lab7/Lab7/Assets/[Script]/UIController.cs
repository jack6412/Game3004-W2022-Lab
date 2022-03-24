using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text healthNum;

    public void OnHealth()
    {
        healthNum.text = healthBar.value.ToString();
    }

    public void takeDamage(int num)
    {
        healthBar.value -= num;
    }

    
}
