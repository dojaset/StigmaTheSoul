//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BombDestroy : MonoBehaviour
//{
//    public int BombDamage = 20;

//    private void OnCollisionEnter(Collision collision)
//    {
//        �浹 ����� �±װ� Ground���
//        if (collision.gameObject.tag == "Ground")
//        {
//            ��ź ����
//            Destroy(gameObject);
//        }

//        �浹 ����� �±װ� Player���
//        if (collision.gameObject.tag == "Player")
//        {
//            ��ź ����
//            Destroy(gameObject);

//            Player�� ������ �Լ� ȣ��
//            GameObject.Find("Player").GetComponent<Player_Move>().DamageAction(BombDamage);
//        }

//    }
//}
