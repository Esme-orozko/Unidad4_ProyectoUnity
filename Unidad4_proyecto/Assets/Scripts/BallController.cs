using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float impulseForce = 3f;
    private bool ignoreNextCollision;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (ignoreNextCollision)
        {
            return;
        }
        DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
        if (deathPart)
        {
            GameManager.singleton.RestartLevel();
        }

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up*impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke("AllownextCollision", 0.2f);
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
