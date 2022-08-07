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
            Debug.Log("초기화 성공!");
            //CustomeSignUp();
        }
        else
        {
            Debug.LogError("초기화 실패! ");
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
            Debug.Log("회원가입 성공!");
        }
        else
        {
            Debug.LogError("회원가입 실패!");
            Debug.LogError(bro);
        }
    }
}
