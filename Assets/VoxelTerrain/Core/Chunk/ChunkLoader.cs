﻿using Eldemarkki.VoxelTerrain.Utilities;
using Eldemarkki.VoxelTerrain.VoxelData;
using Unity.Mathematics;
using UnityEngine;

namespace Eldemarkki.VoxelTerrain.World.Chunks
{
    /// <summary>
    /// A class for loading and initializing chunks
    /// </summary>
    /// 
    //[ExecuteInEditMode]
    public class ChunkLoader : MonoBehaviour
    {
        /// <summary>
        /// The world for which to generate the chunk for
        /// </summary>
        public VoxelWorld VoxelWorld { get; set; }

        /// <summary>
        /// Loads a chunk to a specific coordinate
        /// </summary>
        /// <param name="chunkCoordinate">The coordinate of the chunk to load</param>
        /// <returns>The newly loaded chunk</returns>
        public Chunk LoadChunkToCoordinate(int3 chunkCoordinate)
        {
            int3 worldPosition = chunkCoordinate * VoxelWorld.WorldSettings.ChunkSize;
            Chunk chunk = Instantiate(VoxelWorld.WorldSettings.ChunkPrefab, worldPosition.ToVectorInt(), Quaternion.identity); // ToDo: add transform

            Bounds chunkBounds = BoundsUtilities.GetChunkBounds(chunkCoordinate, VoxelWorld.WorldSettings.ChunkSize);
            JobHandleWithData<IVoxelDataGenerationJob> jobHandleWithData = VoxelWorld.VoxelDataGenerator.GenerateVoxelData(chunkBounds);
            VoxelWorld.VoxelDataStore.SetVoxelDataJobHandle(jobHandleWithData, chunkCoordinate);

            chunk.Initialize(chunkCoordinate, VoxelWorld);

            VoxelWorld.ChunkStore.AddChunk(chunk);

            return chunk;
        }
    }
}