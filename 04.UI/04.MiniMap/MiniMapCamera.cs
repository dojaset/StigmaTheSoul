using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{

    //public Transform target; // Player
    GameObject Player;

    public Quaternion rotatoion;
    public Vector3 offset; // ���� ����
    private void Start()
    {
        Player = GameObject.Find("Player");
       
    }
    void Update()
    {
        if (Player != null)//target�� �����Ѵٸ� �� �÷��̾ �����Ѵٸ�
        {
            transform.position = Player.transform.position + offset;
            transform.rotation = rotatoion;
        }
       
    }
}