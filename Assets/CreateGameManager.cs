using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameManager : MonoBehaviour
{
    public GameObject GameManager;
    void Start()
    {
        Instantiate(GameManager);  
    }

}
