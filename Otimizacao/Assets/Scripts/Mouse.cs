using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] private GameObject _root;
    [SerializeField] private GameObject _point;

    private Vector3 _spawnPosition;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _spawnPosition.z = 0;
            GameObject.Instantiate(_point, _spawnPosition, Quaternion.identity, _root.transform);
        }
    }
}
