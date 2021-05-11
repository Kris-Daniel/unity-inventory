using InventorySystem;
using UnityEngine;

namespace Demo
{
	public class Player : MonoBehaviour, IInventoryUser
	{
		[SerializeField] Inventory inventory;
		public Inventory Inventory => inventory;

		void Awake()
		{
			inventory.OnChange += ChangeInventory;
		}

		void ChangeInventory(Resource resource, int difference)
		{
			print(resource + " => " + difference);
		}

		void Update()
		{
			var ver = Input.GetAxis("Vertical");
			var hor = Input.GetAxis("Horizontal");
			
			transform.position += new Vector3(hor * 5, 0, ver * 5) * Time.deltaTime;
		}
	}
}