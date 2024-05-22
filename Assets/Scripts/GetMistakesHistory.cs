using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Npgsql;
using System;
using System.Threading.Tasks;

public class GetMistakesHistory : MonoBehaviour
{
    public Text question;
    public Text uAns;
    public Text cAns;

    private string date;
    private List<string> qid;
    private List<string> tid;

    void Start()
    {
        qid = new List<string>();
        tid = new List<string>();
        question.text = "";
        uAns.text = "";
        cAns.text = "";
        string selectQuery = "SELECT * FROM mistakes WHERE user_id = '" + Enter.UserID + "'";
        using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
        {
            using (NpgsqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    date = reader[1].ToString();
                    uAns.text += reader[4].ToString() + "\n";
                    qid.Add(reader[3].ToString());
                }
            }
        }

        while (qid.Count > 0)
        {
            selectQuery = "SELECT * FROM questions WHERE id = '" + qid[0] + "'";
            using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        question.text += date + " " + reader[2].ToString() + "\n";
                        cAns.text += reader[3].ToString() + "\n";
                    }
                    else
                    {
                        tid.Add(qid[0]);
                    }
                    qid.RemoveAt(0);
                }
            }
        }

        while (tid.Count > 0)
        {
            selectQuery = "SELECT * FROM tasks WHERE id = '" + tid[0] + "'";
            using (NpgsqlCommand command = new NpgsqlCommand(selectQuery, DatabaseConnector.connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        question.text += date + " " + reader[2].ToString() + "\n";
                        cAns.text += reader[3].ToString() + "\n";
                        tid.RemoveAt(0);
                    }
                }
            }
        }
    }
}