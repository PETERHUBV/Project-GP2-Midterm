using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Color BulletColor;
    public float speed = 3f;


    private void Start()
    {
        Destroy(gameObject, 16f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetColor(Color color)
    {
        BulletColor = color;
        GetComponent<Renderer>().material.color = BulletColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (enemy.GetColor() == BulletColor)
                {
                    Debug.Log("Enemy destroyed!");
                    Destroy(enemy.gameObject);
                    Destroy(gameObject);
                }
                else
                {
                   
                    Destroy(gameObject);
                }
            }
        }
    }
}