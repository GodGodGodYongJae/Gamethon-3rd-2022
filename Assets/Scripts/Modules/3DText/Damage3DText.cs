using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage3DText : MonoBehaviour
{
    TextMesh textMesh;
    GameObject child;
    private void Awake()
    {
        child = transform.Find("Text").gameObject;
        textMesh = child.GetComponent<TextMesh>();
    }
    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Camera.main.transform);
        if(Camera.main != null)
        this.transform.LookAt(this.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }
    public void Damage(int dmg)
    {
       
        textMesh.text = dmg.ToString();
    }
}
