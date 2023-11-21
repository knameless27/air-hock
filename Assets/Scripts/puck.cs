using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puck : MonoBehaviour
{
    int puntuationRight = 0, puntuationLeft = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisión es con el objeto controlado por el mouse
        if (collision.gameObject.name == "right")
        {
            puntuationRight++;
        }
        if (collision.gameObject.name == "left")
        {
            puntuationLeft++;
        }
        Debug.Log("puntuacion: " + "R: " + puntuationRight + ", L: " + puntuationLeft);
    }
}
