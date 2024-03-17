using KRC;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ArcRenderer : MonoBehaviour
{
    public GameObject arrowPregabs;
    public GameObject dotPrefbs;
    public int poolSize = 50;
    private List<GameObject> dotPool = new List<GameObject>();
    private GameObject arrowInstance;
    public float spacing = 50f;
    public float arrowAngleAdjustmen = 0;
    public int dotToSkip = 1;
    private Vector3 arrowDirection;
   
    
    void Start()
    {
        arrowInstance = Instantiate(arrowPregabs, transform);
        arrowInstance.transform.localPosition = Vector3.zero;
        InitializeDotPool(poolSize);

    }

    private void InitializeDotPool(int count)
    {
        for (int i = 0; i < count ; i++)
        {
            GameObject dot = Instantiate(dotPrefbs, Vector3.zero, Quaternion.identity, transform);
            dot.SetActive(false);
            dotPool.Add(dot);
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 startPos = transform.position;
        Vector3 midPoint = CalculateMidPoint(startPos, mousePos);

        UpdateArc(startPos, midPoint, mousePos);
        PositionAndRotateArrow(mousePos);
    }

    private void PositionAndRotateArrow(Vector3 position)
    {
        arrowInstance.transform.position = position;
        Vector3 direction = arrowDirection - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += arrowAngleAdjustmen;
        arrowInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    void UpdateArc(Vector3 start, Vector3 mid, Vector3 end)
    {
        int numDots = Mathf.CeilToInt(Vector3.Distance(start, end) / spacing);

        for (int i = 0; i < numDots && i < dotPool.Count; i++)
        {
            float t = i / (float)numDots;
            t = Mathf.Clamp(t, 0f, 1f); 

            Vector3 position = QuadraticBezierPoint(start, mid, end, t);

            if (i != numDots - dotToSkip)
            {
                dotPool[i].transform.position = position;
                dotPool[i].SetActive(true);
            }
            if (i == numDots - (dotToSkip + 1) && i - dotToSkip + 1 >= 0)
            {
                arrowDirection = dotPool[i].transform.position;
            }

        }

        
        for (int i = numDots - dotToSkip; i < dotPool.Count; i++)
        {
            if (i > 0)
            {
                dotPool[i].SetActive(false);
            }
        }
    }

    private Vector3 QuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * start;
        point += 2 * u * t * control;
        point += tt * end;
        return point;
    }

    Vector3 CalculateMidPoint(Vector3 start, Vector3 end)
    {
        Vector3 midPoint = (start + end) / 2;
        float arcHeight = Vector3.Distance(start, end) / 3f;
        midPoint.y += arcHeight;
        return midPoint;
    }
    

}

