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

    [SerializeField] private GameObject _battleStartUI = null;

    private void Awake() {
        ResourceManager.GetInstance().Initialize();
    }

    private void Start() {
        _currentTurn = TurnInfo.PLAYER;
        _playerControl.Initialize();
        _enemyControl.Initialize();

        _onSkillUsedEvent = new ObserverSubject<BattleUIArgs>();
        _onSkillUsedEvent.Subscribe_And_Listen_CurrentData += GetComponent<BattleUI>().OnSkillUsed;

        StartCoroutine(StartBattle());
    }

    void Update() {
        _playerControl.Progress();
        _enemyControl.Progress();
    }

    private IEnumerator StartBattle() {
        _battleStartUI.SetActive(true);

        yield return YieldInstructionCache.WaitForSeconds(1f);

        yield return StartCoroutine(_enemyControl.SpawnEnemy());

        yield return YieldInstructionCache.WaitForSeconds(0.5f);

        _battleStartUI.SetActive(false);

        StartCoroutine(StartTurn());
    }

    public IEnumerator StartTurn() {
        while (!_playerControl.IsDefeated() && !_enemyControl.IsDefeated()) {
            ++_turnCount;

            EntityControlBase entity =
                (_currentTurn == TurnInfo.PLAYER) ? _playerControl as EntityControlBase : _enemyControl as EntityControlBase;
            yield return StartCoroutine(entity.DoAttack(this, SetupUI));

            EndTurn();
        }

        if (_playerControl.IsDefeated()) {
            SceneSwitcher.ChangeScene();
            //SceneSwitcher.ChangeScene(4);
        }
        if (_enemyControl.IsDefeated()) {
            SceneSwitcher.ChangeScene();
        }
    }

    public void EndTurn() {
        int curTurn = (int)_currentTurn;
        int nextTurn = (curTurn + 1) % 2;
        _currentTurn = (TurnInfo)nextTurn;
    }

    private void SetupUI() {
        float playerHP = _playerControl.GetFillAmounts();
        float[] enemyHP = _enemyControl.GetFillAmounts();
        _onSkillUsedEvent.DoNotify(new BattleUIArgs(playerHP, enemyHP));
    }
}
