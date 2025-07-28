using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    int score;

    new SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        float x = Random.Range(-2.7f, 2.7f);
        float y = Random.Range(4.0f, 5.0f);
        transform.position = new Vector3(x, y, 0);

         score = -5;

    }

    void Update()
    {
        
    }

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
                GameManager.Instance.Life -= 1;
                GameManager.Instance.Charaterlife();
            }
                Destroy(gameObject);
        }
    }
}
