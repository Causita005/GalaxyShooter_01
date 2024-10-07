using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float timeBtwSpawn = 1.5f;
    float timer = 0;
    public Transform leftpoint;
    public Transform rightpoint;
    public List<GameObject> EnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }
    void SpawnEnemy()
    {
        if (timer < timeBtwSpawn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            float x=Random.Range(leftpoint.position.x, rightpoint.position.x);
            int enemy=Random.Range(0,EnemyPrefab.Count);
            Vector3 newpost=new Vector3(x,transform.position.y,0);
            Instantiate(EnemyPrefab[enemy], newpost, Quaternion.Euler(0, 0, 180));
        }
    }
}
