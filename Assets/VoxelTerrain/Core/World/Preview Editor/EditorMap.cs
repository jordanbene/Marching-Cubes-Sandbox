using System.Collections;
using System.Collections.Generic;
using Eldemarkki.VoxelTerrain.World;
using UnityEngine;

namespace Eldemarkki.VoxelTerrain.World.EditorUpdator
{
    [ExecuteInEditMode]
    public class EditorMap : MonoBehaviour
    {
        /*
        public VoxelWorld VoxelWorld { get; set; }
        
        public delegate void UpdateValues();
        public event UpdateValues OnValuesUpdated;

        public bool update;
        public static EditorMap EditorMapManager { get; set; }

        void Update()
        {
            update = VoxelWorld.MapEditorEventHandler.Update;
        }

        
    #if UNITY_EDITOR

        void OnValidate()
        {
            if (update)
            {
                if (EditorMapManager == null)
                {
                    EditorMapManager = this;
                    Debug.Log("Editor Manager Instance Set"); 

                }
                if (EditorMapManager != null)
                {
                    UnityEditor.EditorApplication.update += NotifyOfUpdatedValues;
                } 

            }
        }

        public void NotifyOfUpdatedValues()
        {
            UnityEditor.EditorApplication.update  -= NotifyOfUpdatedValues;
            if (OnValuesUpdated != null)
            {
                OnValuesUpdated();
                Debug.Log("values updated");

            }
        } 

    #endif
        */
    }



}




