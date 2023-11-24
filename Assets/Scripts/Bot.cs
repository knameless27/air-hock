using System.Collections;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private Transform puck;
    private Vector2 targetPosition;
    readonly private float MoveSpeed = 10f;
    readonly private float HitRange = 1f;
    public bool IsPlayer = false;

    void Start()
    {
        puck = GameObject.FindGameObjectWithTag("puck").transform;
        StartCoroutine("MoveTowardsPuck");
        if (IsPlayer)
        {
            gameObject.AddComponent<Mallet>();
            Destroy(GetComponent<Bot>());
        }
    }

    IEnumerator MoveTowardsPuck()
    {
        while (true)
        {
            if (puck.position.x > 0)
            {
                targetPosition = new Vector2(puck.position.x, puck.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, MoveSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, puck.position) < HitRange) HitPuck();
                puck = GameObject.FindGameObjectWithTag("puck").transform;
            }

            yield return null;
        }
    }

    void HitPuck()
    {
        puck.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 5f);
    }
}
