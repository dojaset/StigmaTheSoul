using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemInBox : MonoBehaviour
{
    public GameObject[] Items;

    int idx = 3;
    private void OnTriggerStay(Collider other)
    {
        int rIdx = Random.Range(0, Items.Length);

        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("æ∆¿Ã≈€ »πµÊ");
                for (int i = 0; i <= idx; i++)
                {
                    Instantiate(Items[rIdx], transform);
                }
            }
        }
    }
}
