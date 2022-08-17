using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadCSV_Dialogue : MonoBehaviour
{
    [SerializeField]
    TextAsset asset;
    [SerializeField]
    Text textBox;
    [SerializeField]
    int ID;
    int PageNum = 1;
    string Background;
    string ReadText;
    string DialogueText;

    bool isEnd;

    string dialogueBG;
    int cursor = 0;
    int Maxcursor = 0;
    List<Dictionary<string, object>> data;

    DialogueController DialogueCtr;
    private void Awake()
    {
        DialogueCtr = GetComponent<DialogueController>();
         data = CSVReader.Read(asset);
        ReadPage();
        OnClickText();
        //print(ReadText.Length);
        //print(ReadText.Substring(256, 128));
    }

    private void ReadPage()
    {
        cursor = 0;
        Maxcursor = 0;
        for (int i = 0; i < data.Count; i++)
        {
            if (ID == (int)data[i]["ID"])
            {
                if (PageNum == (int)data[i]["Page"])
                {
                    ReadText = data[i]["Script"].ToString();
                    dialogueBG = data[i]["background"].ToString();
                    
                }
                    
               
            }
           
        }
    }
    public void OnClickText()
    {
        ShowText();
    }
    private void ShowText()
    {
        if (PageNum > data.Count)
        {
            DialogueCtr.ComplateDialogue();
            return;
        }

            DialogueCtr.ChangeBG(dialogueBG);
            int checkPage = PageNum;
            if (ReadText.Length > cursor + 128  )
                Maxcursor = 128;
            else
            {
                Maxcursor = ReadText.Length - cursor;
                PageNum++;
                //ReadPage();
            }

            if(ReadText.IndexOf('*', 0, ReadText.Length) > -1)
            {
            int removecursor = ReadText.IndexOf('*', cursor, Maxcursor);
            Maxcursor = ReadText.IndexOf('*', cursor, Maxcursor) - cursor;
                ReadText = ReadText.Remove(removecursor, 1).Insert(removecursor, "");
                print(cursor + "," + Maxcursor + ","+ ReadText);
                
             }

        DialogueText = ReadText.Substring(cursor, Maxcursor);
        //DialogueText = DialogueText.Replace('*', ' ');

            cursor += Maxcursor;
        print(cursor + "," + Maxcursor);

           textBox.text = DialogueText;
            if (PageNum != checkPage)
                ReadPage();
            
           
    }
}
