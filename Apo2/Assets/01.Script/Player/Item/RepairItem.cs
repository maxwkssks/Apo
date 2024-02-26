using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairItem : BaseItem
{
    public override void OnGetItem(CharacterManager characterManager)
    {
        PlayerHPSystem system = characterManager.Player.GetComponent<PlayerHPSystem>();    
        if (system != null)
        {
            system.Health += 1;
        }
    }
}
