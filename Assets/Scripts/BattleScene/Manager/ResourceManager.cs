using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager> {
    #region Path
    private static readonly string EnemyPath = "Enemies/";
    private static readonly string LevelPath = "Levels/";
    private static readonly string WeaponPath = "Weapons/";
    #endregion

    #region Field
    private Dictionary<int, BattleEntity> _enemyCacheDic;
    private Dictionary<WeaponType, WeaponBase> _weaponCacheDic;
    private SOLevelInfo[] _levels;
    #endregion

    #region Initialize
    public void Initialize() {
        if (_enemyCacheDic == null) {
            InitializeEnemy();
        }
        if (_levels == null) {
            InitializeLevel();
        }
        if (_weaponCacheDic == null) {
            InitializeWeapon();
        }
    }

    private void InitializeEnemy() {
        var enemyCache = Resources.LoadAll<BattleEntity>(EnemyPath);
        _enemyCacheDic = new Dictionary<int, BattleEntity>();
        
        foreach (var enemy in enemyCache) {
            _enemyCacheDic.Add(enemy.EntityID, enemy);
        }
    }

    private void InitializeLevel() {
        _levels = Resources.LoadAll<SOLevelInfo>(LevelPath);
    }

    private void InitializeWeapon() {
        var weaponCache = Resources.LoadAll<WeaponBase>(WeaponPath);
        _weaponCacheDic = new Dictionary<WeaponType, WeaponBase>();
        foreach (var weapon in weaponCache) {
            _weaponCacheDic.Add(weapon.WeaponID, weapon);
        }
    }

    #endregion

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
