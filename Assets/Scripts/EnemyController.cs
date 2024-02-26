using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyType;
    public PlayerController player;
    private Rigidbody2D rb;
    public Vector3 lastSonarPos;

    public GameObject EnemyParticals;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = this.GetComponent<Rigidbody2D>();
        lastSonarPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyType)
        {
            case 1:
                type1Move();
                break;
            case 2:
                type2Move();
                break;
            case 3:
                type3Move();
                break;
        }
        if ((player.transform.position - this.transform.position).magnitude > 100)
        {
            Destroy(this.gameObject);
        }
    }
    public void type1Move()
    {
        rb.velocity = player.transform.position - this.transform.position;
        if((player.transform.position-this.transform.position).magnitude<10&&player.sonar.activeInHierarchy == true)
        {
            Destroy(this.gameObject);
        }
    }
    public void type2Move()
    {
        if (((player.transform.position - this.transform.position).magnitude < 10 && player.sonar.activeInHierarchy == true )|| ((player.transform.position - this.transform.position).magnitude < 5 && player.damageParticles.activeInHierarchy == true))
        {
            lastSonarPos = player.transform.position;
        }
        rb.velocity = lastSonarPos - this.transform.position;
        if (rb.velocity.magnitude > 0.1)
        {
            EnemyParticals.SetActive(true);
        }
        else
        {
            EnemyParticals.SetActive(false);
        }
    }
    public void type3Move()
    {
       
        rb.velocity = player.transform.position - this.transform.position;


        if (rb.velocity.magnitude > 0.1)
        {
            EnemyParticals.SetActive(true);
        }
        else
        {
            EnemyParticals.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player.gameObject)
        {
            if(enemyType == 1)
            {

            }
            else
            {
                player.GameOver();
                Destroy(this.gameObject);
            }
        }
    }
}
