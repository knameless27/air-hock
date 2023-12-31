using UnityEngine;

public class SpecialPuck : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject BarrierLeft;
    public GameObject BarrierRight;
    public float velocidadCambioColor = 0.2f;
    public float umbralColorOscuro = 0.2f;
    private SpriteRenderer spriteRenderer;
    private string[] Powers = { "biggerMallet", "barrier" };

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Color nuevoColor = new Color(Mathf.Sin(Time.time * velocidadCambioColor),
                                    Mathf.Cos(Time.time * velocidadCambioColor),
                                    Mathf.Tan(Time.time * velocidadCambioColor));

        nuevoColor.r = Mathf.Max(nuevoColor.r, umbralColorOscuro);
        nuevoColor.g = Mathf.Max(nuevoColor.g, umbralColorOscuro);
        nuevoColor.b = Mathf.Max(nuevoColor.b, umbralColorOscuro);

        // Aplica el nuevo color al material del objeto
        spriteRenderer.color = nuevoColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("goalRight")) ActivatePower("Right");
        if (collision.gameObject.CompareTag("goalLeft")) ActivatePower("Left");
    }

    private void ActivatePower(string side)
    {
        int index = Random.Range(0, Powers.Length);

        string oppositeSide = (side == "Left") ? "Right" : "Left";

        if (Powers[index] == "biggerMallet")
            ResizeMallet(oppositeSide);
        else
            SetBarrier(side, true);

        ResetPuck();
    }

    public void Spawn(string side)
    {
        gameObject.SetActive(true);

        float xPos = (side.ToLower() == "left") ? -2f : 2f;
        transform.position = new Vector3(xPos, -3f, 0f);

        ResetRigidbody();
    }

    private void ResizeMallet(string side)
    {
        GameObject mallet = GameObject.FindGameObjectWithTag("mallet" + side);
        if (!mallet) return;

        mallet.transform.localScale = new Vector3(2f, 2f, 0);
    }

    public void SetBarrier(string side, bool state)
    {
        if (side.ToLower() == "left")
            BarrierRight.SetActive(state);
        else
            BarrierLeft.SetActive(state);
    }

    private void ResetPuck()
    {
        ResetRigidbody();
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void ResetRigidbody()
    {
        if (!rb) return;

        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
