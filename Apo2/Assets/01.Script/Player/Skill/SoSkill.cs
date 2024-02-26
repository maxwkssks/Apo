using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoSkill : BaseSkill
{
    public GameObject SkillPrefab;
    public override void Activate()
    {
        base.Activate();
        Instantiate(SkillPrefab, GameManager.Instance.GetPlayerCharacter().transform.position, Quaternion.identity);
        SkillPrefab.transform.position = Vector3.up * Time.deltaTime;
    }




}
