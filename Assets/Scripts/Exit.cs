using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public GameObject result;
    public Text score;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            result.SetActive(true);
            score.text = CharControl.points.ToString();
        }
    }

    void Start()
    {
        result.SetActive(false);
    }
}
