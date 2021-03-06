using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class Inventory : MonoBehaviour
	{
		[SerializeField] List<Stack> stacks;
		public List<Stack> Stacks => stacks;

		public delegate void InventoryChangeDelegate(Resource resource, int difference);
		public InventoryChangeDelegate OnChange { get; set; } = delegate { };

		readonly Dictionary<Resource, Stack> stacksDictionary = new Dictionary<Resource, Stack>();

		void Awake()
		{
			for (int i = stacks.Count - 1; i >= 0; i--)
			{
				if (stacks[i].Resource == null || stacksDictionary.ContainsKey(stacks[i].Resource))
				{
					stacks.Remove(stacks[i]);
				}
				else
				{
					stacksDictionary.Add(stacks[i].Resource, stacks[i]);
					stacks[i].Inventory = this;
				}
			}
#if UNITY_EDITOR
			OnChange += (resource, difference) =>
			{
				CloneStacks();
			};
#endif
		}

		public void AddStack(Stack stackToAdd)
		{
			if (stackToAdd != null && stackToAdd.Resource != null)
			{
				stacks.Add(stackToAdd);
				stacksDictionary.Add(stackToAdd.Resource, stackToAdd);
				stackToAdd.Inventory = this;
			}
		}

		public Stack GetStack(Resource resource)
		{
			Stack result = null;

			if (stacksDictionary.ContainsKey(resource))
			{
				result = stacksDictionary[resource];
			}

			return result;
		}

		public static void TransferResources(Stack stackToTake, Stack stackToTransfer, int amount)
		{
			if (stackToTake != null && stackToTransfer != null)
			{
				amount = amount > stackToTake.Amount ? stackToTake.Amount : amount;
				int oldAmount = stackToTake.Amount;
				int maxAmountToAdd = stackToTransfer.MaxAmount - stackToTransfer.Amount;
				amount = amount <= maxAmountToAdd ? amount : maxAmountToAdd;
				stackToTake.Amount -= amount;
				int difference = oldAmount - stackToTake.Amount;
				stackToTransfer.Amount += difference;
			}
		}

#if UNITY_EDITOR
		readonly List<Stack> stacksClone = new List<Stack>();
		
		void OnValidate()
		{
			foreach (var stack in stacks)
			{
				if (stack.MaxAmount < 1)
				{
					stack.MaxAmount = 1;
				}
				
				if (stack.Amount < 0)
				{
					stack.Amount = 0;
				}
				
				else if (stack.Amount > stack.MaxAmount)
				{
					stack.Amount = stack.MaxAmount;
				}
			}
			
			if (stacksClone.Count != stacks.Count)
			{
				CloneStacks();
			}
			else
			{
				for (int i = 0; i < stacks.Count; i++)
				{
					if (stacks[i].Resource != stacksClone[i].Resource || stacks[i].MaxAmount != stacksClone[i].MaxAmount)
					{
						stacksClone.Clear();
						OnValidate();
						return;
					}
					if (stacks[i].Amount != stacksClone[i].Amount)
					{
						int difference = stacks[i].Amount - stacksClone[i].Amount;
						stacksClone[i].Amount = stacks[i].Amount;
						OnChange?.Invoke(stacks[i].Resource, difference);
					}
				}
			}
		}

		void CloneStacks()
		{
			stacksClone.Clear();
			foreach (var stack in stacks)
			{
				stacksClone.Add(stack.Clone());
			}
		}
#endif
	}
}