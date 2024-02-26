using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuelItem : BaseItem
{
    public override void OnGetItem(CharacterManager characterManager)
    {
        PlayerFuelSystem system = characterManager.Player.GetComponent<PlayerFuelSystem>();
        if (system != null)
        {
            system.Fuel = system.MaxFuel;
            GameInstance.instance.CurrentPlayerFuel = system.Fuel;
        }
    }
}
