using UnityEngine;

public class Mallet : MonoBehaviour
{
    bool isMousePressed = false;
    float velocidadTotal = 0;
    private Vector3 posicionAnterior;

    void Start()
    {
        posicionAnterior = transform.position;
    }

    void Update()
    {
        // calcular velocidad
        Vector3 posicionActual = transform.position;
        float tiempoTranscurrido = Time.deltaTime;
        Vector3 velocidad = (posicionActual - posicionAnterior) / tiempoTranscurrido;
        posicionAnterior = posicionActual;
        velocidadTotal = velocidad.magnitude;

        // Obtener la posición del mouse en pantalla
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Convertir la posición del mouse a un rayo en el espacio 3D
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Crear un RaycastHit para almacenar la información del rayo
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // Verificar si el rayo intersecta con un collider
        if (hit.collider != null)
        {
            // Verificar si el collider pertenece al objeto que tiene este script
            if (hit.collider.gameObject == gameObject)
            {
                if (Input.GetMouseButtonDown(0)) isMousePressed = true;
                if (Input.GetMouseButtonUp(0)) isMousePressed = false;

                if (isMousePressed)
                {
                    MoveMallet(mousePosition);
                }
            }
        }
    }

    private void MoveMallet(Vector3 targetPosition)
    {
        // Calcular la posición deseada del mallet
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

        // Mover el mallet solo si no hay colisiones con otros colliders
        if (canMove)
        {
            transform.position = desiredPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisión es con el objeto controlado por el mouse
        if (collision.gameObject.CompareTag("puck"))
        {
            GameObject puck = collision.gameObject;

            Vector2 direction = (puck.transform.position - transform.position).normalized;

            // Ajustar la fuerza aplicada al puck según la velocidad del mallet
            float forceMultiplier = 2.0f; // Puedes ajustar este valor según sea necesario
            Vector2 force = forceMultiplier * velocidadTotal * direction;
            // Limitar la velocidad máxima del puck
            float maxSpeed = 10.0f; // Ajusta este valor según sea necesario
            if (force.magnitude > maxSpeed)
            {
                force = force.normalized * maxSpeed;
            }
            puck.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }
}
