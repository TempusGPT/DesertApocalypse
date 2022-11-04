using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        var enemy = _currentEnemies[0];

        var player = battleControl.PlayerCtrl.Player;
        player.ReceiveAttack(enemy.AttackPower);
        uiSetupCallback.Invoke();

        yield return YieldInstructionCache.WaitForSeconds(0.5f);
    }

    public BattleEntity GetRandomTarget() {
        return _currentEnemies[0];
    }

    public List<BattleEntity> GetRandomTargets(int targetCounts)
    {
        int numOfEntities = _currentEnemies.Count;
        var entityList = _currentEnemies.ToList();

        if (targetCounts < numOfEntities) {
            int diff = numOfEntities - targetCounts;
            for (int i = 0; i < diff; ++i) {
                int removeIndex = Random.Range(0, entityList.Count);
                entityList.RemoveAt(removeIndex);
            }
        }
        return entityList;
    }
}
