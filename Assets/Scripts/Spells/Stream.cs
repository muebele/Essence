using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Essence.Characters;
using Essence.Constants;

namespace Essence.Spells
{
    public class Stream : ContinuousSpell
    {
        private float distance;
        private float angle;
        private float damage = 1;

        private int wetDuration = 180;
        private int slowDuration = 180;
        private float slowPercentage = 0.5F;
        

        public Stream()
        {

            spellName = "Stream";
            description = "Slow your enemies with water";

            fireReq = 0;
            waterReq = 1;
            windReq = 0;
            earthReq = 0;

            cooldown = 0;
            frequency = 60;

            GameObject stream = (GameObject)Resources.Load(spellName, typeof(GameObject));
            ParticleSystem system = stream.GetComponent<ParticleSystem>();
            angle = system.shape.angle;
            distance = system.main.startSpeed.constant * system.main.startLifetime.constant;
            
        }


        public override void Pulse(Character caster)
        {
            List<Character> targets = AcquireTargets(caster, angle, distance);
            
            foreach (Character target in targets)
            {
                Execute(target);
            }
        }

        private void Execute(Character target)
        {
            target.health -= this.damage;
            Wet wet = new Wet(wetDuration);
            Slow slow = new Slow(slowDuration, slowPercentage);

            wet.Apply(target);
            slow.Apply(target);
            
        }
    }
}

