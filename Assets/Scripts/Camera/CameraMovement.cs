using UnityEngine;
using System.Collections;

namespace Essence.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        Vector3 movement;
        private float movementScale = 0.05F;

        // Use this for initialization
        void Start()
        {
            movement = new Vector3();
        }

        // Update is called once per frame
        void Update()
        {
            movement.y = Input.GetAxis("Vertical");
            movement.x = Input.GetAxis("Horizontal");

            this.transform.Translate(movement * movementScale);
        }
    }
}


