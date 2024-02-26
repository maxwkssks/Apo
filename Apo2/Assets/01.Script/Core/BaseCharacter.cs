using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    private CharacterManager _characterManager;
    public CharacterManager CharacterManager => _characterManager;
    /*
      public CharacterManager CharacterManager
      {
        get { return _characterManager; }
      }
     */

    public virtual void Init(CharacterManager characterManager)
    {
        _characterManager = characterManager;
    }
}