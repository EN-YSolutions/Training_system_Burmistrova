using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Npgsql;
using System;
using UnityEngine.SceneManagement;

public class Questions : MonoBehaviour
{
    public GameObject QWindow;
    public Text question;
    public Text score;
    public InputField user_answer;
    public string answer;
    public string cAnswer;
    public List<string> qs;
    public List<string> ans;

    public void GetQuestions()
    {
        qs = new List<string>();
        string selectQuery = "SELECT question FROM questions";
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    qs.Add(reader.GetString(0));
                }
            }
        }
    }

    public void GetAnswers()
    {
        string selectQuery = "SELECT answer FROM questions";
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ans.Add(reader.GetString(0));
                }
            }
        }
    }

    public void Ask()
    {
        user_answer.text = "";
        if (qs.Count <= 0)
        {
            QWindow.SetActive(false);
        }
        int ind = UnityEngine.Random.Range(0, qs.Count);
        question.text = qs[ind];
        qs.RemoveAt(ind);
        cAnswer = ans[ind];
        ans.RemoveAt(ind); 
    }

    void Start()
    {
        GetQuestions();
        GetAnswers();
        Ask();
    }

    void Update()
    {
        score.text = CharControl.points.ToString();
    }

    public void CheckAnswer()
    {
        answer = user_answer.text;
        if(answer == cAnswer)
        {
            CharControl.points += 10;
        }
        else
        {
            CharControl.healthPoint -= 1;
            if (CharControl.healthPoint <= 0)
            {
                QWindow.SetActive(false);
                SceneManager.LoadScene("LevelOne");
            }
        }

        if(qs.Count <= 0)
        {
            QWindow.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Ask();
        }
    }
}
