using InventorySystem;
using UnityEngine;

namespace Demo
{
	public class Player : MonoBehaviour, IInventoryUser
	{
		[SerializeField] Inventory inventory;
		public Inventory Inventory => inventory;
	}
}