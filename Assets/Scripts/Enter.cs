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

    public void Autorize()
    {
        GetData();
        string name, pw;
        string selectQuery = "SELECT * FROM users";
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    name = reader.GetString(1);
                    pw = reader.GetString(2);
                    if (name == username && pw == password)
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
}
