using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Npgsql;
using System;

public class AddQAndT : MonoBehaviour
{
    public InputField question;
    public InputField task;
    public InputField qExplanation;
    public InputField tExplanation;
    public InputField qAnswer;
    public InputField tAnswer;

    public string courseId;

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            string selectQuery = "SELECT * FROM courses";
            using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courseId = reader[0].ToString();
                        break;
                    }
                }
            }
        }
        if (val == 1)
        {
            string selectQuery = "SELECT * FROM courses";
            using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courseId = reader[0].ToString();
                    }
                }
            }
        }
    }

    public void AddQuestion()
    {
        string newQuery = "INSERT INTO questions(course_id, question, answer, explanation) VALUES('" + courseId + "', '" + question.text + "', '" + qAnswer.text + "', '" + qExplanation.text + "')";
        using (NpgsqlCommand command = new NpgsqlCommand(newQuery, DatabaseConnector.connection))
        {
            command.ExecuteNonQuery();
        }
    }

    public void AddTask()
    {
        string newQuery = "INSERT INTO tasks(course_id, question, answer, explanation) VALUES('" + courseId + "', '" + task.text + "', '" + tAnswer.text + "', '" + tExplanation.text + "')";
        using (NpgsqlCommand command = new NpgsqlCommand(newQuery, DatabaseConnector.connection))
        {
            command.ExecuteNonQuery();
        }
    }
}
