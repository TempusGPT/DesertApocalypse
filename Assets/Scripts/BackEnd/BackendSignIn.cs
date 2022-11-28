using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BackendSignIn : MonoBehaviour
{

    [SerializeField]
    private Text id;
    [SerializeField]
    private Text password;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TrySignIn()
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(id.text.ToString(),password.text.ToString());
        if (bro.IsSuccess())
        {
            Debug.Log("로그인에 성공했습니다");
            MessageInterface message = GetComponentInParent<MessageInterface>();
            message.ShowMessage("로그인 성공!");

            SceneManager.LoadScene(1);
        }
        else
        Debug.Log("로그인 실패.."+bro.GetMessage());

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
