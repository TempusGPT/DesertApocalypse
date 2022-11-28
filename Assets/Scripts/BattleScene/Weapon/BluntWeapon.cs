using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapons/Blunt")]
public class BluntWeapon : WeaponBase {
    private static readonly string ChangeTrigger = "changeToBlunt";
    public override void Attack(BattleEntity caster, EnemyControl receiverController) {
        var receiver = receiverController.GetRandomTarget();

        int atk = caster.AttackPower + extraStatus.atk;
        receiver.ReceiveAttack(atk);
    }

    public override string GetChangeTrigger() {
        return ChangeTrigger;
    }
}
