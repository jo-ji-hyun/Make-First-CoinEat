using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public GameObject Coin;
    public GameObject Enemy;
    public static GameManager Instance; // �̱��� ����

    public Text totalScoreTxt;
    public Text Score;
    public Text TimeT;
    public GameObject EndPanel;
    public GameObject Key;
    public GameObject clife1;
    public GameObject clife2;
    public GameObject clife3;
    public GameObject popabove;

    public Text nowScore;
    public Text BestScore;
    public Text Lifepoint;
    public Text Popup_AboveScore;

    public RectTransform skillgauge;

    public Popup_Above scorePopupInstance;
    public Transform playerTargetForPopup;

    public float totalTime = 40.0f;
    public int totalScore = 0;
    int HighScore = 0;

    float Skill = 0;
    float MaxSkill = 3.0f;
    public bool isSkill = false;

    public int Life;

    Coroutine PopupRoutine;

    AudioSource audioSource;
    public AudioClip Power;
    public AudioClip Clear;

    public bool isgameover = false;      public bool isvictory = false;

    void Start()
    {
        InvokeRepeating("MakeCoin", 0, 0.8f);
        InvokeRepeating("MakeEnemy", 0, 6.1f);

        HighScore = PlayerPrefs.GetInt("HighScore", 0);

        BestScore.text = HighScore.ToString();

        Time.timeScale = 1.0f;
        GameManager.Instance.totalTime = 40f;
        Life = 3;
    }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 'R' Ű�� ������ �� (�� ����) MainScene ������ �۵���
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHighScore(); // �ְ� ���� �ʱ�ȭ �Լ� ȣ��
        }

        if (totalTime > 0f && Life >= 1)
        {
            totalTime -= Time.deltaTime;
        }
        else if(Life >= 1) // ���� �������� ��
        {
            if (isvictory) return;
            isvictory = true;
            Time.timeScale = 0.0f;
            totalTime = 0.0f;
            EndPanel.SetActive(true);
            popabove.SetActive(false);
            audioSource.PlayOneShot(Clear);
            nowScore.text = Score.text;
            Lifepoint.text = Life.ToString();

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.StopBGM(); 
            }
        }

        TimeT.text = totalTime.ToString("N2");

        if (totalTime > 0f && Life == 0) // ���� ���� ����
        {
            GameOver();
        }
    }

    public void ResetHighScore()
    {
        // 1. PlayerPrefs�� ����� "HighScore" ���� 0���� �����մϴ�.
        PlayerPrefs.SetInt("HighScore", 0);

        // 2. ����� HighScore ���� ��� �����մϴ�.
        PlayerPrefs.Save();

        // 3. ���� ��ũ��Ʈ�� HighScore ������ 0���� ������Ʈ�մϴ�.
        HighScore = 0;

        // 4. UI Text ������Ʈ���� ����� ������ �ݿ��մϴ�.
        BestScore.text = HighScore.ToString();

        Debug.Log("�ְ� ������ �ʱ�ȭ�Ǿ����ϴ�!");
    }

    public void GameOver()
    {
        if (isgameover) return;
        isgameover = true;
        Time.timeScale = 1.0f;

        RetryButton.Instance.Lose();


        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopBGM(); 
        }
    }

    void MakeCoin()
    {
        Instantiate(Coin);
    }

    void MakeEnemy()
    {
        Instantiate(Enemy);
    }

    public void AddScore(int score)
    {
        totalScore += score;

        if (totalScore <= 0)
        {
            totalScore = 0;
        }

        totalScoreTxt.text = totalScore.ToString();

        Popup_AboveScore.text = (score > 0 ? "+" : "-") + score.ToString();
        Popup_AboveScore.color = score > 0 ? Color.yellow : Color.red;
        Popup_AboveScore.gameObject.SetActive(true);

        StartCoroutine(HidePopup());

        // === �ٽ� ����: Popup_Above ��ũ��Ʈ�� ShowPopup �Լ� ȣ�� ===
        if (scorePopupInstance != null) // scorePopupInstance�� Inspector�� ����Ǿ����� Ȯ��
        {
            if (playerTargetForPopup != null) // playerTargetForPopup�� Inspector�� ����Ǿ����� Ȯ��
            {
                // Popup_Above ��ũ��Ʈ�� target�� ���� playerTargetForPopup���� ����
                scorePopupInstance.target = playerTargetForPopup;

                // Popup_Above ��ũ��Ʈ�� ShowPopup �Լ� ȣ��
                scorePopupInstance.ShowPopup(score); // ���� ������ Popup_Above�� ����

            }
       
        }
 

        if (PopupRoutine != null)
        {
            StopCoroutine(PopupRoutine);
            PopupRoutine = StartCoroutine(HidePopup());

        }

        if (totalScore > HighScore)
        {
            HighScore = totalScore;
            PlayerPrefs.SetInt("HighScore", HighScore);

            BestScore.text = HighScore.ToString();
        }

        // === ��ų ===

        if (isSkill == true) return;

        Skill += score / 5;

        if (Skill >= MaxSkill)
        {
            Skill = MaxSkill;
            isSkill = true;
            Key.SetActive(true);
            audioSource.PlayOneShot(Power);
        }
        else
        {
            Skill = Mathf.Clamp(Skill, 0f, MaxSkill);
        }

            skillgauge.localScale = new Vector3(Skill, 1.0f, 1.0f);

    }

    IEnumerator HidePopup()
    {
        yield return new WaitForSeconds(0.5f); 
        Popup_AboveScore.gameObject.SetActive(false);

    }

    // === ��ų ���� ===
    public void UseSkill()
    {
        if (!isSkill) return;

        Key.SetActive(false);
        Skill = 0;
        isSkill = false;
        skillgauge.localScale = new Vector3(Skill, 1.0f, 1.0f);
    }

    // === Life ===
    public void Charaterlife()
    {
        if ( Life == 2 )
        {
            clife1.SetActive(false);
        }
        else if ( Life == 1 )
        {
            clife2.SetActive(false);
        }
        else
        {
            clife3.SetActive(false);
        }
    }

}
