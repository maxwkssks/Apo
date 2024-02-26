using System.Collections;
using UnityEngine;

public class BossA : MonoBehaviour
{
    public GameObject Projectile;
    public float ProjectileMoveSpeed = 5.0f;
    public float FireRate = 2.0f;
    public float MoveSpeed = 2.0f;
    public float MoveDistance = 5.0f;

    private int _currentPatternIndex = 0;
    private bool _movingRight = true;
    private bool _bCanMove = false;
    private Vector3 _originPosition;

    private void Start()
    {
        _originPosition = transform.position;
        StartCoroutine(MoveDownAndStartPattern());
    }

    private IEnumerator MoveDownAndStartPattern()
    {
        while (transform.position.y > _originPosition.y - 3f)
        {
            transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
            yield return null;
        }

        _bCanMove = true;
        InvokeRepeating("NextPattern", 2.0f, FireRate);
    }

    private void Update()
    {
        if (_bCanMove)
            MoveSideways();
    }

    private void NextPattern()
    {
        // 패턴 인덱스를 증가시키고, 마지막 패턴일 경우 다시 처음 패턴으로 돌아감
        _currentPatternIndex = (_currentPatternIndex + 1) % 4;

        // 현재 패턴 실행
        switch (_currentPatternIndex)
        {
            case 0:
                Pattern1();
                break;
            case 1:
                Pattern2();
                break;
            case 2:
                StartCoroutine(Pattern3());
                break;
            case 3:
                Pattern4();
                break;
        }
    }

    private void MoveSideways()
    {
        if (_movingRight)
        {
            transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
            if (transform.position.x > MoveDistance)
            {
                _movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
            if (transform.position.x < -MoveDistance)
            {
                _movingRight = true;
            }
        }
    }

    private void StartMovingSideways()
    {
        StartCoroutine(MovingSidewaysRoutine());
    }

    private IEnumerator MovingSidewaysRoutine()
    {
        while (true)
        {
            MoveSideways();
            yield return null;
        }
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

    private void Pattern1()
    {
        // 패턴 1: 원형으로 총알 발사
        int numBullets1 = 30;
        float angleStep1 = 360.0f / numBullets1;

        for (int i = 0; i < numBullets1; i++)
        {
            float angle1 = i * angleStep1;
            float radian1 = angle1 * Mathf.Deg2Rad;
            Vector3 direction1 = new Vector3(Mathf.Cos(radian1), Mathf.Sin(radian1), 0);

            ShootProjectile(transform.position, direction1);
        }
    }

    private void Pattern2()
    {
        // 패턴 2: 방사형으로 총알 발사
        int numBullets2 = 12;
        float angleStep2 = 360.0f / numBullets2;

        for (int i = 0; i < numBullets2; i++)
        {
            float angle2 = i * angleStep2;
            float radian2 = angle2 * Mathf.Deg2Rad;
            Vector3 direction2 = new Vector3(Mathf.Cos(radian2), Mathf.Sin(radian2), 0);

            ShootProjectile(transform.position, direction2);
        }


    }

    private IEnumerator Pattern3()
    {
        // 패턴 3: 몇 초 간격으로 플레이어에게 하나씩 발사
        int numBullets = 5;
        float interval = 1.0f;

        for (int i = 0; i < numBullets; i++)
        {
            Vector3 playerDirection = (PlayerPosition() - transform.position).normalized;
            ShootProjectile(transform.position, playerDirection);
            yield return new WaitForSeconds(interval);
        }
    }

    private void Pattern4()
    {
        // 패턴 3: 나선형으로 총알 발사
        int numBullets3 = 10;
        float angleStep3 = 360.0f / numBullets3;
        float radius = 2.0f;

        for (int i = 0; i < numBullets3; i++)
        {
            float angle3 = i * angleStep3;
            float radian3 = angle3 * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(radian3);
            float y = radius * Mathf.Sin(radian3);

            Vector3 direction3 = new Vector3(x, y, 0).normalized;

            ShootProjectile(transform.position, direction3);
        }
    }

    private Vector3 PlayerPosition()
    {
        return GameManager.Instance.GetPlayerCharacter().transform.position;
    }

    private void OnDestroy()
    {
        GameManager.Instance.StageClear();
    }
}