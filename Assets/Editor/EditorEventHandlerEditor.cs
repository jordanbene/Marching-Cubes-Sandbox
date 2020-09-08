using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using Unity.Collections;
using Eldemarkki.VoxelTerrain.World.EditorUpdator;
using Eldemarkki.VoxelTerrain.World;
using Eldemarkki.VoxelTerrain.Settings;

[CustomEditor(typeof(MapEditorEventHandler))]
public class EditorEventHandlerEditor : Editor
{

	public override void OnInspectorGUI()
	{
		MapEditorEventHandler mapEditorEventHandler = (MapEditorEventHandler)target;

		if (DrawDefaultInspector())
		{
			if (mapEditorEventHandler.Update)
			{
				//
			}
		}

		if (GUILayout.Button("Update Editor Map"))
		{
			mapEditorEventHandler.NotifyOfUpdatedValues();
			mapEditorEventHandler.UpdateEditor();
			EditorUtility.SetDirty(target);
			GUIUtility.ExitGUI();

		}
		if (GUILayout.Button("Regenerate Heightmap"))
		{
			mapEditorEventHandler.NotifyOfUpdatedValues();
			mapEditorEventHandler.UpdateEditor();
			EditorUtility.SetDirty(target);
			GUIUtility.ExitGUI();

		}
	}
}