using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    int score;

    new SpriteRenderer renderer;

    AudioSource audioSource;
    public AudioClip Clip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        renderer = GetComponent<SpriteRenderer>();

        float x = Random.Range(-2.7f, 2.7f);
        float y = Random.Range(4.0f, 5.0f);
        transform.position = new Vector3(x, y, 0);

        int type = Random.Range(1, 4);

        if (type == 1)
        {
            score = 1;
        }
        else if (type == 2)
        {
            score = 3;
        }
        else
        {
            score = 5;
        }
    }

    void Update()
    {
        
    }

    // === �浹�� ó�� ���� ===
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(score);

                if (Clip != null)
                {
                    AudioSource.PlayClipAtPoint(Clip, transform.position);
                }
                else
                {
                    Debug.LogWarning("Coins ��ũ��Ʈ�� Clip ������ ����� Ŭ���� �Ҵ���� �ʾҽ��ϴ�!");
                }

            }
            Destroy(gameObject);
        }


    }
}
