using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && CharControl.healthPoint < 3)
        {
            CharControl.healthPoint += 1;
            Destroy(gameObject);
        }
    }
}
