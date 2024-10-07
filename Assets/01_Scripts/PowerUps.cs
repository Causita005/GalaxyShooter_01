using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class PowerUp : MonoBehaviour
{
    public PowerupType type;
    public float damage = 1.5f;
    public float speed = 8f;
    public float bulletspeed = 8f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Destroy(gameObject);

            switch (type)
            {
                case PowerupType.DamageExtra:
                    player.DamageEx(damage);
                    break;
                case PowerupType.MovementExtra:
                    player.VelocityEx(speed);
                    break;
                case PowerupType.VelocityProyectile:
                    player.VelocityPro(bulletspeed);
                    break;
                case PowerupType.Critical:
                    float doble = Random.Range(0, 2);
                    if (doble == 0)
                    {
                        damage = 2f;
                        player.CriticalD(damage);
                    }
                    break;
                case PowerupType.Shield:
                    break;
            }
        }
    }
    public enum PowerupType
    {
        DamageExtra,
        MovementExtra,
        VelocityShoot,
        Critical,
        VelocityProyectile,
        Shield
    }
}