using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Popup_Above : MonoBehaviour
{

    public Transform target; 
    public Vector3 offset = new Vector3(0, 1f, 0); //inspectorâ���� ����!

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
        // �˾��� ��Ȱ��ȭ �����̸� ������Ʈ ������ �������� ����
        if (!gameObject.activeSelf) return;

        // target�� �Ҵ�Ǿ����� Ȯ��
        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }

        // ���� ī�޶� �����ϴ��� Ȯ��
        if (Camera.main == null)
        {
            gameObject.SetActive(false);
            return;
        }

        // ����� ���� ��ǥ + �������� ȭ�� ��ǥ�� ��ȯ�Ͽ� �˾� ��ġ ����
        Vector3 worldPos = target.position + offset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        transform.position = screenPos;

        // Debug.Log($"Popup_Above Update: Target World Pos: {worldPos}, Screen Pos: {screenPos}", this); // �ʹ� ���� �αװ� �߹Ƿ� �ʿ��� ���� Ȱ��ȭ

        // Ÿ�̸� ���� �� �˾� ��Ȱ��ȭ ó��
        timer += Time.deltaTime;
        if (timer >= showTime)
        {
            gameObject.SetActive(false);
        }
    }

    // �ܺο��� ȣ���Ͽ� �˾��� ǥ���ϴ� �Լ�
    public void ShowPopup(int score)
    {
        if (popupText == null)
        {
            return;
        }

        // ������ ���� �ؽ�Ʈ ����� ���� ����
        popupText.text = (score >= 0 ? "+" : "") + score.ToString();
        popupText.color = (score >= 0) ? Color.yellow : Color.red;

        // Ÿ�̸� �ʱ�ȭ �� �˾� Ȱ��ȭ
        timer = 0f;
        gameObject.SetActive(true);
    }

}
