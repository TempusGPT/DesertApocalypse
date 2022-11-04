using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapons/Katana")]
public class KatanaWeapon : WeaponBase {
    public override void Attack(BattleEntity caster, EnemyControl receiverController) {
        var receivers = receiverController.GetRandomTargets(2);
        int atk = caster.AttackPower + extraStatus.atk;
        
        foreach (BattleEntity receiver in receivers) {
            receiver.ReceiveAttack(atk);
        }
    }
}
