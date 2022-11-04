using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageInterface : MonoBehaviour
{
    [SerializeField]
    private MessagePopup messagePopup;
    [SerializeField]
    private PopopManager popopManager;

    public void ShowMessage(string str)
    {
        messagePopup.InitMessage(str);
        popopManager.PushPopup(messagePopup.gameObject);
    }
}
