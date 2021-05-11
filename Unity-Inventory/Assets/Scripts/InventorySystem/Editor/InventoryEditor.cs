using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
	[CustomEditor(typeof(Inventory))]
	public class InventoryEditor : UnityEditor.Editor
	{
		Inventory inventory;
		
		void OnEnable()
		{
			inventory = (Inventory) target;
		}
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			
			if (GUILayout.Button("Update"))
			{
				Debug.Log("UpdateInventory");
			}
		}
	}
}