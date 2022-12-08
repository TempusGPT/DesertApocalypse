using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityControlBase {
    [SerializeField]
    private BattleEntity _player = null;
    public BattleEntity Player {
        get { return _player; }
    }
    private bool _isDefeated = false;

    public static int PlayerHP = 100;

    public override void Initialize() {
        _player.Initialize();

        int randomType = Random.Range((int)WeaponType.WeaponTypeStarts + 2, (int)WeaponType.WeaponTypeEnds);
        _player.SetupWeapon((WeaponType)randomType);
    }

    public override void Progress() {
        if (_isDefeated)
            return;
        if (IsDefeated()) {
            _isDefeated = true;
            _player.StartDeadAnimation();
        }
    }

    public float GetFillAmounts() {
        return _player.GetHPPercent();
    }

    public override bool IsDefeated() {
        return _player.GetHPPercent() < Mathf.Epsilon;
    }

    public override IEnumerator DoAttack(BattleMain battleControl, System.Action uiSetupCallback) {
        Debug.Log("플레이어 공격");

        var player = battleControl.PlayerCtrl.Player;

        player.DoAttack(battleControl.EnemyCtrl);
        uiSetupCallback.Invoke();

        yield return YieldInstructionCache.WaitForSeconds(0.5f);
    }
}
