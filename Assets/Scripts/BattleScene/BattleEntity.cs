using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityInfo {
    public int entityID;
    public int maxHP;
    public int atk;
    public int def;
    public string name = null;

    public EntityInfo(int id, int hp, int atk, int def, string name)
    {
        this.entityID = id;
        this.maxHP = hp;
        this.atk = atk;
        this.def = def;
        this.name = name;
    }
}

public class BattleEntity : MonoBehaviour {
    protected int _maxHP;
    protected int _curHP;

    public int CurHP {
        get { return _curHP; }
    }

    public void Initialize() {
        _maxHP = _curHP = 100;
    }

    public float GetHPPercent() {
        return (float)_curHP / _maxHP;
    }

    public void DecreaseHP(int amount) {
        _curHP = Mathf.Max(_curHP - amount, 0);
    }
}
