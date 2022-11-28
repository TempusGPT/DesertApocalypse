using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    WeaponTypeStarts = 100,
    Default,
    Shotgun,
    Knife,
    Katana,
    Rifle,
    Blunt,
    WeaponTypeEnds
}

public abstract class WeaponBase : ScriptableObject {
    public WeaponType WeaponID;
    public EntityStatus extraStatus;
    public abstract void Attack(BattleEntity caster, EnemyControl receiverController);
    public abstract string GetChangeTrigger();
}
