using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSwordSkill : MonoBehaviour
{

    public float speed = 5f; // 이동 속도

    void Update()
    {
        MoveUpward();
    }

    void MoveUpward()
    {
        // 물체를 위로 이동시키는 벡터 계산
        Vector2 upwardDirection = Vector2.up * speed * Time.deltaTime;

        // Rigidbody를 이용하여 이동
        GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + upwardDirection);
    }
}
