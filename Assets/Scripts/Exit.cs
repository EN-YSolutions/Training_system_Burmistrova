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
    private string id = Enter.UserID;

    private static string connectionString = "Server=localhost;Port=5432;Database=gameSQL;User Id=postgres;Password=admin;";
    public NpgsqlConnection connection;

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.name == "Player")
        {
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            result.SetActive(true);
            score.text = CharControl.points.ToString();
            string selectQuery = "INSERT INTO previous_games(user_id, course_id, game_date, points_for_game) VALUES('"+id+"', 'ff28c433-601a-4b6e-ac8b-ea505bec4e7d', '"+GameDate.gamedate+"', '"+CharControl.points+"')";
            using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    void Start()
    {
        result.SetActive(false);
    }
}
