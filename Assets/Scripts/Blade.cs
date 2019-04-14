using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitNinja
{
    public class Blade : MonoBehaviour
    {
        public float cutVelocity = .001f;
        public GameObject bladeTrailPrefab;

        private GameObject currentBladeTrail;
        private bool isCutting = false;
        private Vector2 direction;
        private Vector2 previousPosition;
        private Rigidbody2D rigid;
        private CircleCollider2D circleCollider;

        void Start()
        {
            // Get components from GameObject
            rigid = GetComponent<Rigidbody2D>();
            circleCollider = GetComponent<CircleCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
            // Is the mouse down?
            if (Input.GetMouseButtonDown(0))
            {
                StartCutting();
            }
            // Otherwise, is the mouse up?
            else if (Input.GetMouseButtonUp(0))
            {
                StopCutting();
            }

            if (isCutting)
            {
                UpdateCut();
            }
        }

        void UpdateCut()
        {
            // Get the new mouse position
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rigid.position = newPosition; // Set rigid position to mouse pos

            // Update BladeTrail
            currentBladeTrail.transform.position = newPosition;

            // Get direction from previous position
            direction = newPosition - previousPosition;

            // Get velocity from direction
            float velocity = direction.magnitude * Time.deltaTime;
            // If velocity has reached cutting velocity
            if (velocity > cutVelocity)
            {
                // Enable the collider to hit something
                circleCollider.enabled = true;
            }
            else // Otherwise...
            {
                // Disable the collider to not hit anything
                circleCollider.enabled = false;
            }

            // Update previous position for next frame
            previousPosition = newPosition;
        }

        void StartCutting()
        {
            isCutting = true;
            // Create a new blade trail
            currentBladeTrail = Instantiate(bladeTrailPrefab, transform);
        }

        void StopCutting()
        {
            isCutting = false;
            // Destroy blade trail
            Destroy(currentBladeTrail);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            // Did we hit a fruit?
            Fruit fruit = col.GetComponent<Fruit>();
            if (fruit)
            {
                // Cut the fruit in the direction of the swipe
                fruit.Cut(direction);
            }
        }
    }
}