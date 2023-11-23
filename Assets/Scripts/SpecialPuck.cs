using UnityEngine;

public class SpecialPuck : MonoBehaviour
{
    private Rigidbody2D rb;
    string[] sexito;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Spawn(string side)
    {
        sexito = new string[] { "biggerMallet", "barrier", "changePositionGoal" };
        gameObject.SetActive(true);

        if (side == "left") transform.position = new Vector3(-02f, -3f, 0f);
        if (side == "right") transform.position = new Vector3(02f, -3f, 0f);

        if (rb)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        int xd = Random.Range(0, sexito.Length - 1);
        Debug.Log(xd);
    }
}
