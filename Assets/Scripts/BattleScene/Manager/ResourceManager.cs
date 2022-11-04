using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager> {
    private static readonly string EnemyPath = "Enemies/";
    private static readonly string LevelPath = "Levels/";
    private static readonly string WeaponPath = "Weapons/";

    private Dictionary<int, BattleEntity> _enemyCacheDic;
    private Dictionary<WeaponType, WeaponBase> _weaponCacheDic;
    private SOLevelInfo[] _levels;

    public void Initialize() {
        var enemyCache = Resources.LoadAll<BattleEntity>(EnemyPath);
        _enemyCacheDic = new Dictionary<int, BattleEntity>();
        
        foreach (var enemy in enemyCache) {
            _enemyCacheDic.Add(enemy.EntityID, enemy);
        }

        _levels = Resources.LoadAll<SOLevelInfo>(LevelPath);

        var weaponCache = Resources.LoadAll<WeaponBase>(WeaponPath);
        _weaponCacheDic = new Dictionary<WeaponType, WeaponBase>();
        foreach (var weapon in weaponCache) {
            _weaponCacheDic.Add(weapon.WeaponID, weapon);
        }
    }

    public BattleEntity GetEnemyPrefab(int id) {
        if (_enemyCacheDic == null) {
            Debug.LogError("Please Initialize First");
            Initialize();
        }
        
        BattleEntity ret = null;
        _enemyCacheDic.TryGetValue(id, out ret);
        return ret;
    }

    public SOLevelInfo GetLevel(int level) {
        if (level <= 0 || level > _levels.Length) {
            Debug.LogError("Level not exists");
            return null;
        }
        return _levels[level - 1];
    }

    public WeaponBase GetWeapon(WeaponType id) {
        if (_weaponCacheDic == null) {
            Debug.LogError("Please Initialize First");
            Initialize();
        }
        
        WeaponBase ret = null;
        _weaponCacheDic.TryGetValue(id, out ret);
        return ret;
    }
}
