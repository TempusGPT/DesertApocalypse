using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapons/Default")]
public class DefaultWeapon : WeaponBase {
    public override void Attack(BattleEntity caster, EnemyControl receiverController) {
        var receiver = receiverController.GetRandomTarget();
        receiver.ReceiveAttack(caster.AttackPower);
    }
}
