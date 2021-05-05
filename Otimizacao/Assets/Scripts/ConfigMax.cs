using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConfigMax 
{

    [SerializeField]
    public struct PairOfPoints
    {
        [SerializeField] public float distance;
        [SerializeField] public GameObject point1;
        [SerializeField] public GameObject point2;
    }
}
