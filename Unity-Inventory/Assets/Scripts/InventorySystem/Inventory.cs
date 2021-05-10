using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class Inventory : MonoBehaviour
	{
		[SerializeField] List<Stack> stacks;
		
		public Stack this[int i] => stacks[i];
		public int StacksCount => stacks.Count;
		
		public Action<Resource, int> OnChange { get; set; }

		void Awake()
		{
			for (int i = stacks.Count - 1; i >= 0; i--)
			{
				if (stacks[i].Resource == null)
				{
					stacks.Remove(stacks[i]);
				}
				else
				{
					stacks[i].Inventory = this;
				}
			}
		}

		public void AddStack(Stack stackToAdd)
		{
			if (stackToAdd.Resource != null)
			{
				stacks.Add(stackToAdd);
				stackToAdd.Inventory = this;
			}
		}

		public static void TransferResources(Stack stackToGet, Stack stackToTransfer, int amount)
		{
			amount = amount > stackToTransfer.Amount ? stackToTransfer.Amount : amount;
			int oldAmount = stackToGet.Amount;
			stackToGet.Amount -= amount;
			int difference = oldAmount - stackToGet.Amount;
			stackToTransfer.Amount += difference;
		}
	}
}