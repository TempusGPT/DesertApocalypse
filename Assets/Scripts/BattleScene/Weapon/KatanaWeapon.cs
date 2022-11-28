using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapons/Katana")]
public class KatanaWeapon : WeaponBase {
    private static readonly string ChangeTrigger = "changeToKatana";
    public override void Attack(BattleEntity caster, EnemyControl receiverController) {
        var receivers = receiverController.GetRandomTargets(2);
        int atk = caster.AttackPower + extraStatus.atk;
        
        foreach (BattleEntity receiver in receivers) {
            receiver.ReceiveAttack(atk);
        }
    }

    public override string GetChangeTrigger() {
        return ChangeTrigger;
    }
}
