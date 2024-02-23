using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Npgsql;
using System;

public class Enter : MonoBehaviour
{
    public Text errorlog;
    public InputField user_username;
    public InputField user_password;
    private string username;
    public static string password;

    public static string UserID;

    public void GetData()
    {
        username = user_username.text;
        password = user_password.text;
    }

    private static string connectionString = "Server=localhost;Port=5432;Database=gameSQL;User Id=postgres;Password=admin;";
    public NpgsqlConnection connection;

    public void Autorize()
    {
        GetData();
        connection = new NpgsqlConnection(connectionString);
        try
        {
            connection.Open();
            string name, pw;
            string selectQuery = "SELECT * FROM users";
            using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        name = reader.GetString(1);
                        pw = reader.GetString(2);
                        if(name == username && pw == password)
                        {
                            errorlog.text = "";
                            UserID = reader[0].ToString();
                            Debug.Log(UserID);
                            SceneManager.LoadScene("Menu");
                            break;
                        }
                        else
                        {
                            errorlog.text = "Ошибка! Неправильное имя или пароль";
                        }
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
