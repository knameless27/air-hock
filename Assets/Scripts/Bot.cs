using System.Collections;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private Transform puck;
    private Vector2 targetPosition;
    private float moveSpeed = 10f;
    private float hitRange = 1f;

    void Start()
    {
        puck = GameObject.FindGameObjectWithTag("puck").transform;
        StartCoroutine("MoveTowardsPuck");
    }

    IEnumerator MoveTowardsPuck()
    {
        while (true)
        {
            if (puck.position.x > 0)
            {
                targetPosition = new Vector2(puck.position.x, puck.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, puck.position) < hitRange) HitPuck();
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
