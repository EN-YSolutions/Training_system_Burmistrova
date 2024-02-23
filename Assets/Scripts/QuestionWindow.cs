using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionWindow : MonoBehaviour
{
    public GameObject QWindow;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            QWindow.SetActive(true);
            Time.timeScale = 0f;
            Destroy(gameObject);
        }
    }

    void Start()
    {
        QWindow.SetActive(false);
    }
}
