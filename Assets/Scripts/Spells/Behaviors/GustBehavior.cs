using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Essence.Characters;
using System.Collections.Generic;

namespace Essence.Spells
{
    public class GustBehavior : SpellBehavior
    {
        private float angle;
        private float distance;
        private float speed;
        private float time;

        private Vector3 origin;

        // Use this for initialization
        void Start()
        {
            origin = caster.transform.position;

            damage = 5;

            duration = 100;

            // Set position/rotation
            this.gameObject.transform.position = caster.transform.position;
            this.gameObject.transform.eulerAngles = Spell.GetEulerAngles(caster);

            // Get area of affect
            ParticleSystem system = this.gameObject.GetComponent<ParticleSystem>();
            angle = system.shape.angle;
            speed = system.main.startSpeed.constant;
            time = system.main.startLifetime.constant;
            distance = speed * time;

            
            // Raycast targets
            List<Character> targets = Spell.AcquireTargetsCone(caster, angle, distance);

            
            foreach (Character target in targets)
            {
                Execute(target);
            }
        }

        // Update is called once per frame
        void Update()
        {
            duration--;
            if (duration == 0)
            {
                Destroy(this.gameObject);
            }
        }

        public override void Execute(Character target)
        {
            target.health -= this.damage;

            Vector3 direction = (target.transform.position - origin).normalized;

            float adjustedSpeed = speed / 60; //// FIGURE OUT WHAT WE NEED TO DO HERE to get the right vel, frames
            float velocity = distance * adjustedSpeed;
            int frames = (int)(distance / adjustedSpeed);
            int delayFrames = (int)(direction.magnitude / adjustedSpeed);

            target.GetComponent<Controller>().ApplyKnockback(direction, velocity, frames, delayFrames);


        }





    }

}

