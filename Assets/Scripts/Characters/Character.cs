﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Essence.Spells;
using Essence.Constants;

namespace Essence.Characters
{
	public class Character : MonoBehaviour
	{
        public string playerNum;
        public SpriteRenderer sprite;

		public float health;
		private float maxHealth;

		private int fireMana;
		private int maxFireMana;
		private int waterMana;
		private int maxWaterMana;
		private int windMana;
		private int maxWindMana;
		private int earthMana;
		private int maxEarthMana;

        public float maxSpeed;
        public float speed;
        

        private int ticks = 0;

        public List<StatusEffect> statusEffects = new List<StatusEffect>();

        public Spell[] spells = new Spell[4];

		public Slider healthSlider;
        public Text fireUI;
        public Text waterUI;
        public Text airUI;
        public Text earthUI;



		// Use this for initialization
		public virtual void Start ()    
		{
			maxHealth = 100;
			health = maxHealth;
			healthSlider.maxValue = maxHealth;

            maxSpeed = 1;
            speed = 1;

            maxFireMana = 10;
            maxWaterMana = 10;
            maxWindMana = 10;
            maxEarthMana = 10;

            fireMana = 10;
            waterMana = 1;
            windMana = 10;
            earthMana = 10;
            
            spells[0] = new Fireball();
            spells[1] = new Stream();
            spells[2] = new Gust();
            spells[3] = new Teleport();

            UpdateAllManaUI();

            switch (playerNum)
            {
                case "P1":
                    sprite.color = PlayerData.PlayerOneColor;
                    break;
                case "P2":
                    sprite.color = PlayerData.PlayerTwoColor;
                    break;
                case "P3":
                    sprite.color = PlayerData.PlayerThreeColor;
                    break;
                case "P4":
                    sprite.color = PlayerData.PlayerFourColor;
                    break;
                default:
                    break;
            }
		}
	
		// Update is called once per frame
		public virtual void Update ()
		{
            ProcessStatusEffects();

			healthSlider.value = health;
            //fireUI.text = fireMana + " / " + maxFireMana;
            //waterUI.text = waterMana + " / " + maxWaterMana;
            //earthUI.text = earthMana + " / " + maxEarthMana;
            //airUI.text = windMana + " / " + maxWindMana;

            if (health <= 0) 
			{
				Destroy (this.gameObject);
			}

            if (ticks % 300 == 0)
            {
                //if (fireMana < maxFireMana) fireMana++;
            }
            ticks++;
		}

        private void ProcessStatusEffects()
        {
            List<StatusEffect> toRemove = new List<StatusEffect>();
            foreach (StatusEffect statusEffect in statusEffects)
            {
                statusEffect.Affect(this);
                if (statusEffect.duration == 0)
                {
                    toRemove.Add(statusEffect);
                }
            }
            foreach (StatusEffect effect in toRemove)
            {
                effect.Remove(this);
            }
        }

        public bool CastSpell(Spell spell)
        {
            if (spell.isAvailable)
            {
                if (fireMana >= spell.fireReq && waterMana >= spell.waterReq && earthMana >= spell.earthReq && windMana >= spell.windReq)
                {
                    fireMana -= spell.fireReq;
                    waterMana -= spell.waterReq;
                    earthMana -= spell.earthReq;
                    windMana -= spell.windReq;

                    spell.cooldown = spell.maxCooldown;
                    spell.isAvailable = false;

                    UpdateAllManaUI();

                    return true;
                }

            }
            return false;
        }

        public void CollectEssence(Element element, int quantity)
        {
            switch (element)
            {
                case Element.FIRE:
                    fireMana += quantity;
                    if (fireMana > maxFireMana) fireMana = maxFireMana;
                    UpdateManaUI(fireMana, "MF");
                    break;
                case Element.WATER:
                    waterMana += quantity;
                    if (waterMana > maxWaterMana) waterMana = maxWaterMana;
                    UpdateManaUI(waterMana, "MWa");
                    break;
                case Element.WIND:
                    windMana += quantity;
                    if (windMana > maxWindMana) windMana = maxWindMana;
                    UpdateManaUI(windMana, "MWi");
                    break;
                case Element.EARTH:
                    earthMana += quantity;
                    if (earthMana > maxEarthMana) earthMana = maxEarthMana;
                    UpdateManaUI(earthMana, "ME");
                    break;
            }
        }

        public StatusEffect GetEffect(StatusEffect type)
        {
            foreach (StatusEffect effect in statusEffects)
            {
                if (effect.GetType() == type.GetType())
                {
                    return effect;
                }
            }

            return null;
        }

        private void UpdateAllManaUI()
        {
            UpdateManaUI(fireMana, "MF");
            UpdateManaUI(waterMana, "MWa");
            UpdateManaUI(windMana, "MWi");
            UpdateManaUI(earthMana, "ME");
        }

        private void UpdateManaUI(int mana, string manaType)
        {
            int i = 1;
            while (i <= 10)
            {
                Image image = GameObject.Find(playerNum + manaType + i).GetComponent<Image>();
                if (mana > 0)
                {
                    image.sprite = Resources.Load<Sprite>("UI/UI " + manaType);
                }
                else
                {
                    image.sprite = Resources.Load<Sprite>("UI/UI Image Empty");
                }

                i++;
                mana--;
            }
        }
    }
}
