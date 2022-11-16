using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bookPage : MonoBehaviour
{
    private Image m_image;
    public Quest m_quest;


    private Button m_button;

    private GameObject note;
    private TextMeshProUGUI titleText;
    private TextMeshProUGUI mainText;
    
    
    
    [SerializeField]
    [Header("제목")]
    private string m_title;
    [SerializeField]
    [Header("내용")]
    [TextArea]
    private string m_mainText;


    private void Awake()
    {
        m_button = gameObject.AddComponent(typeof(Button)) as Button;
        m_button.onClick.AddListener(ChangeText);
        note = LobbyController.Instance.book_noteObj;
        titleText = note.transform.FindChild("title").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        mainText = note.transform.FindChild("main_note").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    private void ChangeText()
    {
        note.SetActive(true);
        titleText.SetText(m_title);
        mainText.SetText(m_mainText);
    }
}
