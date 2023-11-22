using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class puck : MonoBehaviour
{
    int puntuationRight = 0, puntuationLeft = 0;
    private Rigidbody2D rb;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    void Start()
    {
        // Obtener el componente Rigidbody2D del objeto
        rb = GetComponent<Rigidbody2D>();
        leftText = GameObject.Find("leftText").GetComponent<TextMeshProUGUI>();
        rightText = GameObject.Find("rightText").GetComponent<TextMeshProUGUI>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "goalRight")
        {
            puntuationRight++;
            resetPuck("right");
            rightText.text = "" + puntuationRight;
        }
        if (collision.gameObject.name == "goalLeft")
        {
            puntuationLeft++;
            resetPuck("left");
            leftText.text = "" + puntuationLeft;
        }

    }

    void resetPuck(string side)
    {
        if (side == "left") transform.position = new Vector3(-02f, 0f, 0f);
        if (side == "right") transform.position = new Vector3(02f, 0f, 0f);
        // Establecer la velocidad del Rigidbody2D en cero
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f; // También puedes establecer la velocidad angular en cero si es relevante
    }
}
