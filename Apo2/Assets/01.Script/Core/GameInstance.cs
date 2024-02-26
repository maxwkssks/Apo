using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;

    public float GameStartTime = 0f;
    public int Score = 0;
    public int CurrentStageLevel = 1;

    public int CurrentPlayerWeaponLevel = 0;
    public int CurrentPlayerHP = 3;
    public float CurrentPlayerFuel = 100f;

    private void Awake()
    {
        if (instance == null)  // 단 하나만 존재하게끔
        {
            instance = this;  // 객체 생성시 instance에 자기 자신을 넣어줌
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        GameStartTime = Time.time;
    }
}