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
    public float MoveSpeed = 5f;

    private void Start()
    {
        PlayerColor = Color.yellow;
        UpdateColor();
        InvokeRepeating("ShootBullet", ShootInterval, ShootInterval);
    }

    private void Update()
    {
        MovePlayer();
        DetectEnemyColors();
        RotateTowardsMouse();
        ShootBullet();


        if (Input.GetMouseButtonDown(0))
        {
            ChangeColor();
        }
    }

    public void MovePlayer()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.position += movement * MoveSpeed * Time.deltaTime;
    }

    private void RotateTowardsMouse()
    {

        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Vector3 direction = (targetPoint - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    }

    private void ChangeColor()
    {
        PlayerColor = (PlayerColor == Color.yellow) ? Color.red : Color.yellow;
        UpdateColor();
    }

    private void UpdateColor()
    {

        GetComponent<Renderer>().material.color = PlayerColor;


        if (BulletPrefab != null)
        {
            Bullet bulletComponent = BulletPrefab.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.SetColor(PlayerColor);
            }
        }
    }

    private void DetectEnemyColors()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Color enemyColor = collider.GetComponent<Enemy>().GetColor();
                UpdatePlayerColor(enemyColor);
                CheckGameOver(collider);
            }
        }
    }

    private void UpdatePlayerColor(Color enemyColor)
    {
        if (enemyColor == Color.red || enemyColor == Color.yellow)
        {
            PlayerColor = enemyColor;
            UpdateColor();
        }
    }

    private void CheckGameOver(Collider enemyCollider)
    {
        if (enemyCollider != null && enemyCollider.transform != null) { 
        if (enemyCollider.transform.position == transform.position)
        {
            GameManager.GameOver();
        }
    }
}
    private void ShootBullet()
    {
        if (BulletPrefab != null)
        {
          
            GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();

            if (bulletComponent != null)
            {
                bulletComponent.SetColor(PlayerColor);
            }
        }
    }
}