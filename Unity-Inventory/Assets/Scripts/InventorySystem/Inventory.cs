using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class Inventory : MonoBehaviour
	{
		[SerializeField] List<Stack> stacks;
		public List<Stack> Stacks => stacks;

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

		public Stack GetStack(Resource resource)
		{
			Stack result = null;
			
			foreach (var stack in stacks)
			{
				if (stack.Resource == resource)
				{
					result = stack;
					break;
				}
			}

			return result;
		}

		public static void TransferResources(Stack stackToTake, Stack stackToTransfer, int amount)
		{
			amount = amount > stackToTake.Amount ? stackToTake.Amount : amount;
			int oldAmount = stackToTake.Amount;
			stackToTake.Amount -= amount;
			int difference = oldAmount - stackToTake.Amount;
			stackToTransfer.Amount += difference;
		}
	}
}