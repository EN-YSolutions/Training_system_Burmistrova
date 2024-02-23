using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDate : MonoBehaviour
{
    public static string gamedate;
    void Start()
    {
        gamedate = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy HH:mm");
    }
}
