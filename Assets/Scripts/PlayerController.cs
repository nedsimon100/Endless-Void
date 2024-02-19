using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection;
    private Vector2 Speed = Vector2.zero;
    public GameObject sonar;
    public float topSpeed = 5f;
    public float decelerationMult = 0.9f;
    private Rigidbody2D rb;
    public float accelerationMult = 0.3f;
    public GameObject damageDisplay;
    public float damagePercent;
    public GameObject damageParticles;
    public TextMeshProUGUI xPos;
    public TextMeshProUGUI yPos;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sonar.SetActive(false);
        StartCoroutine(SonarBurst());
        StartCoroutine(healDamage());
    }

    IEnumerator SonarBurst()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sonar.SetActive(true);
                yield return new WaitForSeconds(5);
                sonar.SetActive(false);
            }
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetInputs();
        checkDamage();
        updateHud();
    }
    public void updateHud()
    {
        xPos.text = "X: " + Mathf.FloorToInt(this.transform.position.x).ToString();
        yPos.text = "Y: " + Mathf.FloorToInt(this.transform.position.y).ToString();
    }
    public void checkDamage()
    {
        damageDisplay.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, damagePercent * 2);
        if (damagePercent > 100)
        {
            //gameOver
        }
    }
    private void FixedUpdate()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        Speed.x = rb.velocity.x + (moveDirection.x* accelerationMult);
        Speed.y = rb.velocity.y + (moveDirection.y * accelerationMult);
        Speed *= decelerationMult;
        Vector2.ClampMagnitude(Speed, topSpeed);
        rb.velocity = Speed;
    }
    private void GetInputs()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection = moveDirection.normalized;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        damagePercent += rb.velocity.magnitude * 5;
        damageParticles.SetActive(false);
        damageParticles.SetActive(true);
    }
    IEnumerator healDamage()
    {

        while (true)
        {
            if (damagePercent > 0)
            {
                damagePercent--;
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }

       
    }
}
