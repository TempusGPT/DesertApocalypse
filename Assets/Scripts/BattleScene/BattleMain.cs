using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Button[] _playerButton = null;

    private bool _startAttack;

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

        SetupUI();

        yield return YieldInstructionCache.WaitForSeconds(0.5f);

        _battleStartUI.SetActive(false);

        StartCoroutine(StartTurn());
    }

    public IEnumerator StartTurn() {
        while (!_playerControl.IsDefeated() && !_enemyControl.IsDefeated()) {
            ++_turnCount;

            foreach (Button btn in _playerButton) {
                btn.interactable = (_currentTurn == TurnInfo.PLAYER);
            }

            if (_currentTurn == TurnInfo.ENEMY) {
                _startAttack = true;
            }

            yield return new WaitUntil(() => _startAttack );
            _startAttack = false;

            EntityControlBase entity =
                (_currentTurn == TurnInfo.PLAYER) ? _playerControl as EntityControlBase : _enemyControl as EntityControlBase;

            yield return StartCoroutine(entity.DoAttack(this, SetupUI));

            EndTurn();
        }

        if (_playerControl.IsDefeated()) {
            SceneSwitcher.ChangeGameoverScene();
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

    public void StartAttack() {
        _startAttack = true;
    }

    public void ChangeWeapon() {
        EndTurn();
        _startAttack = true;
        _playerControl.SetToRandomWeapon();
    }
}
