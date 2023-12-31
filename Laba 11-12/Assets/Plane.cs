﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CubePrefab;
    public GameObject SpherePrefab;
    public float SpawnHeight = 5f;

    private Vector3 _minBounds;
    private Vector3 _maxBounds;
    private float _tiltAngle;

    private void Start()
    {
        var component = GetComponent<Renderer>();
        _minBounds = component.bounds.min;
        _maxBounds = component.bounds.max;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnObjectAtRandomPosition(CubePrefab);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObjectAtRandomPosition(SpherePrefab);
        }

        if (Input.GetKey(KeyCode.W))
        {
            _tiltAngle += Time.deltaTime * 30f;
            transform.rotation = Quaternion.Euler(0f, 0f, _tiltAngle);
        }
    }

    private void SpawnObjectAtRandomPosition(GameObject prefab)
    {
        var spawnPosition = new Vector3(Random.Range(_minBounds.x, _maxBounds.x), SpawnHeight, Random.Range(_minBounds.z, _maxBounds.z));
        var cube = Instantiate(prefab, spawnPosition, Quaternion.identity);
        cube.GetComponent<Rigidbody>().useGravity = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sphere")
        {
            Destroy(collision.gameObject);
        }
    }
}
