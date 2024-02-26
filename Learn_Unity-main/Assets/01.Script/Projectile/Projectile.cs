using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float MoveSpeed = 2f;

    private Vector3 _direction;

    public GameObject ExplodeFX;

    [SerializeField]
    private float _lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    void Update()
    {
        transform.Translate(_direction * MoveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnDestroy()
    {
        //Instantiate(ExplodeFX, transform.position, Quaternion.identity);
    }
}