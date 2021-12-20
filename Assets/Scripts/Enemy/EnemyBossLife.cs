using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossLife : EnemyLife
{
    [SerializeField] public HealthyBarController healthyBar;
    [SerializeField] public AudioSource HPrestoreEffect;

    protected override void InnerStart()
    {
        base.InnerStart();
        GlobalVars.BossDefeat = false;
        healthyBar.SetHPMaxValue(eData.EnemyMaxHP);
        healthyBar.SetHPValue(eData.EnemyMaxHP);
    }

    protected override void RefreshHPBar()
    {
        base.RefreshHPBar();
        healthyBar.SetHPValue(eData.EnemyHP);
    }

    protected override void OnEnemyDie()
    {
        GlobalVars.BossDefeat = true;
    }
}
