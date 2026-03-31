using UnityEngine;
using System.Collections;
public class CarCumin : MonoBehaviour
{
    public float dur;
    public GameObject trans;
    raycast rc;
    public GameObject RayCastObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rc = RayCastObj.GetComponent<raycast>();
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        if (rc.nickLeave)
        {
            trans.transform.position = new Vector2(trans.transform.position.x + 15, trans.transform.position.y);
            StartCoroutine(Move());
            rc.nickLeave = false;
        }
    }
    private IEnumerator Move()
    {
        float t = 0;
        Vector2 start = transform.position;
        Vector2 end = trans.transform.position;
        while (t < dur)
        {
            t+= Time.deltaTime;
            float norm = t / dur;
            transform.position = Vector2.Lerp(start, end, norm);
            yield return null;
        }
    }
}
