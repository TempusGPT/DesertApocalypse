using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct BattleUIArgs {
    public float playerHPFillAmounts;
    public float[] enemyHPFillAmounts;

    public BattleUIArgs(float playerHP, float[] enemyHP) {
        playerHPFillAmounts = playerHP;
        enemyHPFillAmounts = enemyHP;
    }
}

public class BattleUI : MonoBehaviour {
    [SerializeField] private Image _playerHPUIImage = null;
    [SerializeField] private Image[] _enemyHPUIImages = null;

    public void OnSkillUsed(BattleUIArgs args) {
        OnPlayerHPChanged(args.playerHPFillAmounts);
        OnEnemyHPChanged(args.enemyHPFillAmounts);
    }

    private void OnPlayerHPChanged(float amount) {
        SetImageFillAmount(_playerHPUIImage, amount);
    }

    private void OnEnemyHPChanged(float[] args) {
        for (int i = 0; i < _enemyHPUIImages.Length; ++i) {
            if (args == null || args.Length <= i) continue;
            _enemyHPUIImages[i].transform.parent.gameObject.SetActive(true);
            SetImageFillAmount(_enemyHPUIImages[i], args[i]);
        }
    }

    private void SetImageFillAmount(Image image, float amount) {
        image.fillAmount = amount;
    }
}
