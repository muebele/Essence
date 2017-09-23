using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Essence.Characters;

namespace Essence.Spells
{
    public class TrapBehavior : SpellBehavior
    {
        private Immobilize effect;

        // Use this for initialization
        void Start()
        {
            damage = 5;

            duration = 1000;

            movementRatio = 0;

            effect = new Immobilize(120);
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
            effect.Apply(target);
        }
    }

}

