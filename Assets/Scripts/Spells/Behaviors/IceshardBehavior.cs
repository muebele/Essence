using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Essence.Characters;


namespace Essence.Spells
{
    public class IceshardBehavior : SpellBehavior
    {

        // Use this for initialization
        void Start()
        {
            Debug.Log("iceshard construct");

            damage = 12;

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

