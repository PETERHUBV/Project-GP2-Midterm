using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Color EnemyColor;

    // Start is called before the first frame update
    void Start()
    {
        EnemyColor = Random.value > 0.5 ? Color.red : Color.yellow;
        GetComponent<Renderer>().material.color = EnemyColor;
    }

    // Update is called once per frame
   public Color GetColor()
    {
        return EnemyColor;
    }
}
