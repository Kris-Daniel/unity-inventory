using UnityEngine;

namespace InventorySystem
{
	[CreateAssetMenu(fileName = "Resource", menuName = "Inventory/Resource", order = 0)]
	public class Resource : ScriptableObject
	{
		public GameObject prefab;
	}
}