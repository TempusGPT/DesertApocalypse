using UnityEngine;
using UnityEngine.UI;

public class MessagePopup : MonoBehaviour
{
    [SerializeField]
    private Text text;
    public void InitMessage(string str)
    {
        text.text = str;
    }
}
