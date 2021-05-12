using System.Linq;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
	[CustomEditor(typeof(Inventory))]
	public class InventoryEditor : UnityEditor.Editor
	{
		Inventory inventory;
		string resourcesPath = "InventoryResources";
		int maxAmount = 1;
		bool debug;
		
		void OnEnable()
		{
			inventory = (Inventory) target;
		}
		
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Debug");
			debug = EditorGUILayout.Toggle(debug);
			GUILayout.EndHorizontal();

			if (debug)
			{
				GUILayout.Space(20);
				
				if (GUILayout.Button("Fill"))
				{
					foreach (var inventoryStack in inventory.Stacks)
					{
						inventoryStack.Amount = inventoryStack.MaxAmount;
					}
				}
				
				if (GUILayout.Button("Clear"))
				{
					foreach (var inventoryStack in inventory.Stacks)
					{
						inventoryStack.Amount = 0;
					}
				}
				
				GUILayout.Space(20);
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("MaxAmount");
				maxAmount = EditorGUILayout.IntField(maxAmount);
				maxAmount = Mathf.Clamp(maxAmount, 1, int.MaxValue);
				GUILayout.EndHorizontal();
				
				if (GUILayout.Button("Set MaxAmount to All"))
				{
					foreach (var inventoryStack in inventory.Stacks)
					{
						inventoryStack.MaxAmount = maxAmount;
					}
				}
				
				GUILayout.Space(20);
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("Resources Path");
				resourcesPath = GUILayout.TextField(resourcesPath, 55);
				GUILayout.EndHorizontal();
				
				if (GUILayout.Button("Recreate with all Resources"))
				{
					var resources = Resources.LoadAll<Resource>(resourcesPath).ToList();
					if (resources.Count > 0)
					{
						int maxResourcesInStack = inventory.Stacks.Count > 0 ? inventory.Stacks[0].MaxAmount : 10;
						inventory.Stacks.Clear();
						foreach (var resource in resources)
						{
							inventory.AddStack(Stack.Create(resource, 0, maxResourcesInStack));
						}
					}
				}
			}
		}
	}
}