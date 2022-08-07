using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackEndTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var bro = Backend.Initialize(true);
        if(bro.IsSuccess())
        {
            Debug.Log("�ʱ�ȭ ����!");
            //CustomeSignUp();
        }
        else
        {
            Debug.LogError("�ʱ�ȭ ����! ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Backend.AsyncPoll();
    }

    public void CustomeSignUp()
    {
        string id = "user1";
        string password = "1234";

        var bro = Backend.BMember.CustomSignUp(id, password);
        if (bro.IsSuccess())
        {
            Debug.Log("ȸ������ ����!");
        }
        else
        {
            Debug.LogError("ȸ������ ����!");
            Debug.LogError(bro);
        }
    }
}
