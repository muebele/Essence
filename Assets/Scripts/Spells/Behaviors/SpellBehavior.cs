using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Essence.Characters;

namespace Essence.Spells
{
    public abstract class SpellBehavior : MonoBehaviour
    {
        public Character caster;

        protected int duration;

        protected float damage;
        protected List<StatusEffect> statusEffects;

        public Vector3 movement;
        protected float movementRatio;


        public abstract void Execute(Character target);

        void OnTriggerEnter2D(Collider2D col)
        {
            GameObject obj = col.gameObject;
            Debug.Log(obj.tag);
            Debug.Log(obj.layer);
            Debug.Log(obj.name);

            Character character = (Character)obj.GetComponent(typeof(Character));
            if (character != null)
            {
                if (character != this.caster)
                {
                    Debug.Log("execute");
                    this.Execute(character);
                    Destroy(this.gameObject);
                }
            }
            else if (obj.tag == "collisionAll")
            {
                Destroy(this.gameObject);
            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log(col.gameObject);
        }
    }
}
