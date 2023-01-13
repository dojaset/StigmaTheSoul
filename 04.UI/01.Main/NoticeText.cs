using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoticeText : MonoBehaviour
{
    TextMeshProUGUI noticeText;

    // Start is called before the first frame update
    void Start()
    {
        noticeText = GetComponent<TextMeshProUGUI>();
        noticeText.text = null;
    }

    public void ChangeText(string notice)
    {
        noticeText.text = notice;
    }
}
