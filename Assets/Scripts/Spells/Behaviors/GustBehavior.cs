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

        private Vector3 origin;

        // Use this for initialization
        void Start()
        {
            origin = caster.transform.position;

            damage = 2;

            duration = 100;

            // Set position/rotation
            this.gameObject.transform.position = caster.transform.position;
            this.gameObject.transform.eulerAngles = Spell.GetEulerAngles(caster);

            // Get area of affect
            ParticleSystem system = this.gameObject.GetComponent<ParticleSystem>();
            angle = system.shape.angle;
            distance = system.main.startSpeed.constant * system.main.startLifetime.constant;

            
            // Raycast targets
            List<Character> targets = Spell.AcquireTargetsLine(caster, angle, distance);

            
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

            Vector3 difference = target.transform.position - origin;
            Vector3 final = (difference.normalized * distance) + origin;
            difference = final - target.transform.position;

            target.GetComponent<Controller>().ApplyKnockback(final);


        }





    }

}

