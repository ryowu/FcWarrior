using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteRateController : MonoBehaviour
{
    [SerializeField] private Text txtCompleteRate;
    void Start()
    {
        int completeRate = 60;
        if (PlayerData.DoubleDef)
            completeRate += 8;
        if (PlayerData.DoubleGun)
            completeRate += 8;
        if (PlayerData.RecoverPowerUp)
            completeRate += 8;
        if (PlayerData.SideWeaponCooldownHalf)
            completeRate += 8;
        if (PlayerData.SideWeaponCostHalf)
            completeRate += 8;

        txtCompleteRate.text = string.Format("Íê³É¶È£º{0}%", completeRate);

    }
}
