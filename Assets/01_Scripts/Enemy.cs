using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType type;
    public float life = 3;
    public float speed = 2;
    public float timeBtwShoot = 1.5f;
    float timer = 0;
    public float range = 4;
    public float damage = 1f;
    bool targetInRange = false;
    Transform target;
    public Transform firePoint;
    public Bullet bulletPrefab;
    public float bulletspeed = 5f;
    public ParticleSystem explosionEffectPrefab;

    void Start()
    {
        Player playerComponent = FindObjectOfType<Player>();
        if (playerComponent != null)
        {
            target = playerComponent.transform;
        }
        else
        {
            Debug.LogError("Player component not found!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case EnemyType.Normal:
                MoveForward();
                break;
            case EnemyType.NormalShoot:
                MoveForward();
                Shoot();
                break;
            case EnemyType.Kamikase:
                if (targetInRange)
                {
                    RotateToTarget();
                    MoveForward(2);
                }
                else
                {
                    MoveForward();
                    SearchTarget();
                }
                break;
            case EnemyType.Sniper:
                if (targetInRange)
                {
                    RotateToTarget();
                    Shoot();
                }
                else
                {
                    MoveForward();
                    SearchTarget();
                }
                break;
            default:
                break;
        }
    }
    public void TakeDamage(float damage)
    {
        life-=damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
    void MoveForward()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    void MoveForward(float m)
    {
        transform.Translate(Vector2.up * speed*m * Time.deltaTime);
    }
    void RotateToTarget()
    {
        Vector2 dir=target.position-transform.position;
        float angelZ= Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg+0;
        transform.rotation = Quaternion.Euler(0,0,-angelZ);
    }
    void SearchTarget()
    {
        if (target == null)
        {
            targetInRange = false; // Si no hay objetivo, definitivamente no está en rango.
            return; // Salimos de la función porque no hay más qué hacer aquí.
        }

        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= range)
        {
            targetInRange = true;
        }
        else
        {
            targetInRange = false;
        }
    }
    void Shoot()
    {
        if (timer<timeBtwShoot)
        {
            timer += Time.deltaTime;       
        }
        else
        {
            timer = 0;
            Bullet b = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            b.damage = damage;
            b.speed = bulletspeed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
          if (collision.gameObject.CompareTag("Player"))
          {
            Player p = collision.gameObject.GetComponent<Player>();
            p.TakeDamage(damage);
            TriggerExplosion();
            Destroy(gameObject);
      
          }else if (collision.gameObject.CompareTag("Floor"))
          {
            life = 0;
            TriggerExplosion();
            Destroy(gameObject);
          }
    }
    void TriggerExplosion()
    {
        if (explosionEffectPrefab != null)
        {
            ParticleSystem explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            explosionEffect.Play();
            Destroy(explosionEffect.gameObject, explosionEffect.main.duration);
        }
    }

    public enum EnemyType
    {
        Normal,
        NormalShoot,
        Kamikase,
        Sniper
    }
}
