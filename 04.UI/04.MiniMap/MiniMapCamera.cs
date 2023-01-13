using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{

    //public Transform target; // Player
    GameObject Player;

    public Quaternion rotatoion;
    public Vector3 offset; // 추후 조절
    private void Start()
    {
        Player = GameObject.Find("Player");
       
    }
    void Update()
    {
        if (Player != null)//target이 존재한다면 즉 플레이어가 존재한다면
        {
            transform.position = Player.transform.position + offset;
            transform.rotation = rotatoion;
        }
       
    }
}