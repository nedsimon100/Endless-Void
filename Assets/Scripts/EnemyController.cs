using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyType;
    public PlayerController player;
    private Rigidbody2D rb;
    public Vector3 lastSonarPos;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = this.GetComponent<Rigidbody2D>();
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

                break;
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
       
        rb.velocity = player.transform.position - this.transform.position;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == player.gameObject)
        {
            player.damagePercent += 10;
        }
    }
}
