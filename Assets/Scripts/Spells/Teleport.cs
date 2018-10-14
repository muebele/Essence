using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Essence.Characters;



namespace Essence.Spells
{
    public class Teleport : InstantCastSpell
    {
        private int distanceScale = 3;

        public Teleport()
        {

            spellName = "Teleport";
            description = "";

            fireReq = 0;
            waterReq = 0;
            windReq = 0;
            earthReq = 0;

            cooldown = 600;
        }

        public override void Affect(Character caster, Character targets)
        {


            Controller casterController = caster.gameObject.GetComponent<Controller>();

            Vector3 teleportation = casterController.directionFacing * distanceScale;

            Vector3 newPos = caster.gameObject.transform.position + teleportation;

            Collider2D collider = caster.gameObject.GetComponent<Collider2D>();
            GameObject collisionAll = GameObject.FindGameObjectWithTag("collisionAll");
            GameObject collisionPlayer = GameObject.FindGameObjectWithTag("collisionPlayer");

            Collider2D[] allColliders = collisionAll.GetComponents<Collider2D>();
            Collider2D[] playerColliders = collisionPlayer.GetComponents<Collider2D>();

            

            while (true)
            {
                Collider2D boundary = null;

                foreach (Collider2D col in allColliders)
                {
                    if (col.bounds.Contains(newPos))
                    {
                        boundary = col;
                    }
                }
                foreach (Collider2D col in playerColliders)
                {
                    if (col.bounds.Contains(newPos))
                    {
                        boundary = col;
                    }
                }

                if (boundary != null)
                {
                    Vector3 direction = boundary.bounds.center - newPos;

                    direction = -(direction.normalized);


                    while (boundary.bounds.Contains(newPos))
                    {
                        newPos = newPos + direction * .04F;
                    }
                    newPos = newPos + direction * .01F;
                }
                else
                {
                    break;
                }
            }
            
            
            
            
            
            caster.gameObject.transform.position = newPos;
            
        }
    }
}

