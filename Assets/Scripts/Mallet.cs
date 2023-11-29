using UnityEngine;

public class Mallet : MonoBehaviour
{
    bool isTouchPressed = false;
    float velocidadTotal = 0;
    private Vector3 posicionAnterior;

    void Start()
    {
        posicionAnterior = transform.position;
    }

    void Update()
    {
        // Calcular velocidad
        Vector3 posicionActual = transform.position;
        float tiempoTranscurrido = Time.deltaTime;
        Vector3 velocidad = (posicionActual - posicionAnterior) / tiempoTranscurrido;
        posicionAnterior = posicionActual;
        velocidadTotal = velocidad.magnitude;

        // Verificar toques en la pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

            // if (hit.collider != null)
            // {
            //     if (hit.collider.gameObject == gameObject)
            //     {
                    if (touch.phase == TouchPhase.Began) isTouchPressed = true;
                    if (touch.phase == TouchPhase.Ended) isTouchPressed = false;

                    if (isTouchPressed)
                    {
                        MoveMallet(touchPosition);
                    }
            //     }
            // }
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
