using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerCharacter : BaseCharacter
{
    #region Movement
    private Vector2 _moveInput;
    public float MoveSpeed;
    #endregion

    #region Skills
    [HideInInspector] public Dictionary<EnumTypes.PlayerSkill, BaseSkill> Skills;
    [SerializeField] private GameObject[] _skillPrefabs;
    #endregion

    #region Invincibility
    private bool invincibility;
    private Coroutine invincibilityCoroutine;
    private const double InvincibilityDurationInSeconds = 3; // 무적 지속 시간 (초)
    public bool Invincibility
    {
        get { return invincibility; }
        set { invincibility = value; }
    }
    #endregion

    #region Weapon
    public int CurrentWeaponLevel = 0;
    public int MaxWeaponLevel = 3;
    #endregion

    public override void Init(CharacterManager characterManager)
    {
        base.Init(characterManager);
        InitializeSkills();
    }

    public void DeadProcess()
    {
        GameManager.Instance.InitInstance();
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        UpdateMovement();
        UpdateSkillInput();
    }

    private void UpdateMovement()
    {
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(new Vector3(_moveInput.x, _moveInput.y, 0f) * (MoveSpeed * Time.deltaTime));

        // 카메라의 좌측 하단은(0, 0, 0.0)이며, 우측 상단은(1.0 , 1.0)이다.
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void UpdateSkillInput()
    {
        if (Input.GetKey(KeyCode.Z)) ActivateSkill(EnumTypes.PlayerSkill.Primary);
        if (Input.GetKeyUp(KeyCode.X)) ActivateSkill(EnumTypes.PlayerSkill.Repair);
        if (Input.GetKeyUp(KeyCode.C)) ActivateSkill(EnumTypes.PlayerSkill.Bomb);
        if (Input.GetKeyUp(KeyCode.V)) ActivateSkill(EnumTypes.PlayerSkill.So);
    }

    private void InitializeSkills()
    {
        Skills = new Dictionary<EnumTypes.PlayerSkill, BaseSkill>();

        for (int i = 0; i < _skillPrefabs.Length; i++)
        {
            AddSkill((EnumTypes.PlayerSkill)i, _skillPrefabs[i]);
        }

        CurrentWeaponLevel = GameInstance.instance.CurrentPlayerWeaponLevel;
    }

    private void AddSkill(EnumTypes.PlayerSkill skillType, GameObject prefab)
    {
        GameObject skillObject = Instantiate(prefab, transform.position, Quaternion.identity);
        skillObject.transform.parent = this.transform;

        if (skillObject != null)
        {
            BaseSkill skillComponent = skillObject.GetComponent<BaseSkill>();
            skillComponent.Init(CharacterManager);
            Skills.Add(skillType, skillComponent);
        }
    }
    private void ActivateSkill(EnumTypes.PlayerSkill skillType)
    {
        if (Skills.ContainsKey(skillType))
        {
            if (Skills[skillType].IsAvailable())
            {
                Skills[skillType].Activate();
            }
            else
            {
                if(skillType != EnumTypes.PlayerSkill.Primary)
                    GetComponent<PlayerUI>().NoticeSkillCooldown(skillType);
            }
        }
    }

    public void InitSkillCooldown()
    {
        foreach (var skill in Skills.Values)
        {
            skill.InitCoolDown();
        }
    }

    public void SetInvincibility(bool invin)
    {
        if (invin)
        {
            if (invincibilityCoroutine != null)
            {
                StopCoroutine(invincibilityCoroutine);
            }

            invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        Invincibility = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // 무적 지속 시간 (초)
        float invincibilityDuration = 3f;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        // 무적이 해제될 때까지 대기
        yield return new WaitForSeconds(invincibilityDuration);

        // 타이머가 만료되면 무적을 비활성화
        Invincibility = false;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
            BaseItem item = collision.gameObject.GetComponent<BaseItem>();
            if (item != null)
            {
                item.OnGetItem(CharacterManager);
                Destroy(collision.gameObject);
            }
        }
    }
}