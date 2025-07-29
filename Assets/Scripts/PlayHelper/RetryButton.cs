using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public static RetryButton Instance;

    private static string currentSceneNameBeforeRetry = "";

    // === 씬 로드 ===
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("MainScene")) 
        {
            currentSceneNameBeforeRetry = scene.name;
        }
    }

    // === 다시하기 ===
    public void Retry()
    {

        if (!string.IsNullOrEmpty(currentSceneNameBeforeRetry))
        {
            SceneManager.LoadScene(currentSceneNameBeforeRetry);
        }
        else
        {
            SceneManager.LoadScene("StartScene");
        }
    }

    // === 다음 스테이지 ===
    public void NextStage()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "MainScene")
        {
            SceneManager.LoadScene("MainScene 1");
        }
        else if (currentScene == "MainScene 1")
        {
            SceneManager.LoadScene("MainScene 2");
        }
        else if (currentScene == "MainScene 2")
        {
            Debug.Log("준비중");
           // SceneManager.LoadScene("BossScene");
        }
        else 
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    // === 이전 스테이지 ===
    public void BackStage()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "MainScene 1")
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (currentScene == "MainScene 2")
        {
            SceneManager.LoadScene("MainScene 1");
        }
        else 
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    // === 패배시 ===
    public void Lose()
    {
        SceneManager.LoadScene("DefeatScene");
    }

    // === 메인으로 ===
    public void Home()
    {
        SceneManager.LoadScene("StartScene");
    }
}
