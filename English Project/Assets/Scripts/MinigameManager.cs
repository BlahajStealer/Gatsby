using UnityEngine;
using System.Collections;
public class MinigameManager : MonoBehaviour
{
    public GameObject Leavable;
    public GameObject[] customers;
    public bool StartGame;
    GameObject activeObj;
    int currentObj = 0;
    public Transform start;
    public Transform up;
    public Transform right;
    bool next = true;
    bool second = false;
    bool startup = false;
    TextSystem ts;
    public GameObject tsObj;
    public string[] CustomerLines;
    string currentline = "";
    public GameObject button1;
    public GameObject button2;
    public GameObject panel;
    public int[] corrects;
    public GameObject loss;
    public bool CanLeave = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ts = tsObj.GetComponent<TextSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StartGame)
        {
            if (next)
            {
                if (currentObj != 0)
                {
                    next = false;
                    StartCoroutine(move(up, .75f));
                    second = true;

                } else
                {
                    
                    activeObj = Instantiate(customers[currentObj], start.transform.position, start.transform.rotation);
                    currentObj++;
                    next = false;
                    StartCoroutine(move(up, 2));
                }
                
            }

        }
        if (startup)
        {
            if (currentObj == customers.Length)
            {
                Debug.Log("You won!");
                Leavable.SetActive(false);
                CanLeave = true;
            } else
            {
                Debug.Log("Start");
                activeObj = Instantiate(customers[currentObj], start.transform.position, start.transform.rotation);
                currentObj++;
                startup = false;
                next = false;
                StartCoroutine(move(up, 2));
            }
            
        }
    }
    public void nextFunc()
    {
        if (currentline != CustomerLines[currentObj-1])
        {
            panel.SetActive(true);
            button1.SetActive(true);
            button2.SetActive(true);
            currentline = CustomerLines[currentObj-1];
            ts.Interaction(currentline, 0);
            
        }
    }
    public void refusal()
    {
        panel.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        ts.off();
        next = true;
    }
    public void approval()
    {
        if (corrects[currentObj-1] == 1)
        {
            loss.SetActive(true);
        } else
        {
            panel.SetActive(false);
            button1.SetActive(false);
            button2.SetActive(false);
            ts.off();
            next = true;
        }
        
    }
    private IEnumerator move(Transform ts, float len)
    {
        Vector2 Starting = activeObj.transform.position;
        float time = 0;
        while (time < len)
        {
            time+=Time.deltaTime;
            float normal = time/len;
            activeObj.transform.position = Vector3.Lerp(Starting, ts.transform.position, normal);
            yield return null;
        }
        if (ts == up && !second)
        {
            StartCoroutine(move(right, .75f));
        } else if (ts == up && second)
        {
            StartCoroutine(move(start, 2));
        } else if (ts==start)
        {
            second = false;
            startup = true;
        }
    }
}
