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
    public GameObject ExpWindow;
    public Text exp;
    public Text question;
    public Text score;
    public InputField user_answer;
    private string answer;
    private string cAnswer;
    private List<string> qs;
    private List<string> exps;
    private List<string> ans;
    private string qID;

    private string currentCourseId;
    private string sceneName;

    public void GetId()
    {
        string selectQuery = "SELECT * FROM courses";
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if(sceneName == "LevelOne")
                    {
                        currentCourseId = reader[0].ToString();
                        break;
                    }
                    else if(sceneName == "LevelTwo")
                    {
                        currentCourseId = reader[0].ToString();
                    }
                }
            }
        }
        Debug.Log(currentCourseId);
    }

    public void GetQuestions()
    {
        qs = new List<string>();
        exps = new List<string>();
        string selectQuery = "SELECT * FROM questions WHERE course_id = '"+currentCourseId+"'";
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    qs.Add(reader.GetString(2));
                    exps.Add(reader.GetString(4));
                }
            }
        }
    }

    public void GetAnswers()
    {
        ans = new List<string>();
        string selectQuery = "SELECT answer FROM questions WHERE course_id = '"+currentCourseId+"'";
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
        exp.text = exps[ind];
        exps.RemoveAt(ind);
        cAnswer = ans[ind];
        ans.RemoveAt(ind); 
    }

    void Start()
    {
        ExpWindow.SetActive(false);
        var currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        GetId();
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
            if (qs.Count <= 0)
            {
                QWindow.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                Ask();
            }
        }
        else
        {
            ExpWindow.SetActive(true);
            CharControl.healthPoint -= 1;
        }
    }

    public void Continue()
    {
        ExpWindow.SetActive(false);

        string selectQuery = "SELECT * FROM questions WHERE question = '" + question.text + "'";
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    qID = reader[0].ToString();
                    break;
                }
            }
        }

        string newQuery = "INSERT INTO mistakes(game_date, user_id, question_id, user_answer) VALUES('" + GameDate.gamedate + "', '" + Enter.UserID + "', '" + qID + "', '" + answer + "')";
        using (NpgsqlCommand command = new NpgsqlCommand(newQuery, DatabaseConnector.connection))
        {
            command.ExecuteNonQuery();
        }

        if (CharControl.healthPoint <= 0)
        {
            QWindow.SetActive(false);
            SceneManager.LoadScene(sceneName);
        }

        if (qs.Count <= 0)
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
