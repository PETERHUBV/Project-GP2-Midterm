using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Color EnemyColor;
    public float MoveSpeed = 1f; 

    public Color GetColor()
    {
        return EnemyColor;
    }

    private void Update()
    {
       
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
        }
    }
}