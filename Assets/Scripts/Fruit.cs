using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitNinja
{
    public class Fruit : MonoBehaviour
    {
        public GameObject fruitSlicedPrefab;
        public float startForce = 15f;

        private Rigidbody2D rigid;

        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            rigid.AddForce(transform.up * startForce, ForceMode2D.Impulse);
        }

        public void Cut(Vector3 direction)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject slicedFruit = Instantiate(fruitSlicedPrefab, transform.position, rotation);
            Destroy(slicedFruit, 3f);
            Destroy(gameObject);
        }
    }
}