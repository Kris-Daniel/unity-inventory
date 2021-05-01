using System;
using UnityEngine;

namespace Inventory
{
	[Serializable]
	public class Stack
	{
		[SerializeField] Resource resource;
		[SerializeField] int amount;
		[SerializeField] [Min(1)] int maxAmount = 1;

		public Resource Resource => resource;

		public int Amount
		{
			get => amount;
			set => amount = Mathf.Clamp(value, 0, maxAmount);
		}
		public int MaxAmount
		{
			get => maxAmount;
			set
			{
				if (value < maxAmount && amount > value)
				{
					amount = value;
				}
				maxAmount = value;
			}
		}

		public bool IsFull => amount == maxAmount;
		public float Fill => amount / maxAmount;

		public static Stack Create(Resource resource, int amount, int maxCount)
		{
			return new Stack {resource = resource, maxAmount = maxCount, amount = amount};
		}
	}
}