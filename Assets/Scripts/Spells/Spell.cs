using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Essence.Characters;

namespace Essence.Spells
{
	public abstract class Spell
	{
        public GameObject spellObject;

        public string spellName;
        public string description;

		public int fireReq;
        public int waterReq;
        public int windReq;
        public int earthReq;

        public int maxCooldown;
        public int cooldown = 0;
        public bool isAvailable = true;

        public void DecreaseCooldown()
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            else
            {
                isAvailable = true;
            }
        }

        public static List<Character> AcquireTargetsCone(Character caster, float angle, float distance)
        {
            // Set up vectors
            Vector3 position = caster.transform.position;
            Vector3 direction = caster.gameObject.GetComponent<Controller>().directionPointing;
            if (direction.magnitude == 0) direction = caster.gameObject.GetComponent<Controller>().directionFacing;

            // Initialize lists
            RaycastHit2D[] hits = new RaycastHit2D[8];
            List<Character> targets = new List<Character>();

            // Fire raycast at every angle in cone
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

            return targets;
        }

        public static List<Character> AcquireTargetsLine(Character caster, float radius, float distance)
        {
            // Set up vectors
            Vector3 position = caster.transform.position;
            Vector3 direction = caster.gameObject.GetComponent<Controller>().directionPointing;
            if (direction.magnitude == 0) direction = caster.gameObject.GetComponent<Controller>().directionFacing;

            // Initialize lists
            RaycastHit2D[] hits = new RaycastHit2D[8];
            RaycastHit2D[] wallHits = new RaycastHit2D[8];
            List<Character> targets = new List<Character>();

           
            
            // Get all objects hit
            Physics2D.CircleCastNonAlloc(position, radius, direction, hits, distance, 1 << LayerMask.NameToLayer("Player"));

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    // Parse for player objects that aren't the caster
                    Character target = hit.collider.gameObject.GetComponent<Character>();
                    if (target != null && target != caster)
                    {
                        float colDistance = (target.transform.position - position).magnitude;
                        // Check for wall
                        RaycastHit2D wallHit = Physics2D.CircleCast(position, radius, direction, colDistance, 1 << LayerMask.NameToLayer("Walls"));
                        if (wallHit.collider == null)
                        {
                            
                            if (!targets.Contains(target))
                            {
                                // Got em
                                targets.Add(target);

                            }
                        }
                    }

                }
            }
            
                

            return targets;
        }


        public static Vector3 GetEulerAngles(Character character)
        {
            Vector3 direction = character.gameObject.GetComponent<Controller>().directionPointing;
            float a = Vector3.Angle(direction, new Vector3(1, 0));
            if (character.gameObject.GetComponent<Controller>().directionPointing.y > 0) a = -a;
            return new Vector3(a, 90, 0);
        }
	}
}
