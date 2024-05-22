using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Npgsql;
using System;
using UnityEngine.SceneManagement;

public class Tasks : MonoBehaviour
{
    public GameObject TWindow;
    public GameObject ExpWindow;
    public Text exp;
    public Text question;
    public Text score;
    public InputField user_answer;
    private string answer;
    private string cAnswer;
    private List<string> ts;
    private List<string> exps;
    private List<string> ans;
    private string tID;

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
                    if (sceneName == "LevelOne")
                    {
                        currentCourseId = reader[0].ToString();
                        break;
                    }
                    else if (sceneName == "LevelTwo")
                    {
                        currentCourseId = reader[0].ToString();
                    }
                }
            }
        }
        Debug.Log(currentCourseId);
    }

    public void GetTasks()
    {
        ts = new List<string>();
        exps = new List<string>();
        string selectQuery = "SELECT * FROM tasks WHERE course_id = '" + currentCourseId + "'";
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ts.Add(reader.GetString(2));
                    exps.Add(reader.GetString(4));
                }
            }
        }
    }

    public void GetAnswers()
    {
        ans = new List<string>();
        string selectQuery = "SELECT answer FROM tasks WHERE course_id = '" + currentCourseId + "'";
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
        if (ts.Count <= 0)
        {
            TWindow.SetActive(false);
        }
        int ind = UnityEngine.Random.Range(0, ts.Count);
        question.text = ts[ind];
        ts.RemoveAt(ind);
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
        GetTasks();
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
        if (answer == cAnswer)
        {
            CharControl.points += 20;
            if (ts.Count <= 0)
            {
                TWindow.SetActive(false);
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

        string selectQuery = "SELECT * FROM tasks WHERE task = '" + question.text + "'";
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tID = reader[0].ToString();
                    break;
                }
            }
        }

        string newQuery = "INSERT INTO mistakes(game_date, user_id, question_id, user_answer) VALUES('" + GameDate.gamedate + "', '" + Enter.UserID + "', '" + tID + "', '" + answer + "')";
        using (NpgsqlCommand command = new NpgsqlCommand(newQuery, DatabaseConnector.connection))
        {
            command.ExecuteNonQuery();
        }

        if (CharControl.healthPoint <= 0)
        {
            TWindow.SetActive(false);
            SceneManager.LoadScene(sceneName);
        }

        if (ts.Count <= 0)
        {
            TWindow.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Ask();
        }
    }
}
