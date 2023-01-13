//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BombDestroy : MonoBehaviour
//{
//    public int BombDamage = 20;

//    private void OnCollisionEnter(Collision collision)
//    {
//        충돌 대상의 태그가 Ground라면
//        if (collision.gameObject.tag == "Ground")
//        {
//            폭탄 삭제
//            Destroy(gameObject);
//        }

//        충돌 대상의 태그가 Player라면
//        if (collision.gameObject.tag == "Player")
//        {
//            폭탄 삭제
//            Destroy(gameObject);

//            Player의 데미지 함수 호출
//            GameObject.Find("Player").GetComponent<Player_Move>().DamageAction(BombDamage);
//        }

//    }
//}
