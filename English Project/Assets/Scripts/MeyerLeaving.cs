using UnityEngine;
using System.Collections;
public class MeyerLeaving : MonoBehaviour
{

    public Vector3 up;
    public Vector3 left;
    public Vector3 down;
    public float time;
    Objectives ob;
    public Objectives objectOb;
    Animator anim;
    MinigameManager mm;
    public GameObject mmObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mmObj != null)
        {
            mm = mmObj.GetComponent<MinigameManager>();
        }
 {}     anim = GetComponent<Animator>();
        ob = objectOb.GetComponent<Objectives>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ob.Run)
        {
            Debug.Log("Began");
            ob.Run = false;
            anim.SetBool("IdleTBack", true);
            StartCoroutine(MeyerLeaves(up));
        }
    }
    public IEnumerator MeyerLeaves(Vector3 use)
    {
        Vector3 start = transform.position;
        Vector3 end = use;
        float t = 0;
        while (t < time)
        {
            t+= Time.deltaTime;
            float Normal = t/time;
            transform.position = Vector3.Lerp(start, end, Normal);
            yield return null;
        }
        if (use == up) {
            anim.SetBool("BackTSide", true);

            StartCoroutine(MeyerLeaves(left));
        } else if (use == left)
        {
            anim.SetBool("SideTFront", true);

            StartCoroutine(MeyerLeaves(down));
        } else
        {
            mm.StartGame = true;
        }

    }
}
