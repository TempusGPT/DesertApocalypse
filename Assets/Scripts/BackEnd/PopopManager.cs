using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopopManager : MonoBehaviour
{
    private Stack<GameObject> PopupStack;
    private void Awake()
    {
        PopupStack = new Stack<GameObject>();
    }
    public void PushPopup(GameObject popup)
    {
        PopupStack.Push(popup);
        popup.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ClosePopup();
    }
    public void ClosePopup()
    {
        PopupStack.Pop().SetActive(false);
    }
}
