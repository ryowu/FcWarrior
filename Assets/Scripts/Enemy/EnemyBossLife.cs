using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossLife : EnemyLife
{
    private int hp;
    [SerializeField] public HealthyBarController healthyBar;

    protected override void InnerStart()
    {
        base.InnerStart();
        hp = 0;
        healthyBar.SetHPMaxValue(300);
        healthyBar.SetHPValue(hp);
    }

    protected override void RefreshHPBar()
    {
        base.RefreshHPBar();
        healthyBar.SetHPValue(eData.EnemyHP);
    }

    protected override void OnEnemyDie()
    {
        GlobalVars.BossAbnormalSequenceEvent.StopBossBGM = true;
    }
}
