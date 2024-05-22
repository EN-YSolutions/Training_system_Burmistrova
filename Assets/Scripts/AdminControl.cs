using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Npgsql;
using System;

public class AdminControl : MonoBehaviour
{
    public GameObject qPanel;
    public GameObject tPanel;

    void Start()
    {
        tPanel.SetActive(false);
        qPanel.SetActive(true);
    }

    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            qPanel.SetActive(true);
            tPanel.SetActive(false);
        }
        if (val == 1)
        {
            qPanel.SetActive(false);
            tPanel.SetActive(true);
        }
    }
}
