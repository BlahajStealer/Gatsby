using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class carScriptNPC : MonoBehaviour
{
    public bool Police = false;
    public float speed;
    public Rigidbody2D rb;
    Vector2 move;
    public GameObject Player;
    public GameObject self;
    public float duration = 2f;
    public Vector2 end;
    CarScript cs;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Player != null)
        {
            cs = Player.GetComponent<CarScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Police)
        {
            if (cs.Finishing)
            {
                StartCoroutine(moveObj(self));
                Police = false;
            }
        }
    }
    private void FixedUpdate()
    {
        move.y = 0;
        move.x = 1;
        Vector2 position = (Vector2)rb.position + move * speed * Time.deltaTime;
        rb.MovePosition(position);

    }
    public IEnumerator moveObj(GameObject Moving)
    {

        Vector2 start;
        start = Moving.transform.localPosition;
        float starty = start.y;
        float t = 0;
        while (t < duration)
        {
            if (t < duration/2)
            {
                t += Time.deltaTime;
                float normalized = t / duration;
                Moving.transform.localPosition = Vector3.Lerp(new Vector2(start.x, starty), end, normalized);
                yield return null;
            } else
            {
                t += Time.deltaTime;
                float normalized = t / duration;
                Moving.transform.localPosition = Vector3.Lerp(start, end, normalized);
                yield return null;
            }

        }
        Moving.transform.position = end;


    }
}
