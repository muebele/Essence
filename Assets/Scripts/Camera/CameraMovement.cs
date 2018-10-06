using UnityEngine;
using System.Collections;
using Essence.Constants;

namespace Essence.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        Vector3 movement;
        private float movementScale = 0.05F;

        // For maintaining current framerate - https://answers.unity.com/questions/46745/how-do-i-find-the-frames-per-second-of-my-game.html
        int m_frameCounter = 0;
        float m_timeCounter = 0.0f;
        float m_lastFramerate = 0.0f;
        public float m_refreshTime = 0.5f;

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


            // Maintain framerate
            if (m_timeCounter < m_refreshTime)
            {
                m_timeCounter += Time.deltaTime;
                m_frameCounter++;
            }
            else
            {
                //This code will break if you set your m_refreshTime to 0, which makes no sense.
                m_lastFramerate = (float)m_frameCounter / m_timeCounter;
                m_frameCounter = 0;
                m_timeCounter = 0.0f;
            }

            GlobalConstants.Frame_Rate = m_lastFramerate;
        }
    }
}


