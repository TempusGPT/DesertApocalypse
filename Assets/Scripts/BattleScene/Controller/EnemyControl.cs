using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyControl : EntityControlBase {
    [SerializeField] private Transform[] _enemySpawnPositions = null;
    private List<BattleEntity> _currentEnemies;

    private float[] _fillAmounts;

    public override void Initialize() {
        var resourceManager = ResourceManager.GetInstance();
        SOLevelInfo levelInfo = resourceManager.GetLevel(1);
        RoomInfo room = levelInfo.roomInformation[Random.Range(0, levelInfo.roomInformation.Length)];
        _currentEnemies = new List<BattleEntity>();

        for (int i = 0; i < room.enemyID.Length; ++i) {
            int eid = room.enemyID[i];
            var prefab = resourceManager.GetEnemyPrefab(eid);
            var enemy = Instantiate(prefab, _enemySpawnPositions[i].position, Quaternion.identity);

            enemy.Initialize();
            _currentEnemies.Add(enemy);
        }

        _fillAmounts = new float[_currentEnemies.Count];
    }

    public override void Progress() {
        
    }

    public override bool IsDefeated() {
        return false;
    }

    public float[] GetFillAmounts() {
        for (int i = 0; i < _currentEnemies.Count; ++i) {
            _fillAmounts[i] = _currentEnemies[i].GetHPPercent();
        }
        return _fillAmounts;
    }

    public override IEnumerator DoAttack(BattleMain battleControl, System.Action uiSetupCallback) {
        Debug.Log("적 공격");

        var enemy = _currentEnemies[0];

        var player = battleControl.PlayerCtrl.Player;
        player.ReceiveAttack(enemy.AttackPower);
        uiSetupCallback.Invoke();

        yield return YieldInstructionCache.WaitForSeconds(0.5f);
    }

    public BattleEntity GetRandomTarget() {
        int idx = Random.Range(0, _currentEnemies.Count);
        return _currentEnemies[idx];
    }

    public List<BattleEntity> GetRandomTargets(int targetCounts)
    {
        int numOfEntities = _currentEnemies.Count;
        var entityList = _currentEnemies.ToList();

        if (targetCounts < numOfEntities) {
            int diff = numOfEntities - targetCounts;
            for (int i = 0; i < diff; ++i) {
                int removeIndex = Random.Range(0, entityList.Count);
                entityList.RemoveAt(removeIndex);
            }
        }
        return entityList;
    }
}
