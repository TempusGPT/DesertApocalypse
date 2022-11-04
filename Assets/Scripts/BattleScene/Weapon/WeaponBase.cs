using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : ScriptableObject, IWeaponBehaviour {
    public abstract void Attack(BattleEntity caster, EnemyControl receiverController);
}
