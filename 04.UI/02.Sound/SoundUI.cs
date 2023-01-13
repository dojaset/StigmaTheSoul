using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundUI : MonoBehaviour
{
    Slider slider1;
    Slider slider2;
    TextMeshProUGUI bgmtext_text;
    TextMeshProUGUI effect_text;

    void Start()
    {
        // 이 오브젝트의 0번째 자식의 슬라이더를 가져온다.
        slider1 = transform.GetChild(0).GetComponent<Slider>();

        // 이 오브젝트의 1번째 자식의 슬라이더를 가져온다.
        slider2 = transform.GetChild(1).GetComponent<Slider>();

        // + - 버튼이 눌릴때 마다 텍스트가 변하도록 만들어준다.
        bgmtext_text = GameObject.Find("BgmValueText").GetComponent<TextMeshProUGUI>();

        // + - 버튼이 눌릴때 마다 텍스트가 변하도록 만들어준다.
        effect_text = GameObject.Find("EffectValueText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //매 프레임마다 각 슬라이더들의 벨류값을 텍스트로도 화면에 보이도록 만들어준다.
        bgmtext_text.text = slider1.value.ToString();
        effect_text.text = slider2.value.ToString();
    }
    public void BgmPlusBtn()
    {
        //버튼을 누를때 마다 슬라이더의 벨류값이 0.1씩 증가하는 코드
        slider1.value += 1;
        SoundManager.Instance.audioSource.volume += 0.1f;
    }
    public void BgmMinusBtn()
    {
        //버튼을 누를때 매다 슬라이더의 벨류값이 0.1씩 감소하는 코드
        slider1.value -= 1;
        SoundManager.Instance.audioSource.volume -= 0.1f;
    }
    public void EffectPlusBtn()
    {
        //버튼을 누를때 마다 슬라이더의 벨류값이 0.1씩 증가하는 코드
        slider2.value += 1;
        Player_Data.Instance.audioSource.volume += 0.1f;
    }
    public void EffectMinusBtn()
    {
        //버튼을 누를때 매다 슬라이더의 벨류값이 0.1씩 감소하는 코드
        slider2.value -= 1;
        Player_Data.Instance.audioSource.volume -= 0.1f;
    }
}