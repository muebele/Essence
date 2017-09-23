using UnityEngine;
using System.Collections;

namespace Essence.Characters
{
	public class AI : Character
	{
		private Character enemy;
		// Use this for initialization
		public override void Start ()
		{
			base.Start ();
			GameObject kalladin = GameObject.FindWithTag ("Kalladin");
			enemy = kalladin.GetComponent<Character>();
		}
		
		// Update is called once per frame
		public override void Update ()
		{
			base.Update ();

			//if (hasTurn) 
			//{
			//	this.Attack(enemy);
			//	this.hasTurn = false;
			//}
		}
	}
}

