using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
public class BackendNickname : MonoBehaviour
{
    [SerializeField]
    Text text;
    void Start()
    {
        text.text = "NickName: "+ Backend.UserNickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
