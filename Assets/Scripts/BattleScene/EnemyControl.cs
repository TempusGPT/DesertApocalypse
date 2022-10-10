using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SharedStat
{
    public int AttackPower;

    /*
    public SharedStat(Data.MonsterStat stat) {
        AttackPower = stat.AttackPower;
    }

    public SharedStat(Data.CharacterStat stat) {
        AttackPower = stat.AttackPower;
    }
    */
}

public class EnemyControl : EntityControlBase {
    [SerializeField]
    private List<BattleEntity> _currentEnemies = null;

    private float[] _fillAmounts;

    public override void Initialize() {
        _fillAmounts = new float[1];
        _currentEnemies[0].Initialize();
    }

    public override void Progress() {
        
    }

    public override bool IsDefeated() {
        return false;
    }

    public float[] GetFillAmounts() {
        for (int i = 0; i < _currentEnemies.Count; ++i) {
            _fillAmounts[i] = _currentEnemies[i].GetHPPercent();
        }
        return _fillAmounts;
    }

    public override IEnumerator DoAttack(BattleMain battleControl, System.Action uiSetupCallback) {
        Debug.Log("적 공격");

        ApplyAttack(battleControl.PlayerCtrl.Player, _currentEnemies[0]);
        uiSetupCallback.Invoke();

        yield return YieldInstructionCache.WaitForSeconds(0.5f);
    }

    public BattleEntity GetRandomTarget() {
        return _currentEnemies[0];
    }
}
