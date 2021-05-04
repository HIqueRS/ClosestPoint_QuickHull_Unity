using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

    private GameObject[] _points;


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            CreateArray();
        }
    }

    private void CreateArray()
    {
        _points = new GameObject[transform.childCount];
        
        for (int i = 0; i < transform.childCount ; i++)
        {
            _points[i] = transform.GetChild(i).gameObject;
        }

        OrderArray(_points);
    }

    private void OrderArray(GameObject[] arrayToOrder)
    {
        int min;
        GameObject temp;

        for (int i = 0; i < arrayToOrder.Length; i++)
        {
            min = i;
            for (int j = i + 1; j < arrayToOrder.Length; j++)
            {
                if (arrayToOrder[j].transform.position.x < arrayToOrder[min].transform.position.x)
                {
                    min = j;
                }
            }

            if(min != i)
            {
                temp = arrayToOrder[i];
                arrayToOrder[i] = arrayToOrder[min];
                arrayToOrder[min] = temp;
            }
        }
    }

    private float ClosestPoint(GameObject[] array)
    {
        float dist;
        dist = float.MaxValue;

        if(array.Length == 2)
        {
            dist = Vector3.Distance(array[0].transform.position, array[1].transform.position);
            return dist;
        }
        else if(array.Length == 1)
        {
            return dist;
        }

        //float midPoint;
        //midPoint = (array[0].transform.position.x + array[array.Length-1].transform.position.x);
        //if(midPoint != 0)
        //{
        //    midPoint = midPoint / 2;
        //}

        int midPoint;

        midPoint = array.Length / 2;

        GameObject[] array1;
        array1 = new GameObject[midPoint];
        GameObject[] array2;
        array2 = new GameObject[array.Length - midPoint];


        for (int i = 0; i < midPoint; i++)
        {
            array1[i] = array[i]; 
        }

        for (int i = midPoint; i < array.Length; i++)
        {
            array2[i-midPoint] = array[i];
        }

        float d1, d2, d3;

        d1 = ClosestPoint(array1);
        d2 = ClosestPoint(array2);
        d3 = Vector3.Distance(array1[array1.Length - 1].transform.position, array2[0].transform.position);

        if(d1 < d2)
        {
            if(d1 < d3)
            {
                dist = d1;
            }
            else
            {
                dist = d3;
            }
        }

        if (d2 < d1)
        {
            if (d2 < d3)
            {
                dist = d2;
            }
            else
            {
                dist = d3;
            }
        }

        return dist;
    }
}
