using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern1 : MonoBehaviour
{
    public float MoveSpeed;
    public float Amplitude; // 패턴의 진폭(위아래 이동 거리)

    private bool movingUp = true;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float verticalMovement = MoveSpeed * Time.deltaTime;

        // 위로 이동 중이면서 현재 위치가 시작 위치보다 진폭보다 작은 경우
        if (movingUp && transform.position.x < startPosition.x + Amplitude)
        {
            transform.position += new Vector3(verticalMovement, 0f , 0f);
        }
        // 아래로 이동 중이면서 현재 위치가 시작 위치보다 진폭보다 큰 경우
        else if (!movingUp && transform.position.x > startPosition.x - Amplitude)
        {
            transform.position -= new Vector3(verticalMovement, 0f, 0f);
        }
        // 진폭 범위를 벗어날 경우 이동 방향을 반대로 변경
        else
        {
            movingUp = !movingUp;
        }

        transform.position -= new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
    }
}
