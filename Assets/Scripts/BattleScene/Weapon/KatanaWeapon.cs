using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapons/Katana")]
public class KatanaWeapon : WeaponBase {
    public override void Attack(BattleEntity caster, EnemyControl receiverController) {
        var receivers = receiverController.GetRandomTargets(2);
        foreach (BattleEntity receiver in receivers) {
            receiver.ReceiveAttack(caster.AttackPower);
        }
    }
}
