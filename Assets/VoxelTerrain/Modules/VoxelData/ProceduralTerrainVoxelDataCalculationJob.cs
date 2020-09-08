﻿using System.Runtime.CompilerServices;
using Eldemarkki.VoxelTerrain.Settings;
using Eldemarkki.VoxelTerrain.Utilities;
using Unity.Burst;
using Unity.Mathematics;

namespace Eldemarkki.VoxelTerrain.VoxelData
{
    /// <summary>
    /// A procedural terrain voxel data calculation job
    /// </summary>
    [BurstCompile]
    public struct ProceduralTerrainVoxelDataCalculationJob : IVoxelDataGenerationJob
    {
        /// <summary>
        /// The procedural terrain generation settings
        /// </summary>
        public ProceduralTerrainSettings ProceduralTerrainSettings { get; set; }

        /// <summary>
        /// The sampling point's world position offset
        /// </summary>
        public int3 WorldPositionOffset { get; set; }

        /// <summary>
        /// The generated voxel data
        /// </summary>
        public VoxelDataVolume OutputVoxelData { get; set; }

        /// <summary>
        /// The execute method required for Unity's IJobParallelFor job type
        /// </summary>
        /// <param name="index">The iteration index provided by Unity's Job System</param>
        public void Execute(int index)
        {
            int3 worldPosition = IndexUtilities.IndexToXyz(index, OutputVoxelData.Width, OutputVoxelData.Height) + WorldPositionOffset;
            int worldPositionX = worldPosition.x;
            int worldPositionY = worldPosition.y;
            int worldPositionZ = worldPosition.z;

            float voxelData = CalculateVoxelData(worldPositionX, worldPositionY, worldPositionZ);
            OutputVoxelData.SetVoxelData(voxelData, index);
        }

        /// <summary>
        /// Calculates the voxel data at the world-space position
        /// </summary>
        /// <param name="worldPositionX">Sampling point's world-space x position</param>
        /// <param name="worldPositionY">Sampling point's world-space y position</param>
        /// <param name="worldPositionZ">Sampling point's world-space z position</param>
        /// <returns>The voxel data sampled from the world-space position</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float CalculateVoxelData(int worldPositionX, int worldPositionY, int worldPositionZ)
        {
            float voxelData = worldPositionY;
            voxelData -= OctaveNoise(worldPositionX, worldPositionZ, ProceduralTerrainSettings.NoiseFrequency * 0.001f, ProceduralTerrainSettings.NoiseOctaveCount) * ProceduralTerrainSettings.Amplitude;
            voxelData -= ProceduralTerrainSettings.HeightOffset;
            voxelData *= 0.5f;
            return voxelData;
        }

        /// <summary>
        /// Calculates octave noise
        /// </summary>
        /// <param name="x">Sampling point's x position</param>
        /// <param name="y">Sampling point's y position</param>
        /// <param name="frequency">The frequency of the noise</param>
        /// <param name="octaveCount">How many layers of noise to combine</param>
        /// <returns>The sampled noise value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float OctaveNoise(float x, float y, float frequency, int octaveCount)
        {
            float value = 0;

            for (int i = 0; i < octaveCount; i++)
            {
                int octaveModifier = (int)math.pow(2, i);

                // (x+1)/2 because noise.snoise returns a value from -1 to 1 so it needs to be scaled to go from 0 to 1.
                float pureNoise = (noise.snoise(new float2(octaveModifier * x * frequency, octaveModifier * y * frequency)) + 1) / 2f;
                value += pureNoise / octaveModifier;
            }

            return value;
        }
    }
}