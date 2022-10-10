using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMain : MonoBehaviour {
    public enum TurnInfo {
        PLAYER,
        ENEMY
    }

    [SerializeField] private PlayerControl _playerControl = null;
    public PlayerControl PlayerCtrl {
        get { return _playerControl; }
    }

    [SerializeField] private EnemyControl _enemyControl = null; 
    public EnemyControl EnemyCtrl {
        get { return _enemyControl; }
    }

    private TurnInfo _currentTurn;
    private int _turnCount;

    private ObserverSubject<BattleUIArgs> _onSkillUsedEvent;

    void Start() {
        _currentTurn = TurnInfo.PLAYER;
        _playerControl.Initialize();
        _enemyControl.Initialize();

        _onSkillUsedEvent = new ObserverSubject<BattleUIArgs>();
        _onSkillUsedEvent.Subscribe_And_Listen_CurrentData += GetComponent<BattleUI>().OnSkillUsed;

        StartCoroutine(StartTurn());
    }

    void Update() {
        _playerControl.Progress();
        _enemyControl.Progress();
    }

    public IEnumerator StartTurn() {
        ++_turnCount;

        EntityControlBase entity =
            (_currentTurn == TurnInfo.PLAYER) ? _playerControl as EntityControlBase : _enemyControl as EntityControlBase;
        yield return StartCoroutine(entity.DoAttack(this, SetupUI));

        EndTurn();
    }

    public void EndTurn() {
        int curTurn = (int)_currentTurn;
        int nextTurn = (curTurn + 1) % 2;
        _currentTurn = (TurnInfo)nextTurn;

        StartCoroutine(StartTurn());
    }

    private void SetupUI() {
        float playerHP = _playerControl.GetFillAmounts();
        float[] enemyHP = _enemyControl.GetFillAmounts();
        _onSkillUsedEvent.DoNotify(new BattleUIArgs(playerHP, enemyHP));
    }
}
