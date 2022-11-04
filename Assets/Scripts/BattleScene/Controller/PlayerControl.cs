using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityControlBase {
    [SerializeField]
    private BattleEntity _player = null;
    public BattleEntity Player {
        get { return _player; }
    }

    public override void Initialize() {
        _player.Initialize();
    }

    public override void Progress() {

    }

    public float GetFillAmounts() {
        return _player.GetHPPercent();
    }

    public override bool IsDefeated() {
        return false;
    }

    public override IEnumerator DoAttack(BattleMain battleControl, System.Action uiSetupCallback) {
        Debug.Log("플레이어 공격");

        var player = battleControl.PlayerCtrl.Player;

        player.DoAttack(battleControl.EnemyCtrl);
        uiSetupCallback.Invoke();

        yield return YieldInstructionCache.WaitForSeconds(0.5f);
    }
}
