using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Essence.Characters;
using System.Threading;


namespace Essence.Spells
{
    public class IceshardsBehavior : SpellBehavior
    {
        private Vector3 startPosition;
        private int numShards = 3;
        private float shardGap = 0.05F;

        // Use this for initialization
        void Start()
        {
            damage = 5;

            duration = 60;

            movementRatio = .2F;
            
            startPosition = this.transform.position;
            StartCoroutine(GenerateShards(numShards));
            
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

        IEnumerator GenerateShards(int numShards)
        {
            yield return new WaitForSeconds(shardGap);
            if (numShards > 0) StartCoroutine(GenerateShards(numShards-1));

            GameObject iceShard = (GameObject)Resources.Load("Iceshard", typeof(GameObject));
            iceShard.transform.position = this.startPosition;

            float angleRotation = Random.Range(-30, 30);
            Vector3 spellMovement = new Vector3();
            spellMovement = Quaternion.Euler(0, 0, angleRotation) * this.movement;
            spellMovement.Normalize();

            iceShard.transform.GetComponent<SpellBehavior>().movement = spellMovement;
            iceShard.transform.GetComponent<SpellBehavior>().caster = this.caster;
            Instantiate(iceShard);
        }
        


        public override void Execute(Character target)
        {
            target.health -= this.damage;
        }



    }

}

