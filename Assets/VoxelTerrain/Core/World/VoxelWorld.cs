using Eldemarkki.VoxelTerrain.Meshing;
using Eldemarkki.VoxelTerrain.Settings;
using Eldemarkki.VoxelTerrain.VoxelData;
using Eldemarkki.VoxelTerrain.World.Chunks;
using Eldemarkki.VoxelTerrain.World.EditorUpdator;

using UnityEngine;

namespace Eldemarkki.VoxelTerrain.World
{
    /// <summary>
    /// The main entry point for interacting with the voxel world
    /// </summary>
    /// 
    [ExecuteAlways]
    public class VoxelWorld : MonoBehaviour
    {
        [SerializeField] private WorldSettings worldSettings;
        public WorldSettings WorldSettings => worldSettings;

        [SerializeField] private VoxelMesher voxelMesher;
        public VoxelMesher VoxelMesher => voxelMesher;

        [SerializeField] private VoxelDataStore voxelDataStore;
        public VoxelDataStore VoxelDataStore => voxelDataStore;

        [SerializeField] private VoxelDataGenerator voxelDataGenerator;
        public VoxelDataGenerator VoxelDataGenerator => voxelDataGenerator;

        [SerializeField] private ChunkProvider chunkProvider;
        public ChunkProvider ChunkProvider => chunkProvider;

        [SerializeField] private ChunkStore chunkStore;
        public ChunkStore ChunkStore => chunkStore;

        [SerializeField] private ChunkLoader chunkLoader;
        public ChunkLoader ChunkLoader => chunkLoader;

        [SerializeField] private EditorMap editorMap;
        public EditorMap EditorMap => editorMap;

        [SerializeField] private MapEditorEventHandler mapEditorEventHandler;
        public MapEditorEventHandler MapEditorEventHandler => mapEditorEventHandler;
    
     

        private void Awake()
        {
            voxelDataStore.VoxelWorld = this;
            chunkProvider.VoxelWorld = this;
            chunkLoader.VoxelWorld = this;
            //editorMap.VoxelWorld = this;
            mapEditorEventHandler.voxelWorld = this;
        }
    }
}