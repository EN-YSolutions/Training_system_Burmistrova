using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Npgsql;
using System;

public class Courses : MonoBehaviour
{
    public Text title1;
    public Text desc1;
    public Text title2;
    public Text desc2;

    private static string connectionString = "Server=localhost;Port=5432;Database=gameSQL;User Id=postgres;Password=admin;";
    public NpgsqlConnection connection;

    void Start()
    {
        connection = new NpgsqlConnection(connectionString);
        try
        {
            connection.Open();
            string selectQuery = "SELECT * FROM courses";
            using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        title1.text = reader.GetString(1);
                        desc1.text = reader.GetString(2);
                        break;
                    }
                }
            }
            using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        title2.text = reader.GetString(1);
                        desc2.text = reader.GetString(2);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }
}
