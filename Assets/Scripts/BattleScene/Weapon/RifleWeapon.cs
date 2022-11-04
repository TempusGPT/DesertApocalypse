using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapons/Rifle")]
public class RifleWeapon : WeaponBase {
    public override void Attack(BattleEntity caster, EnemyControl receiverController) {
        var receiver = receiverController.GetRandomTarget();
        int atk = caster.AttackPower + extraStatus.atk;
        receiver.ReceiveAttack(atk);
    }
}
