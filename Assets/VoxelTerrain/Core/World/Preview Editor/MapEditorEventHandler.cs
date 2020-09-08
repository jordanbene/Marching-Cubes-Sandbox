using System;
using Unity.Collections;
using UnityEngine;
using Eldemarkki.VoxelTerrain.World.EditorUpdator;
using Eldemarkki.VoxelTerrain.World;

namespace Eldemarkki.VoxelTerrain.Settings
{
    /// <summary>
    /// Editor Map Interface
    /// </summary>
    [CreateAssetMenu(fileName = "New Map Editor Event Handler", menuName = "Marching Cubes Terrain/Map Editor Event Handler")]
    public class MapEditorEventHandler : ScriptableObject
    {
        [NonSerialized] public bool Update;

        public bool RecreateHeightMap;

        public VoxelWorld voxelWorld { get; set; }

        public delegate void UpdateValues();
        public event UpdateValues OnValuesUpdated;

        public static EditorMap EditorMapManager { get; set; }




    #if UNITY_EDITOR

        protected virtual void OnValidate()
        {
            
            UpdateEditor();

        }

        public void UpdateEditor()
        {
            UnityEditor.EditorApplication.update += NotifyOfUpdatedValues;
        }

        public void NotifyOfUpdatedValues()
        {
            UnityEditor.EditorApplication.update -= NotifyOfUpdatedValues;
            if (OnValuesUpdated != null)
            {
                OnValuesUpdated();
                Debug.Log("values updated");

            }
        }

    #endif




    }
}