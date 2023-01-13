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
        // �� ������Ʈ�� 0��° �ڽ��� �����̴��� �����´�.
        slider1 = transform.GetChild(0).GetComponent<Slider>();

        // �� ������Ʈ�� 1��° �ڽ��� �����̴��� �����´�.
        slider2 = transform.GetChild(1).GetComponent<Slider>();

        // + - ��ư�� ������ ���� �ؽ�Ʈ�� ���ϵ��� ������ش�.
        bgmtext_text = GameObject.Find("BgmValueText").GetComponent<TextMeshProUGUI>();

        // + - ��ư�� ������ ���� �ؽ�Ʈ�� ���ϵ��� ������ش�.
        effect_text = GameObject.Find("EffectValueText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //�� �����Ӹ��� �� �����̴����� �������� �ؽ�Ʈ�ε� ȭ�鿡 ���̵��� ������ش�.
        bgmtext_text.text = slider1.value.ToString();
        effect_text.text = slider2.value.ToString();
    }
    public void BgmPlusBtn()
    {
        //��ư�� ������ ���� �����̴��� �������� 0.1�� �����ϴ� �ڵ�
        slider1.value += 1;
        SoundManager.Instance.audioSource.volume += 0.1f;
    }
    public void BgmMinusBtn()
    {
        //��ư�� ������ �Ŵ� �����̴��� �������� 0.1�� �����ϴ� �ڵ�
        slider1.value -= 1;
        SoundManager.Instance.audioSource.volume -= 0.1f;
    }
    public void EffectPlusBtn()
    {
        //��ư�� ������ ���� �����̴��� �������� 0.1�� �����ϴ� �ڵ�
        slider2.value += 1;
        Player_Data.Instance.audioSource.volume += 0.1f;
    }
    public void EffectMinusBtn()
    {
        //��ư�� ������ �Ŵ� �����̴��� �������� 0.1�� �����ϴ� �ڵ�
        slider2.value -= 1;
        Player_Data.Instance.audioSource.volume -= 0.1f;
    }
}