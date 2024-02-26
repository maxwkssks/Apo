using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CharacterManager : BaseManager
{
    [SerializeField]
    private BaseCharacter _player;
    public BaseCharacter Player => _player;

    private List<BaseCharacter> _enemys = new List<BaseCharacter>();
    public List<BaseCharacter> Enemys => _enemys;

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        _player.Init(this);
    }
}