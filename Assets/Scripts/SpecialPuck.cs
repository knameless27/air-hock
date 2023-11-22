using UnityEngine;

public class SpecialPuck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(string side) {
        gameObject.SetActive(true);
        if (side == "left")
        {
            transform.position = new Vector3(-02f, 3f, 0f);
        }
        if (side == "right")
        {
            transform.position = new Vector3(02f, 3f, 0f);
        }
    }
}
