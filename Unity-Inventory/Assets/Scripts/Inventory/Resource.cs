using UnityEngine;

namespace Inventory
{
	[CreateAssetMenu(fileName = "Resource", menuName = "Inventory/Resource", order = 0)]
	public class Resource : ScriptableObject
	{
		public GameObject prefab;
	}
}