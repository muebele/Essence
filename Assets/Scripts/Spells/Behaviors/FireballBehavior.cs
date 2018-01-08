using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Essence.Characters;

namespace Essence.Spells
{
    public class FireballBehavior : SpellBehavior
    {
        float rotation;

        // Use this for initialization
        void Start()
        {
            damage = 10;

            duration = 60;

            movementRatio = .2F;

            // Set position/rotation
            //this.gameObject.transform.position = caster.transform.position;
            //this.gameObject.transform.eulerAngles = Spell.GetEulerAngles(caster);

            float z = Mathf.Atan2(movement.y, movement.x);

            if (z < 0) z = (2 * Mathf.PI + z);
            z = (z / Mathf.PI) * 180;
            rotation = z + 90;

            this.gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);
        }
        
        // Update is called once per frame
        void Update()
        {
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            this.gameObject.transform.Translate(movement * movementRatio);
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);

            duration--;
            if (duration == 0)
            {
                //Destroy(this.gameObject);
            }
        }

        public override void Execute(Character target)
        {
            target.health -= this.damage;
        }


        


    }

}

