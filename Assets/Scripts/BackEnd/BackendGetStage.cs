using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class BackendGetStage : MonoBehaviour
{
    [SerializeField]
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        int stage = 0;
        var bro = Backend.GameData.GetMyData("stage_clear", new Where(), 1);
        if (bro.IsSuccess() == false)
        {
            Debug.Log(bro);
            return;
        }
        if (bro.GetReturnValuetoJSON()["rows"].Count <= 0)
        {
            //데이터가 없음 - 기본값 삽입
            Param param = new Param();
            Backend.GameData.Insert("stage_clear", param);
            Debug.Log(bro);
            return;
        }
        string inDate = bro.FlattenRows()[0]["inDate"].ToString();
        stage = int.Parse(bro.FlattenRows()[0]["stage"].ToString());
        Debug.Log(inDate);

        text.text = "Level 진행도 - "+stage.ToString()+"Level";
    }
}
