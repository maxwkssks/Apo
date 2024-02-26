using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSwordSkill : MonoBehaviour
{

    public float speed = 5f; // �̵� �ӵ�

    void Update()
    {
        MoveUpward();
    }

    void MoveUpward()
    {
        // ��ü�� ���� �̵���Ű�� ���� ���
        Vector2 upwardDirection = Vector2.up * speed * Time.deltaTime;

        // Rigidbody�� �̿��Ͽ� �̵�
        GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + upwardDirection);
    }
}
