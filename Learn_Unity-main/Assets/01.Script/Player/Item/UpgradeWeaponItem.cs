using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeaponItem : BaseItem
{
    public override void OnGetItem(CharacterManager characterManager)
    {
        if (characterManager != null && characterManager.Player)
        {
            PlayerCharacter playerCharacter = characterManager.Player.GetComponent<PlayerCharacter>();

            int currentLevel = playerCharacter.CurrentWeaponLevel;
            int maxLevel = playerCharacter.MaxWeaponLevel;

            if(currentLevel >= maxLevel)
            {
                GameManager.Instance.AddScore(30);
                return;
            }

            playerCharacter.CurrentWeaponLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
            GameInstance.instance.CurrentPlayerWeaponLevel = playerCharacter.CurrentWeaponLevel;
        }
    }
}