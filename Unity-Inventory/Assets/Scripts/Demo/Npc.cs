using InventorySystem;
using UnityEngine;

namespace Demo
{
	public class Npc : MonoBehaviour, IInventoryUser
	{
		[SerializeField] Inventory inventory;
		public Inventory Inventory => inventory;
		
		void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IInventoryUser playerInventoryUser))
			{
				foreach (var inventoryStack in inventory.Stacks)
				{
					Stack stack = playerInventoryUser.Inventory.GetStack(inventoryStack.Resource);
					Inventory.TransferResources(inventoryStack, stack, 3);
				}
			}
		}
	}
}