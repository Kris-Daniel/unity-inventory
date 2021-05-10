using System;
using InventorySystem;
using UnityEngine;

public class Test : MonoBehaviour
{
	[SerializeField] Inventory inventory;

	void Start()
	{
		for (int i = 0; i < inventory.StacksCount; i++)
		{
			print(inventory[i].Resource);
		}
	}
}