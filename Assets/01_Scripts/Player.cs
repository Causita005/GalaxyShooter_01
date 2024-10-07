using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    //random.range(0,2);
    public float damage = 1f; 
    public float speed = 2f;
    public float bulletspeed = 5f;
    public Transform firePoint;
    public Bullet bulletPrefab;
    public Rigidbody2D rb;
    float cargadores = 0;
    public float timeBtwShoot = 0.2f;
    float timer = 0;
    public float life = 3;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Inicio del juego");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Juego en proceso");
        Movement();
        Shoot();
        Reload();
    }
    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(x, y) * speed;
    }
    void Shoot()
    {
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) && timer >= timeBtwShoot && cargadores < 5)
            {
                Bullet b=Instantiate(bulletPrefab, firePoint.position, transform.rotation);
                b.damage = damage;
                b.speed = bulletspeed;
                timer = 0; 
                cargadores++;
            } 
    }
    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            cargadores = 0;
        }   
    }
    public void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void DamageEx(float damageEx)
    {
        damage = damageEx;
        Shoot();
    }
    public void VelocityEx(float Velocity)
    {
        speed = Velocity;
        Movement();
    }
    public void VelocityPro(float Velocityp)
    {
        bulletspeed = Velocityp;
        Shoot();
    }
    public void CriticalD(float DamageC)
    {
        DamageC = damage;
        Shoot();
    }  
}
