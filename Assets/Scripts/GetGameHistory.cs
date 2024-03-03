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
        string selectQuery = "SELECT * FROM previous_games WHERE user_id = '" + Enter.UserID + "' AND course_id = '302cb1a0-ac5a-4dfc-8f29-376351450789'";
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
            string selectQuery = "SELECT * FROM previous_games WHERE user_id = '"+ Enter.UserID + "' AND course_id = '302cb1a0-ac5a-4dfc-8f29-376351450789'";
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
            string selectQuery = "SELECT * FROM previous_games WHERE user_id = '"+ Enter.UserID + "' AND course_id = '13976654-8fb7-4c9b-9a1d-e585c5ef7e21'";
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
