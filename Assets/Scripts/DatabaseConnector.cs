using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npgsql;
using System;

public class DatabaseConnector : MonoBehaviour
{
    private static string connectionString = "Server=localhost;Port=5432;Database=gameSQL;User Id=postgres;Password=admin;";
    public static NpgsqlConnection connection;

    void Start()
    {
        connection = new NpgsqlConnection(connectionString);
        connection.Open();
    }
}
