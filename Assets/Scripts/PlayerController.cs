using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection;
    private Vector2 Speed = Vector2.zero;
    public GameObject sonar;
    public float topSpeed = 5f;
    public float decelerationMult = 0.9f;
    private Rigidbody2D rb;
    public float accelerationMult = 0.3f;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sonar.SetActive(false);
        StartCoroutine(SonarBurst());
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
}
