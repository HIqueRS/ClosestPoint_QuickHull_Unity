using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
public class QuickHull : MonoBehaviour
{
    [SerializeField] private GameObject[] _points;
    [SerializeField] private GameObject[] _convex;
    private LineRenderer _line;
    private int _a;
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();

        _convex = new GameObject[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _points = CreateArray(_points);
            OrderArray(_points);

            _convex = new GameObject[2];

            _convex[0] = _points[0];
            _convex[1] = _points[_points.Length - 1];

            (_convex,_a) = QHull(0, _convex.Length - 1, _points, _convex,true);
            (_convex, _a) = QHull(_convex.Length - 1, 0, _points, _convex,true);

            _line.positionCount = _convex.Length;

            for (int i = 0; i < _convex.Length; i++)
            {
                _line.SetPosition(i, _convex[i].transform.position);
            }
        }
    }

    private GameObject[] CreateArray(GameObject[] array)
    {
        array = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            array[i] = transform.GetChild(i).gameObject;
        }

        return array;
       
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

            if (min != i)
            {
                temp = arrayToOrder[i];
                arrayToOrder[i] = arrayToOrder[min];
                arrayToOrder[min] = temp;
            }
        }       
    }

    private int FindSide(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float val = (p3.y - p1.y) * (p2.x - p1.x) -
                    (p2.y - p1.y) * (p3.x - p1.x);

        if (val > 0)
            return 1;
        if (val < 0)
            return -1;
        return 0;
    }

    private float LineDist(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        return math.abs((p3.y - p1.y) * (p2.x - p1.x) -
                    (p2.y - p1.y) * (p3.x - p1.x));
    }

    private (GameObject[],int) QHull(int p1, int p2, GameObject[] points, GameObject[] convex, bool teste)
    {

        float maxDist;
        maxDist = 0;

        int pos,pos1;
        pos = 0;

        
       
        for (int i = 0; i < points.Length; i++)
        {
            if (FindSide(convex[p1].transform.position, convex[p2].transform.position, points[i].transform.position) > 0)
            {
                float dist = LineDist(convex[p1].transform.position, convex[p2].transform.position, points[i].transform.position);

                if (dist > maxDist)
                {
                    pos = i;
                    maxDist = dist;
                }
            }
        }

        


        if (maxDist != 0)
        {
            convex = PutOnMiddle(convex, p2, points[pos],teste);//posicao ta errada
            

            pos1 = p2;


            if (p2 - 1 < 0)
            {
                //do fim até p2
                (convex, pos1) = QHull(convex.Length - 1, p2, points, convex, teste);
                
            }
            else
            {
                //faz do p2-- ate p2
                (convex, pos1) = QHull(p2 - 1, p2, points, convex, teste);
            }


            if (p2 + 1 >= convex.Length)
            {
                //do p2 até 0
                (convex, pos1) = QHull(p2, 0, points, convex, teste);
                Debug.Log("a"); 
            }
            else
            {
                //do p2 até p2++
                (convex, pos1) = QHull(p2, p2 + 1, points, convex, teste);
                //Debug.Log("a");
            }

           



        }
       

        return (convex, p2);
    }

    private GameObject[] PutOnMiddle(GameObject[] array,int pos,GameObject insert,bool teste)// a posicao ta errada
    {
        GameObject[] arrayAux;

        arrayAux = new GameObject[array.Length + 1];

        if(teste)
        {
            arrayAux[pos] = insert;

            for (int i = 0; i < array.Length + 1; i++)
            {
                if (i < pos)
                {
                    arrayAux[i] = array[i];
                }
                else if (i > pos)
                {
                    arrayAux[i] = array[i - 1];
                }
            }
        }

        

        return arrayAux;
    }

}
