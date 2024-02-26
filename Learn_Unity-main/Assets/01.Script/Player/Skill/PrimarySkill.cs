using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrimarySkill : BaseSkill
{
    public float ProjectileMoveSpeed;
    public GameObject Projectile;

    private Weapon[] weapons; 

    void Start()
    {
        CooldownTime = 0.2f;

        weapons = new Weapon[5];

        weapons[0] = new Level1Weapon();
        weapons[1] = new Level2Weapon();
        weapons[2] = new Level3Weapon();
        weapons[3] = new Level4Weapon();
        weapons[4] = new Level5Weapon();
    }

    public override void Activate()
    {
        base.Activate();
        weapons[_characterManager.Player.GetComponent<PlayerCharacter>().CurrentWeaponLevel].Activate(this, _characterManager);
        GameManager.Instance.SoundManager.PlaySFX("PrimarySkill");
    }

    public void ShootProjectile(Vector3 position, Vector3 direction)
    {
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity);
        Projectile projectile = instance.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.MoveSpeed = ProjectileMoveSpeed;
            projectile.SetDirection(direction.normalized);
        }
    }
}

public interface Weapon
{
    void Activate(PrimarySkill primarySkill, CharacterManager characterManager);
}

public class Level1Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill , CharacterManager characterManager)
    {
        Vector3 position = characterManager.Player.transform.position;
        primarySkill.ShootProjectile(position, Vector3.up);
    }
}

public class Level2Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
    {
        Vector3 position = characterManager.Player.transform.position;
        position.x -= 0.1f;

        for (int i = 0; i < 2; i++)
        {
            primarySkill.ShootProjectile(position, Vector3.up);
            position.x += 0.2f;
        }
    }
}

public class Level3Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
    {
        Vector3 position = characterManager.Player.transform.position;

        primarySkill.ShootProjectile(position, Vector3.up);
        primarySkill.ShootProjectile(position, new Vector3(0.3f, 1, 0));
        primarySkill.ShootProjectile(position, new Vector3(-0.3f, 1, 0));
    }
}

public class Level4Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
    {
        Vector3 position = characterManager.Player.transform.position;
        position.x -= 0.1f;

        for (int i = 0; i < 2; i++)
        {
            primarySkill.ShootProjectile(position, Vector3.up);
            position.x += 0.2f;
        }

        Vector3 position2 = characterManager.Player.transform.position;
        primarySkill.ShootProjectile(position2, new Vector3(0.3f, 1, 0));
        primarySkill.ShootProjectile(position2, new Vector3(-0.3f, 1, 0));
    }
}

public class Level5Weapon : Weapon
{
    public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
    {
        Vector3 position = characterManager.Player.transform.position;

        for (int i = 0; i < 180; i += 10) // 360도를 10도씩 나눠서 총알 발사
        {
            /*
               180 degree = π radian
               1 degree = π / 180 radian
               x degree = x * π / 180 radian
 
               π radian = 180 degree
               1 radian = 180 / π degree
               x radian = x * 180 / π degree
             */

            // i = degree
            // Deg2Rad = 180 / π degree
            // Mathf 의 cos, sin 은 rad 를 넣어줘야 함.

            float angle = i * Mathf.Deg2Rad;
            Debug.Log(angle);
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

            primarySkill.ShootProjectile(position, direction);
        }
    }
}