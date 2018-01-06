using UnityEngine;
using System.Collections;
using Essence.Spells;
using Essence.Constants;

namespace Essence.Characters
{
    public class Controller : MonoBehaviour
    {

        private Character character;

        public Vector3 directionFacing;
        public Vector3 directionPointing;

        public Rigidbody2D rigidBody;

        private Vector3 movement;
        
        private int dashFrames = 0;
        private float dashScale = 4;

        private bool underKnockback = false;
        private int knockbackFrames = 0;
        private Vector3 knockbackDirection;

        Spell spell = null;


		// Use this for initialization
		void Start ()
		{
			movement = new Vector3 ();
            directionFacing = new Vector3();
            directionPointing = new Vector3();

            character = this.gameObject.GetComponent<Character>();
		}
		
		// Update is called once per frame
		void Update ()
		{
			ProcessMovement ();
			ProcessCommand ();

            ManageSpells();
		}

		private void ProcessCommand(){
            spell = null;

			if (Input.GetButtonDown("Special")) 
			{
                dashFrames = 10;
			}
            
			if (Input.GetButtonDown("Spell1")) 
			{
                spell = character.spells[0];
                if (Input.GetButton("Trigger")) spell = character.spells[4];
			}
            else if (Input.GetButtonDown("Spell2"))
            {
                spell = character.spells[1];
                if (Input.GetButton("Trigger")) spell = character.spells[5];
            }
            else if (Input.GetButtonDown("Spell3"))
            {
                spell = character.spells[2];
                if (Input.GetButton("Trigger")) spell = character.spells[6];
            }
            else if (Input.GetButtonDown("Spell4"))
            {
                spell = character.spells[3];
                if (Input.GetButton("Trigger")) spell = character.spells[7];
            }

            if (spell != null)
            {
                if (character.CastSpell(spell))
                {
                    if (spell is InstantCastSpell)
                    {
                        
                        InstantCastSpell instantSpell = spell as InstantCastSpell;
                        instantSpell.Affect(character, null);
                    }

                    else if (spell is ProjectileSpell)
                    {
                        GameObject spellObject = (GameObject)Resources.Load(spell.spellName, typeof(GameObject));
                        spellObject.transform.position = this.transform.position;

                        if (directionPointing.magnitude > 0)    
                        {
                            spellObject.transform.GetComponent<SpellBehavior>().movement = directionPointing;
                        }
                        else
                        {
                            spellObject.transform.GetComponent<SpellBehavior>().movement = directionFacing;
                        }


                        spellObject.transform.GetComponent<SpellBehavior>().caster = this.character;

                        Instantiate(spellObject);
                    }
                    
                    else if (spell is ContinuousSpell)
                    {
                        ContinuousSpell continuousSpell = spell as ContinuousSpell;

                        continuousSpell.Toggle(this.character);
                    }
                    
                }
            }
            
        }

		private void ProcessMovement(){
            if (!underKnockback)
            {
                movement.y = Input.GetAxis("Vertical");
                movement.x = Input.GetAxis("Horizontal");


                if (dashFrames > 0)
                {
                    movement = movement * dashScale;

                    dashFrames -= 1;
                }

                directionPointing.y = Input.GetAxis("VerticalAim");
                //Debug.Log(Input.GetAxis("VerticalAim"));
                directionPointing.x = Input.GetAxis("HorizontalAim");
                directionPointing.Normalize();

                rigidBody.velocity = movement * GlobalConstants.Movement_Scale * character.speed;


                if (movement.normalized.magnitude > 0)
                {
                    directionFacing = movement.normalized;
                }

                if (directionPointing.magnitude == 0)
                {
                    directionPointing = directionFacing;
                }
            }
            else
            {
                Debug.Log(knockbackDirection);
                knockbackFrames--;
                rigidBody.velocity = knockbackDirection;

                if (knockbackFrames == 0) underKnockback = false;
            }
		}

        public void ApplyKnockback(Vector3 targetLocation)
        {
            Vector3 difference = targetLocation - rigidBody.transform.position;

            knockbackDirection = difference;
            knockbackFrames = 30;

            underKnockback = true;
        }

        private void ManageSpells()
        {
            foreach (Spell spell in character.spells)
            {
                spell.DecreaseCooldown();

                if (spell is ContinuousSpell)
                {
                    ContinuousSpell continuousSpell = spell as ContinuousSpell;

                    if (continuousSpell.toggled)
                    {
                        continuousSpell.Update(character);

                        
                    }

                    
                }
            }
        }

        private void CheckBounds()
        {
            
        }

        private Vector3 colMovement = new Vector3(0,0,0);

        void OnCollisionStay2D(Collision2D col)
        {
            //if (movement.magnitude > colMovement.magnitude) colMovement = -movement;
            //this.transform.Translate(colMovement * movementScale);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            /*
            Collider2D collider = this.GetComponent<Collider2D>();

            colMovement = -movement;
            this.transform.Translate(colMovement * movementScale);

            Debug.Log(colMovement);

            
            Vector3 pt = collider.bounds.ClosestPoint(col.collider.bounds.center);

            Vector3 direction = (this.transform.position - pt).normalized;

            Vector3 position = this.transform.position;

            while (col.collider.bounds.Contains(position))
            {
                position = position + direction * .05F;
            }
            
            this.transform.position = position;
            */

        }
    }
}

