using UnityEngine;
using TMPro;

public class Puck : MonoBehaviour
{
    readonly int PuntuationRight = 0, PuntuationLeft = 0;
    private Rigidbody2D rb;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public SpecialPuck sp;

    void Start()
    {
        // Obtener el componente Rigidbody2D del objeto
        rb = GetComponent<Rigidbody2D>();
        leftText = GameObject.Find("leftText").GetComponent<TextMeshProUGUI>();
        rightText = GameObject.Find("rightText").GetComponent<TextMeshProUGUI>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("goalRight"))
        {
            HandleGoalScored(PuntuationLeft, leftText);
            ResetPuck("right");
        }
        else if (collision.gameObject.CompareTag("goalLeft"))
        {
            HandleGoalScored(PuntuationRight, rightText);
            ResetPuck("left");
        }
    }

    private void HandleGoalScored(int score, TextMeshProUGUI scoreText)
    {
        score++;
        scoreText.text = score.ToString();
    }


    void ResetPuck(string side)
    {
        sp.SetBarrier("left", false);
        sp.SetBarrier("right", false);

        GameObject malletR = GameObject.FindGameObjectWithTag("malletRight");
        GameObject malletL = GameObject.FindGameObjectWithTag("malletLeft");

        malletR.transform.localScale = Vector3.one;
        malletL.transform.localScale = Vector3.one;

        sp.Spawn(side);
        transform.position = new Vector3(side == "left" ? -2f : 2f, 0f, 0f);

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
