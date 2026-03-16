using UnityEngine;
using System.Collections;

using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Objectives : MonoBehaviour
{
    public GameObject ObjectivesText;//-714 456
    public GameObject Table;//-714 456
    public GameObject Bar;//-714 456
    public GameObject Band;//-714 456
    public GameObject Phone;//-714 456
    public float duration = .5f;
    public Vector3 start;
    public Vector3 end;
    bool started = true;
    bool middle1 = false;
    bool middle2 = false;
    bool Ended = false;
    public bool TableDone;
    public bool BarDone;
    public bool BandDone;
    public int objectivesFinished = 0;
    float endx;
    bool startNext = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            endx = end.x;

            started = false;
            StartCoroutine(moveObj(ObjectivesText, true, false, false));

        }
        if (middle1)
        {
            middle1 = false;
            StartCoroutine(moveObj(Table, false, true, false));

        }
        if (middle2)
        {
            middle2 = false;
            StartCoroutine(moveObj(Bar, false, false, true));

        } if (Ended)
        {
            Ended = false;
            StartCoroutine(moveObj(Band, false, false, false));

        }


        if (BandDone)
        {
            BandDone = false;
            Debug.Log("Here");
            StartCoroutine(MoveAway(Band));
        }
        if (TableDone)
        {
            TableDone = false;
            StartCoroutine (MoveAway(Table));

        }
        if (BarDone)
        {
            BarDone = false;
            StartCoroutine(MoveAway(Bar));

        }
        if (objectivesFinished == 3)
        {
            StartCoroutine(moveObj(Phone, false, false, false));
        }
    }
    public IEnumerator moveObj(GameObject Moving, bool first, bool second, bool third)
    {
        end.x = -714;

        start = Moving.transform.localPosition;
        end.y = Moving.transform.localPosition.y;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;
            Moving.transform.localPosition = Vector3.Lerp(start, end, normalized);
            yield return null;
        }
        if (first)
        {
            middle1 = true;
        }
        else if (second)
        {
            middle2 = true;
        } else if (third)
        {
            Ended = true;
        }

    }    
    public IEnumerator MoveAway(GameObject Moving)
    {
        endx = end.x;
        start = Moving.transform.localPosition;
        end.y = Moving.transform.localPosition.y;
        end.x = -1271;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;
            Moving.transform.localPosition = Vector3.Lerp(start, end, normalized);
            yield return null;
        }

        objectivesFinished++;

    }
}
