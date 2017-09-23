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
            RaycastHit2D[] hits = new RaycastHit2D[8];
            Vector3 position = caster.transform.position;
            Vector3 direction = caster.gameObject.GetComponent<Controller>().directionFacing;

            //Physics2D.CircleCastNonAlloc(position, radius, direction, hits, distance, 1 << LayerMask.NameToLayer("Player"));
            //Physics2D.RaycastNonAlloc(position, direction, hits, distance, 1 << LayerMask.NameToLayer("Player"));
            List<Character> targets = new List<Character>();

            for (int i = (int)-angle; i < angle; i++)
            {
                Vector3 dir = (Quaternion.Euler(0, 0, i) * direction).normalized;
                RaycastHit2D wallHit = Physics2D.Raycast(position, dir, distance, 1 << LayerMask.NameToLayer("Walls"));
                if (wallHit.collider == null)
                {
                    hits = Physics2D.RaycastAll(position, dir, distance, 1 << LayerMask.NameToLayer("Player"));
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider != null)
                        {
                            Character target = hit.collider.gameObject.GetComponent<Character>();
                            if (target != null && target != caster)
                            {
                                if (!targets.Contains(target))
                                {
                                    targets.Add(target);
                                }
                            }

                        }
                    }
                }
            }
            
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

