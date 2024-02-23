using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Npgsql;
using System;
using UnityEngine.SceneManagement;

public class SettingsControl : MonoBehaviour
{
    public GameObject setWin;

    void Start()
    {
        setWin.SetActive(false);
    }

    public void SettingsButton()
    {
        setWin.SetActive(true);
    }

    public void DeleteHistory()
    {
        string newQuery = "DELETE FROM previous_games WHERE user_id = '" + Enter.UserID + "'";
        using (NpgsqlCommand command = new NpgsqlCommand(newQuery, DatabaseConnector.connection))
        {
            command.ExecuteNonQuery();
        }
    }

    public void LeaveAccount()
    {
        SceneManager.LoadScene("StartPage");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setWin.SetActive(false);
        }
    }
}
