using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mallet : MonoBehaviour
{
    bool isMousePressed = false;
    private Rigidbody2D rb;

    float velocidadTotal = 0;
    private Vector3 posicionAnterior;
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        posicionAnterior = transform.position;
    }

    void Update()
    {
        Vector3 posicionActual = transform.position;
        float tiempoTranscurrido = Time.deltaTime;
        Vector3 velocidad = (posicionActual - posicionAnterior) / tiempoTranscurrido;
        posicionAnterior = posicionActual;
        velocidadTotal = velocidad.magnitude;


        // Obtener la posición del mouse en pantalla
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Convertir la posición del mouse a un rayo en el espacio 3D
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

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
                    transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisión es con el objeto controlado por el mouse
        if (collision.gameObject.CompareTag("puck"))
        {
            GameObject puck = GameObject.FindWithTag("puck");

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = ((mousePosition - puck.transform.position) * -1).normalized;

            puck.GetComponent<Rigidbody2D>().AddForce(direction * velocidadTotal, ForceMode2D.Impulse);
        }
    }
}
