using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Npgsql;
using System;

public class GetGameHistory : MonoBehaviour
{
    public Text result;

    private string line = "";
    private int i = 1;

    void Start()
    {
        string selectQuery = "SELECT * FROM previous_games WHERE user_id = '" + Enter.UserID + "' AND course_id = 'ff28c433-601a-4b6e-ac8b-ea505bec4e7d'";
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    line = i + ". " + reader.GetString(3) + ", пройдено со счетом " + reader[4] + "\n";
                    result.text += line;
                    i++;
                }
            }
        }
    }

    public void HandleInputData(int val)
    {
        if(val == 0)
        {
            result.text = "";
            i = 1;
            string selectQuery = "SELECT * FROM previous_games WHERE user_id = '"+ Enter.UserID + "' AND course_id = 'ff28c433-601a-4b6e-ac8b-ea505bec4e7d'";
            using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        line = i + ". " + reader.GetString(3) + ", пройдено со счетом " + reader[4] + "\n";
                        result.text += line;
                        i++;
                    }
                }
            }
        }
        if(val == 1) 
        {
            result.text = "";
            i = 1;
            string selectQuery = "SELECT * FROM previous_games WHERE user_id = '"+ Enter.UserID + "' AND course_id = 'c3214c5b-51b8-44ce-8137-1c056a3aa173'";
            using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        line = i + ". " + reader.GetString(3) + ", пройдено со счетом " + reader[4] + "\n";
                        result.text += line;
                        i++;
                    }
                }
            }
        }
    }
}
