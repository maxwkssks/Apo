using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseSkill : MonoBehaviour
{
    protected CharacterManager _characterManager;
    public float CooldownTime;
    public float CurrentTime;
    public bool bIsCoolDown = false;

    public void Init(CharacterManager characterManager)
    {
        _characterManager = characterManager;
    }

    public void Update()
    {
        if(bIsCoolDown)
        {
            CurrentTime -= Time.deltaTime;
            if(CurrentTime <= 0)
            {
                bIsCoolDown = false;
            }
        }
    }

    public bool IsAvailable()
    {
        // 스킬이 쿨다운 중인지 확인
        return !bIsCoolDown;
    }

    public virtual void Activate()
    {
        bIsCoolDown = true;
        CurrentTime = CooldownTime;
    }

    public void InitCoolDown()
    {
        bIsCoolDown = false;
        CurrentTime = 0;
    }

}