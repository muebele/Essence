using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Essence.Characters;
using System.Collections.Generic;
using Essence.Constants;

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

            damage = 8;

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

            // Find distance to the target
            Vector3 OriginToTarget = target.transform.position - origin;
            Vector3 direction = OriginToTarget.normalized;

            float adjustedDistance = distance - OriginToTarget.magnitude;
            float distanceRatio = adjustedDistance / distance;

            // Find when the knockback should start and how many frames it should be active for
            int totalFrames = (int)(time * GlobalConstants.Frame_Rate);
            int activeFrames = (int)(distanceRatio * totalFrames);
            int delayFrames = totalFrames - activeFrames;;


            target.GetComponent<Controller>().ApplyKnockback(direction, speed, activeFrames, delayFrames);
        }





    }

}

