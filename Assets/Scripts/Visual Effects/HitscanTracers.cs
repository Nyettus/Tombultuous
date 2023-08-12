using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanTracers : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 startPos;
    private Vector3 endPos;
    // Start is called before the first frame update


    private void OnEnable()
    {
        lr = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        ChaseTowards();
    }

    public void EstablishTrails(Vector3 pos1, Vector3 pos2)
    {
        //transform.position = pos2;
        //transform.LookAt(GameManager._.Master.transform);
        startPos = pos1;
        endPos = pos2;
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, Vector3.Lerp(startPos, endPos, 0.5f));
        lr.SetPosition(2, endPos);
    }

    public void ChaseTowards()
    {
        startPos = Vector3.MoveTowards(startPos, endPos, 1f);
        Vector3 tempEnd = Vector3.MoveTowards(startPos, endPos, 25f);
        lr.SetPosition(1, Vector3.Lerp(startPos, tempEnd, 0.5f));
        lr.SetPosition(0, startPos);
        lr.SetPosition(2, tempEnd);
        if ((startPos - endPos).magnitude<1)
        {
            this.gameObject.SetActive(false);
        }
    }

}
