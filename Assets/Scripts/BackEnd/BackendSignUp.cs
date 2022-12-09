using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class BackendSignUp : MonoBehaviour
{
    [SerializeField]
    private Text id;
    [SerializeField]
    private Text password;
    [SerializeField]
    private Text nickname;

    public void TrySignUp()
    {
        BackendReturnObject bro = Backend.BMember.CustomSignUp(id.text, password.text);

        MessageInterface message = GetComponentInParent<MessageInterface>();
        PopopManager popopManager = GetComponentInParent<PopopManager>();
        if (bro.IsSuccess())
        {
            popopManager.ClosePopup();
            message.ShowMessage("ȸ�������� �Ϸ��߽��ϴ�.");
            Backend.BMember.CreateNickname(nickname.text);
        }
        else
        {
            Debug.Log(bro.GetMessage());
            message.ShowMessage(bro.GetMessage());
        }
        /*SendQueue.Enqueue(Backend.BMember.CustomSignUp, id.text, password.text, callback => {
            MessageInterface message = GetComponentInParent<MessageInterface>();
            PopopManager popopManager = GetComponentInParent<PopopManager>();
            if (callback.IsSuccess())
            {
                popopManager.ClosePopup();
                message.ShowMessage("ȸ�������� �Ϸ��߽��ϴ�.");
            }
            else
            {
                message.ShowMessage(callback.GetMessage());

            }
        });*/
    }
}
