using UnityEngine;
using BackEnd;

public class BackendInit : MonoBehaviour
{
    void Start()
    {
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            // �ʱ�ȭ ���� �� ����
            Debug.Log("�ʱ�ȭ ����!");
        }
        else
        {
            // �ʱ�ȭ ���� �� ����
            Debug.LogError("�ʱ�ȭ ����!");
        }
    }
}

