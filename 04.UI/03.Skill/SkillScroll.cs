using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillScroll : MonoBehaviour
{
    // 슬롯들의 부모가 되는 LayoutGroup 오브젝트
    public GameObject skillParant;

    // 슬롯들의 리스트
    public List<Transform> slots = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        // 레이아웃 그룹의 열 개수가 리스트의 개수가 되게하기
        skillParant.GetComponent<GridLayoutGroup>().constraintCount = slots.Count;
    }

    // Update is called once per frame
    void Update()
    {
        // E 키를 누르면
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 슬롯의 0번째 오브젝트를 마지막으로 보내주기 (하이어라키뷰 제어)
            slots[0].SetAsLastSibling();

            // 슬롯의 0번째를 마지막으로 끼워넣기 (리스트 제어)
            slots.Insert(slots.Count, slots[0]);

            // 슬롯의 0번째 오브젝트는 이제 중복이므로 삭제해주기 (리스트 제어)
            slots.RemoveAt(0);
        }

        // Q 키를 누르면
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 슬롯의 마지막 오브젝트를 0번째로 보내주기 (하이어라키뷰 제어)
            slots[slots.Count - 1].SetAsFirstSibling();

            // 슬롯의 마지막을 0번째로 끼워넣기 (리스트 제어)
            slots.Insert(0, slots[slots.Count - 1]);

            // 슬롯의 마지막 오브젝트는 이제 중복이므로 삭제해주기 (리스트 제어)
            slots.RemoveAt(slots.Count - 1);
        }
    }
}