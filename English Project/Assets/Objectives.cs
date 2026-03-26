using UnityEngine;
using System.Collections;

using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Objectives : MonoBehaviour
{
    public GameObject ObjectivesText;//-714 456
    public GameObject Table;//-714 456
    public GameObject Bar;//-714 456
    public GameObject Band;//-714 456
    public GameObject Phone;//-714 
    public GameObject Wolf;
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
    public bool phoneDone = false;
    bool[] startedBools = {false,false,false};
    bool did = true;
    bool WolfIncoming = false;
    public bool DoneScene = false;
    public bool newObj;
    public bool pharm;
    public bool Run;
    public GameObject RayCast;
    raycast rc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (RayCast != null)
        {
            rc = RayCast.GetComponent<raycast>();
        }
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
        if (rc != null)
        {
            if (rc.Scene4)
            {
                StartCoroutine(MoveAway(ObjectivesText));
                rc.Scene4 = false;
            }
        }

        if (BandDone && !startedBools[0])
        {
            BandDone = false;
            Debug.Log("Here");
            StartCoroutine(MoveAway(Band));
            startedBools[0] = true;
        }
        if (TableDone && !startedBools[1])
        {
            TableDone = false;
            StartCoroutine (MoveAway(Table));
            startedBools[1] = true;

        }
        if (BarDone && !startedBools[2])
        {
            BarDone = false;
            StartCoroutine(MoveAway(Bar));
            startedBools[2] = true;
        }
        if (objectivesFinished == 3 && did)
        {
            did = false;
            StartCoroutine(moveObj(Phone, false, false, false));
        }
        if (phoneDone) {
            StartCoroutine(MoveAway(Phone));
            phoneDone = false;
        }
        if (WolfIncoming)
        {
            DoneScene = true;
            WolfIncoming = false;
            StartCoroutine(moveObj(Wolf, false, false, false));
        }
        if (newObj)
        {
            StartCoroutine(MoveAway(Table));
        }
        if (pharm)
        {
            StartCoroutine(moveObj(Wolf, false, false, false));
            pharm = false;
            Run = true;
        }

    }
    public IEnumerator moveObj(GameObject Moving, bool first, bool second, bool third)
    {
        bool pharmTemp;
        if (newObj)
        {
            newObj = false;
            pharmTemp = true;
        } else
        {
            pharmTemp = false;
        }
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
        if (pharmTemp)
        {
            pharm = true;
        }

    }    
    public IEnumerator MoveAway(GameObject Moving)
    {
        start = Moving.transform.localPosition;
        end.y = Moving.transform.localPosition.y;
        end.x = -1271;
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float normalized = t / duration;
            Moving.transform.localPosition = Vector3.Lerp(start, new Vector3(-1271,Moving.transform.localPosition.y, 0), normalized);
            yield return null;
        }
        if (Moving == Phone)
        {
            WolfIncoming = true;
        }
        objectivesFinished++;

    }
}
