using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Essence.Characters;

namespace Essence.Spells
{
    public class FireballBehavior : SpellBehavior
    {


        // Use this for initialization
        void Start()
        {
            damage = 10;

            duration = 60;

            movementRatio = .2F;
        }

        // Update is called once per frame
        void Update()
        {
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

