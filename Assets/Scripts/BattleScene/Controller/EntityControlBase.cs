using System.Collections;
using UnityEngine;

public abstract class EntityControlBase : MonoBehaviour {
    /// 초기화 단계에서 호출되는 함수. 프레임워크에 의해 호출됨.
    public abstract void Initialize();
    /// 매 프레임마다 호출하는 함수. 프레임워크에 의해 호출됨.
    public abstract void Progress();
    public abstract bool IsDefeated();
    public abstract IEnumerator DoAttack(BattleMain battleControl, System.Action uiSetupCallback);
}