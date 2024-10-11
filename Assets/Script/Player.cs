using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    public GameObject BulletPrefab;
    public float ShootInterval = 1f;
    public float DetectionRange = 10f;
    public Color PlayerColor;
    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        PlayerColor = Color.yellow;
        GetComponent<Renderer>().material.color = PlayerColor;
        InvokeRepeating("ShootBullet", ShootInterval, ShootInterval);
    }
    public void OnMouseDown()
    {
        PlayerColor = (PlayerColor == Color.yellow) ? Color.red : Color.yellow;
        GetComponent<Renderer>().material.color = PlayerColor;
        UpdateColor();
       
    
}

    // Update is called once per frame
    void Update()
    {
        DetectEnemyColors();

    }
    public void DetectEnemyColors()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
        foreach (var collider in hitColliders) 
        {
            if (collider.CompareTag("Enemy"))
            {
                Color EnemyColor = GetComponent<Collider>().GetComponent<Enemy>().GetColor();
                UpdatePlayerColor(EnemyColor);
                CheckGameOver(collider);

            }
        }
    }
    void UpdatePlayerColor(Color EnemyColor)
    {
        if (EnemyColor == Color.red || EnemyColor == Color.yellow)
        {
            PlayerColor = EnemyColor; 
            UpdateColor(); 
        }
    }

    void UpdateColor()
    {
        GetComponent<Renderer>().material.color = PlayerColor; 
        if (BulletPrefab != null)
        {
            BulletPrefab.GetComponent<Bullet>().SetColor(PlayerColor); 
        }
    }

    public void CheckGameOver(Collider EnemyCollider)
    {
        if (EnemyCollider.transform.position == transform.position)
        {
            GameManager.GameOver();
        }
    }

      public  void RotateTowardsNearestEnemy()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
            Transform nearestEnemy = null;
            float nearestDistance = float.MaxValue;

            foreach (var collider in hitColliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestEnemy = collider.transform;
                    }
                }
            }

            if (nearestEnemy != null)
            {
                Vector3 direction = (nearestEnemy.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    
    public void ShootBullet()
    {
        if (BulletPrefab != null)
        {
            GameObject Bullet = Instantiate(BulletPrefab, transform.position, transform.rotation); 
            Bullet.GetComponent<Bullet>().SetColor(PlayerColor); 
        }
    }
}
