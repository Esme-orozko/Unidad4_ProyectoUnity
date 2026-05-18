using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;

public class BallController : MonoBehaviour
{

    public Rigidbody rb;
    public float impulseForce = 3f;
    private bool ignoreNextCollision;
    private Vector3 startPosition;
    public int perfectPass;
    public float superSpeed = 10;
    private bool isSuperSpeedActive;
    private int perfectPassCount = 3;
    public AudioSource collisionAudio;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisionAudio.Play();
        if (ignoreNextCollision)
        {
            return;
        }

        if (isSuperSpeedActive && !collision.transform.GetComponent<GoalController>())
        {
            Destroy(collision.transform.parent.gameObject,0.2f);
        }
        else
        {
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
                if (deathPart)
                {
                    GameManager.singleton.RestartLevel();
                }
        }
        
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up*impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllownextCollision", 0.2f);

        perfectPass = 0;
        isSuperSpeedActive = false;
    }

    private void Update()
    {
        if (perfectPass >= perfectPassCount && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down*superSpeed,ForceMode.Impulse);
        }
    }
    private void AllownextCollision()
    {
        ignoreNextCollision = false;
    }

    public void ResetBall()
    {
        transform.position = startPosition;
    }

}
