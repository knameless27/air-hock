using UnityEngine;

public class Mallet : MonoBehaviour
{
    bool isMousePressed = false;
    float velocidadTotal = 0;
    private Vector3 posicionAnterior;
    private ParticleSystem particles;

    void Start()
    {
        posicionAnterior = transform.position;
        particles = GetComponent<ParticleSystem>();
        particles.Stop();
    }

    void Update()
    {
        // calcular velocidad
        Vector3 posicionActual = transform.position;
        float tiempoTranscurrido = Time.deltaTime;
        Vector3 velocidad = (posicionActual - posicionAnterior) / tiempoTranscurrido;
        posicionAnterior = posicionActual;
        velocidadTotal = velocidad.magnitude;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (Input.GetMouseButtonDown(0)) isMousePressed = true;
                if (Input.GetMouseButtonUp(0)) isMousePressed = false;

                if (isMousePressed)
                {
                    MoveMallet(mousePosition);
                    if (velocidadTotal > 0.1f)
                    {
                        particles.Play();
                    }
                }
                else particles.Stop();
            }
        }
    }

    private void MoveMallet(Vector3 targetPosition)
    {
        Vector3 desiredPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(desiredPosition, GetComponent<Collider2D>().bounds.extents.x);

        bool canMove = true;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && !collider.gameObject.CompareTag("puck"))
            {
                canMove = false;
                break;
            }
        }

        if (canMove)
        {
            transform.position = desiredPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("puck"))
        {
            GameObject puck = collision.gameObject;

            Vector2 direction = (puck.transform.position - transform.position).normalized;

            float forceMultiplier = 2.0f;
            Vector2 force = forceMultiplier * velocidadTotal * direction;
            
            float maxSpeed = 10.0f;
            if (force.magnitude > maxSpeed)
            {
                force = force.normalized * maxSpeed;
            }
            puck.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }
}
