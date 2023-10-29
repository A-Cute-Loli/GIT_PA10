using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animation thisAnimation;
    private Rigidbody rb;
    public GameObject player;
    [SerializeField] private float velocity = 3.5f;
    [SerializeField] private float rotationspeed = 10f;
    [SerializeField] private GameManager GM;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thisAnimation = GetComponent<Animation>();
        thisAnimation["Flap_Legacy"].speed = 3;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            thisAnimation.Play();
            rb.velocity = Vector2.up * velocity;
        }

        if(player.transform.position.y >= 3.5f)
        {
            velocity = 0;
        }
        else if(player.transform.position.y <= -4.5f)
        {
            SceneManager.LoadScene("LoseScene");
        }
        else
        {
            velocity = 3.5f;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, rb.velocity.y * rotationspeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Score")
        {
            GM.GetComponent<GameManager>().UpdateScore(1);
        }
    }
}
