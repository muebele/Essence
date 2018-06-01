using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Essence.Characters;

namespace Essence.Spells
{
    public class StreamBehavior : SpellBehavior
    {
        private float distance;
        private float radius;

        // Use this for initialization
        void Start()
        {
            damage = 10;

            duration = 60;
            distance = 3;
            radius = 1;

            movementRatio = .2F;
        }

        // Update is called once per frame
        void Update()
        {
            

            RaycastHit2D[] hits = new RaycastHit2D[8];
            Vector3 position = this.transform.position;
            Vector3 direction = this.caster.gameObject.GetComponent<Controller>().directionFacing;

            Physics2D.CircleCastNonAlloc(position, radius, direction, hits, distance, 1 << LayerMask.NameToLayer("Player"));

            foreach (RaycastHit2D hit in hits)
            {
                Character target = hit.collider.gameObject.GetComponent<Character>();
                if (target != null)
                {
                    if(target != caster)
                    {
                        Execute(target);
                    }
                }
            }

            this.gameObject.transform.Translate(movement * movementRatio);
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

