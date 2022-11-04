using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapons/Knife")]
public class KnifeWeapon : WeaponBase {
    public override void Attack(BattleEntity caster, EnemyControl receiverController) {
        var receiver = receiverController.GetRandomTarget();

        int atk = caster.AttackPower + extraStatus.atk;
        receiver.ReceiveAttack(atk);
        receiver.ReceiveAttack(atk);
    }
}