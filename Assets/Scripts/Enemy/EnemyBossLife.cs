using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossLife : EnemyLife
{
    private int hp;
    [SerializeField] private HealthyBarController healthyBar;

    protected override void InnerStart()
    {
        base.InnerStart();
        hp = 300;
        healthyBar.SetHPMaxValue(hp);
        healthyBar.SetHPValue(hp);
    }

    protected override void RefreshHPBar()
    {
        base.RefreshHPBar();
        healthyBar.SetHPValue(eData.EnemyHP);
    }
}
