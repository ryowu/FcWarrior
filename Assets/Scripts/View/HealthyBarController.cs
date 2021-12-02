using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthyBarController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetHPValue(int hp)
    {
        if (hp < 0) hp = 0;
        slider.value = hp;
    }

    public void SetHPMaxValue(int maxHp)
    {
        slider.maxValue = maxHp;
    }
}
