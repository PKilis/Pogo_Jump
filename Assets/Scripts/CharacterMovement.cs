using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{

    public Rigidbody rb;

    private float jumpForce = 5;
    private float speed = 0.1f;

    public GameObject barrelPieces;
    public GameObject starPoof;
    public GameObject confetti;
    public GameObject stick;
    public GameObject uiComplete;
    public GameObject particleEff;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Physics.IgnoreLayerCollision(3, 6);
    }
    public enum JumpMode
    {
        Low = 2,
        Normal = 5,
        High = 8,
        Extreme = 10
    }

    private void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(0, 0, speed);
    }

    public void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            Jump();
        }
        if (collision.gameObject.CompareTag("Barrel"))
        {
            Instantiate(barrelPieces, collision.gameObject.transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Pebble"))
        {
            StartSliding();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Star"))
        {
            Destroy(other.gameObject);
            Instantiate(starPoof, other.transform.position + new Vector3(0, 1, 0), Quaternion.Euler(-90, 0, 0));
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            FinishLevelEvent();
        }
    }


    private void StartSliding()
    {
        anim.enabled = false;
        rb.velocity = Vector3.zero;
        jumpForce = 0;

        var part = Instantiate(particleEff, gameObject.transform.position + new Vector3(0, 0, -0.3f), gameObject.transform.rotation);
        part.transform.parent = gameObject.transform;
        part.tag = "Particle";


        gameObject.transform.DOLocalRotate(new Vector3(320, 0, 0), 1);
        speed = 0.2f;
    }

    private void FinishLevelEvent()
    {
        GameObject[] particles = GameObject.FindGameObjectsWithTag("Particle");
        foreach (GameObject p in particles)
        {
            p.SetActive(false);
        }

        anim.enabled = true;
        rb.velocity = Vector3.zero;
        jumpForce = 0;
        gameObject.transform.rotation = Quaternion.Euler(-1.51f, -215, -8.49f);
        stick.SetActive(false);
        anim.SetBool("isDancing", true);
        Instantiate(confetti, gameObject.transform.position + new Vector3(0, 2, 3), Quaternion.Euler(-90, 0, 0));
        Instantiate(uiComplete, transform.position, transform.rotation);
    }
}
