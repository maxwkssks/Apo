using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public CharacterManager CharacterManager;
    public MapManager MapManager;
    public EnemySpawnManager EnemySpawnManager;
    public SoundManager SoundManager;
    public ItemManager ItemManager;

    public Canvas StageClearResultCanvas;
    public TMP_Text CurrentScoreText;
    public TMP_Text ElapsedTimeText;

    [HideInInspector] public bool bStageCleared = false;

    private void Awake()  // 객체 생성시 최초 실행 (그래서 싱글톤을 여기서 생성)
    {
        if (Instance == null)  // 단 하나만 존재하게끔
        {
            Instance = this;  // 객체 생성시 instance에 자기 자신을 넣어줌
        }
        else
            Destroy(this.gameObject);
    }

    public PlayerCharacter GetPlayerCharacter() { return CharacterManager.Player.GetComponent<PlayerCharacter>(); }

    public void InitInstance()
    {
        GameInstance.instance.GameStartTime = 0f;
        GameInstance.instance.Score = 0;
        GameInstance.instance.CurrentStageLevel = 1;
        GameInstance.instance.CurrentPlayerWeaponLevel = 0;
        GameInstance.instance.CurrentPlayerHP = 3;
        GameInstance.instance.CurrentPlayerFuel = 100f;
        GameInstance.instance = null;
    }

    void Start()
    {
        if(CharacterManager == null) { return; }
        CharacterManager.Init(this);
        MapManager.Init(this);
        EnemySpawnManager.Init(this);
        SoundManager.PlayBGM("BGM");
    }

    public void EnemyDies()
    {
        AddScore(10);
    }

    public void StageClear()
    {
        GameInstance.instance.Score += 500;

        float gameStartTime = GameInstance.instance.GameStartTime;
        int score = GameInstance.instance.Score;

        // 걸린 시간
        int elapsedTime = Mathf.FloorToInt(Time.time - gameStartTime);

        // 스테이지 클리어 결과창 : 점수, 시간
        StageClearResultCanvas.gameObject.SetActive(true);
        CurrentScoreText.text = "CurrentScore : " + score;
        ElapsedTimeText.text = "ElapsedTime : " + elapsedTime;

        bStageCleared = true;

        // 5초 뒤에 다음 스테이지
        StartCoroutine(NextStageAfterDelay(5f));
    }

    IEnumerator NextStageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        switch(GameInstance.instance.CurrentStageLevel)
        {
            case 1:
                SceneManager.LoadScene("Stage2");
                GameInstance.instance.CurrentStageLevel = 2;
                break;

            case 2:
                SceneManager.LoadScene("Result");
                break;
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void AddScore(int score)
    {
        GameInstance.instance.Score += score;
    }

    private void Update()
    {
        // 맵 내에 모든 적 유닛 제거.
        if (Input.GetKeyUp(KeyCode.F1))
        {
            // 모든 Enemy 찾기
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in enemies)
            {
                Enemy enemy = obj?.GetComponent<Enemy>();
                enemy?.Dead();
            }
        }

        // 공격 업그레이드를 최고 단계로 상승
        if (Input.GetKeyUp(KeyCode.F2))
        {
            GetPlayerCharacter().CurrentWeaponLevel = 3;
            GameInstance.instance.CurrentPlayerWeaponLevel = GetPlayerCharacter().CurrentWeaponLevel;
        }

        // 스킬의 쿨타임 및 횟수를 초기화 시킨다
        if (Input.GetKeyUp(KeyCode.F3))
        {
            GetPlayerCharacter().InitSkillCooldown();
        }

        // 내구도 초기화
        if (Input.GetKeyUp(KeyCode.F4))
        {
            GetPlayerCharacter().GetComponent<PlayerHPSystem>().InitHealth();
        }

        // 연료 초기화
        if (Input.GetKeyUp(KeyCode.F5))
        {
            GetPlayerCharacter().GetComponent<PlayerFuelSystem>().InitFuel();
        }

        // 연료 초기화
        if (Input.GetKeyUp(KeyCode.F6))
        {
            StageClear();
        }

    }
}