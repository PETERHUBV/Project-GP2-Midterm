using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Color BulletColor;


    public void SetColor(Color color)
    {
        BulletColor = color;
        GetComponent<Renderer>().material.color = BulletColor;
    }

    public void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Color enemyColor = enemy.GetColor();


            if (enemyColor == Color.red || enemyColor == Color.yellow)
            {
                HandleCollision(enemyColor, enemy);
            }
        }
    }
    void HandleCollision(Color enemyColor, Enemy enemy)
    {
        if (enemyColor == BulletColor)
        {
            Destroy(enemy.gameObject);
        }

            Destroy(gameObject);
        }
    }
