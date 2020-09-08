using Eldemarkki.VoxelTerrain.Settings;
using Unity.Mathematics;
using UnityEngine;


namespace Eldemarkki.VoxelTerrain.World
{
    /// <summary>
    /// A procedurally generated world
    /// </summary>
    /// 
    [ExecuteAlways]
    public class HeightmapWorldGenerator : MonoBehaviour
    {
        public bool update = true;
        public bool generateHeightmapTerrain = false;
        /// <summary>
        /// The voxel world the "owns" this world generator
        /// </summary>
        [SerializeField] private VoxelWorld voxelWorld;
        //public VoxelWorld voxelWorld { get; set; }

        /// <summary>
        /// The settings for generating the heightmap terrain
        /// </summary>
        [SerializeField] public HeightmapTerrainSettings heightmapTerrainSettings;
        
        
        /// <summary>
        /// The settings for generating the heightmap terrain
        /// </summary>
        public HeightmapTerrainSettings HeightmapTerrainSettings => heightmapTerrainSettings;

        private void Awake()
        {

        }
        private void Start()
        {
            
        }

        private void OnDestroy()
        {
            heightmapTerrainSettings.Dispose();
        }


        void OnHeightValuesUpdated()
        {
            Debug.Log("Initializing heightmap");
                
            heightmapTerrainSettings.Initialize(heightmapTerrainSettings.Heightmap, heightmapTerrainSettings.Amplitude, heightmapTerrainSettings.HeightOffset);
                
 
        }
        void InitializeChunks()
        {
            CreateHeightmapTerrain();

        }
        private void Update()
        {
            if (update)
            {
                UpdateValues();
                Debug.Log("Update tick");
                update = false;
                
            }
        }
        void OnValidate()
        {
            update = true;
            
        }

        void UpdateValues()
        {
            Debug.Log("Heightmap on Validate");

            if (voxelWorld.EditorMap != null)
            {
                Debug.Log("Editormap is not null");

                if (heightmapTerrainSettings.HeightmapData == null & !generateHeightmapTerrain)
                {
                    voxelWorld.MapEditorEventHandler.OnValuesUpdated -= OnHeightValuesUpdated;

                    voxelWorld.MapEditorEventHandler.OnValuesUpdated += OnHeightValuesUpdated;
                    Debug.Log("Validated Heightmap");

                }
                else if (generateHeightmapTerrain & heightmapTerrainSettings.HeightmapData == null)
                {
                  
                        
                    voxelWorld.MapEditorEventHandler.OnValuesUpdated -= InitializeChunks;                       
                    voxelWorld.MapEditorEventHandler.OnValuesUpdated += InitializeChunks;   
                    //autoUpdate = false;
                    Debug.Log("HeightmapData Does not exist: Generating");


                }
                else if (heightmapTerrainSettings.HeightmapData != null) 
                { 
                    
                    Debug.Log("HeightmapData is already Generated");
                }
                 
            }
        }


        /// <summary>
        /// Creates the heightmap terrain and instantiates the chunks.
        /// </summary>
        public void CreateHeightmapTerrain()
        {
            int chunkCountX = Mathf.CeilToInt((float)(heightmapTerrainSettings.Width - 1) / voxelWorld.WorldSettings.ChunkSize); //chunksize is how many voxels per CHUNK? 
            int chunkCountZ = Mathf.CeilToInt((float)(heightmapTerrainSettings.Height - 1) / voxelWorld.WorldSettings.ChunkSize);
            //int chunkCountY = Mathf.CeilToInt(heightmapTerrainSettings.Amplitude / voxelWorld.WorldSettings.ChunkSize);
            int chunkCountY = Mathf.CeilToInt(heightmapTerrainSettings.Amplitude / voxelWorld.WorldSettings.ChunkSize);

            for (int x = 0; x < chunkCountX; x++)
            {
                for (int y = 0; y < chunkCountY; y++) //chunkCountY the height of the map. chunks are loaded for those positions
                {
                    for (int z = 0; z < chunkCountZ; z++)
                    {
                    
                        voxelWorld.ChunkProvider.EnsureChunkExistsAtCoordinate(new int3(x, y, z));
                    }
                }
            }
        }


    }
}