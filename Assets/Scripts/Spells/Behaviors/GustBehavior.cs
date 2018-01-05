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
            damage = 2;

            duration = 20;


            Gust gust = new Gust();
            GameObject stream = (GameObject)Resources.Load(gust.spellName, typeof(GameObject));
            ParticleSystem system = stream.GetComponent<ParticleSystem>();


            
            system.Play();

            angle = system.shape.angle;
            distance = system.main.startSpeed.constant * system.main.startLifetime.constant;

            

            List<Character> targets = Spell.AcquireTargets(caster, angle, distance);

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


        }





    }

}

