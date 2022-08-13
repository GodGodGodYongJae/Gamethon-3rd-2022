using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVersionCheckMrg : MonoBehaviour
{

    public string url;
    // Start is called before the first frame update

    public string _bundleIdentifier { 
        get {
            return url.Substring(url.IndexOf("details"), url.LastIndexOf("details") + 1); 
        } 
    }
    void Start()
    {
        StartCoroutine(PlayStoreVersionCheck());
    }

    private IEnumerator PlayStoreVersionCheck()
    {
        WWW www = new WWW(url); 
            yield return www;         //인터넷 연결 에러가 없다면,       
        if (www.error == null)
        {
            int index = www.text.IndexOf("softwareVersion");
            Debug.Log(index);
            string versionText = www.text.Substring(index, 30);
            int softwareVersion = versionText.IndexOf(">"); 
            string playStoreVersion = versionText.Substring(softwareVersion + 1, Application.version.Length + 1);
            Debug.Log(playStoreVersion.Trim());
        }
        else
        {
            Debug.Log(www.error);
            //https://genieker.tistory.com/149
            // 지금은 낫파운드 뜸 
        }
    }

  
}
