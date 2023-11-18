using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrolling : MonoBehaviour
{
    [SerializeField] private GameObject _background;
    private float _xSpeed, _ySpeed;

    private void Start()
    {
        _xSpeed = .5f;
    }

    void Update()
    {
        _background.transform.position += new Vector3(_xSpeed, 0, 0) * Time.deltaTime;

        if (_background.transform.position.x > 10 )
            _background.transform.position = new Vector3(-10, 0, 0); 
    }
}
