using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Npgsql;
using System;

public class Exit : MonoBehaviour
{
    public GameObject result;
    public Text score;
    private string currentCourseId;
    public static string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            result.SetActive(true);
            score.text = CharControl.points.ToString();
            string newQuery = "INSERT INTO previous_games(user_id, course_id, game_date, points_for_game) VALUES('"+Enter.UserID+"', '"+currentCourseId+"', '"+GameDate.gamedate+"', '"+CharControl.points+"')";
            using (NpgsqlCommand command = new NpgsqlCommand(newQuery, DatabaseConnector.connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

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
    }

    void Start()
    {
        result.SetActive(false);
        var currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        GetId();
    }
}