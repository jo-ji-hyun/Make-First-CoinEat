using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Popup_Above : MonoBehaviour
{

    public Transform target; 
    public Vector3 offset = new Vector3(0, 1f, 0); //inspector창에서 수정!

    private float timer = 0f;
    private float showTime = 0.5f;

    private TextMeshProUGUI popupText;


    void Awake()
    {
      
        popupText = GetComponent<TextMeshProUGUI>();
        if (popupText == null)
        {
            enabled = false; 
            return;
        }

       
        gameObject.SetActive(false);
    }

    void Start()
    {
        popupText = GetComponent<TextMeshProUGUI>();

        gameObject.SetActive(false);

    }


    void Update()
    {
        // 팝업이 비활성화 상태이면 업데이트 로직을 실행하지 않음
        if (!gameObject.activeSelf) return;

        // target이 할당되었는지 확인
        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }

        // 메인 카메라가 존재하는지 확인
        if (Camera.main == null)
        {
            gameObject.SetActive(false);
            return;
        }

        // 대상의 월드 좌표 + 오프셋을 화면 좌표로 변환하여 팝업 위치 설정
        Vector3 worldPos = target.position + offset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        transform.position = screenPos;

        // Debug.Log($"Popup_Above Update: Target World Pos: {worldPos}, Screen Pos: {screenPos}", this); // 너무 많은 로그가 뜨므로 필요할 때만 활성화

        // 타이머 증가 및 팝업 비활성화 처리
        timer += Time.deltaTime;
        if (timer >= showTime)
        {
            gameObject.SetActive(false);
        }
    }

    // 외부에서 호출하여 팝업을 표시하는 함수
    public void ShowPopup(int score)
    {
        if (popupText == null)
        {
            return;
        }

        // 점수에 따라 텍스트 내용과 색상 설정
        popupText.text = (score >= 0 ? "+" : "") + score.ToString();
        popupText.color = (score >= 0) ? Color.yellow : Color.red;

        // 타이머 초기화 및 팝업 활성화
        timer = 0f;
        gameObject.SetActive(true);
    }

}
