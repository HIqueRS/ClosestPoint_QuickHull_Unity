using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConfigMax;

public class Root : MonoBehaviour
{

    private GameObject[] _points;
    [SerializeField] private PairOfPoints _pop;


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

            _pop = ClosestPoint(_points);
            _pop.point1.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            _pop.point2.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
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

    private PairOfPoints ClosestPoint(GameObject[] array)
    {
        PairOfPoints pmin;
        float dist;
        dist = float.MaxValue;

        pmin.distance = dist;
        pmin.point1 = null;
        pmin.point2 = null;
       

        if(array.Length == 2)
        {
            dist = Vector3.Distance(array[0].transform.position, array[1].transform.position);

            pmin.point1 = array[0];
            pmin.point2 = array[1];
            pmin.distance = dist;
            return pmin;
        }
        else if(array.Length <= 1)
        {
            pmin.distance = dist;

            return pmin;
        }

        float midPoint;
        midPoint = (array[0].transform.position.x + array[array.Length - 1].transform.position.x);
        if (midPoint != 0)
        {
            midPoint = midPoint / 2;
        }

        GameObject[] arrayAux1;

        arrayAux1 = new GameObject[array.Length];
        GameObject[] arrayAux2;

        arrayAux2 = new GameObject[array.Length];

        int aux1, aux2;

        aux1 = aux2 = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if(array[i].transform.position.x < midPoint)
            {
                arrayAux1[aux1] = array[i];
                aux1 += 1;
            }
            else
            {
                arrayAux2[aux2] = array[i];
                aux2 += 1;
            }
        }

        GameObject[] array1;
        array1 = new GameObject[aux1];
        GameObject[] array2;
        array2 = new GameObject[aux2];

        for (int i = 0; i < aux1; i++)
        {
            array1[i] = arrayAux1[i];
        }

        for (int i = 0; i < aux2; i++)
        {
            array2[i] = arrayAux2[i];
        }

        //int midPoint;

        //midPoint = array.Length / 2;

        //GameObject[] array1;
        //array1 = new GameObject[midPoint];
        //GameObject[] array2;
        //array2 = new GameObject[array.Length - midPoint];


        //for (int i = 0; i < midPoint; i++)
        //{
        //    array1[i] = array[i]; 
        //}

        //for (int i = midPoint; i < array.Length; i++)
        //{
        //    array2[i-midPoint] = array[i];
        //}

        PairOfPoints d1, d2, d3;

        d1 = ClosestPoint(array1);
        d2 = ClosestPoint(array2);
        d3.distance = Vector3.Distance(array1[array1.Length - 1].transform.position, array2[0].transform.position);
        d3.point1 = array1[array1.Length - 1];
        d3.point2 = array2[0];

        if (d1.distance < d2.distance)
        {
            if (d1.distance < d3.distance)
            {
                pmin = d1;
            }
            else
            {
                pmin = d3;
            }
        }

        if (d2.distance < d1.distance)
        {
            if (d2.distance < d3.distance)
            {
                pmin = d2;
            }
            else
            {
                pmin = d3;
            }
        }

        

        return pmin;
    }
}
