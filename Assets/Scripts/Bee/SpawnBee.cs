using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class SpawnBee : MonoBehaviour
{

        [SerializeField] private Transform _respawnPoint; 
        [SerializeField] private GameObject _bee;
        [SerializeField] private Transform _containerBee;
    
        private void Awake()
        {
            Instantiate(_bee, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity,_containerBee);
            StartCoroutine(Respawn());
        }
    
        private IEnumerator Respawn()
        {
            while (true)
            {
                Random rndY = new Random();
                var valueY = rndY.Next(-2, 5);
                
                Random rnd = new Random();
                var value = rnd.Next(5, 30);
                yield return new WaitForSeconds(value);
                var position = _respawnPoint.position;
                Instantiate(_bee, new Vector3(position.x, position.y+ valueY, position.z), Quaternion.identity,_containerBee);
            }
        }
}
