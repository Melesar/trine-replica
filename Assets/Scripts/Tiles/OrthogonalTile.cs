using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR

#endif

namespace Tiles
{
    [CreateAssetMenu(menuName = "Tiles/Orthogonal tile")]
    public class OrthogonalTile : Tile, ISerializationCallbackReceiver
    {
        public TilePiece[] tilePieces;

        private Dictionary<byte, int> indiciesMap;
    
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            var mask = GetMask(position, tilemap);

            var index = GetTileIndex(mask);
            if (index < 0 || index >= tilePieces.Length) {
                Debug.LogError($"Invalid index {index}", this);
                return;
            }

            tileData.sprite = tilePieces[index].sprite;
        }

        private int GetMask(Vector3Int position, ITilemap tilemap)
        {
            var mask = 0;
            
            mask |= HasNeighbour(tilemap, position + Vector3Int.up) ? (1 << 4) : 0;
            mask |= HasNeighbour(tilemap, position + Vector3Int.down) ? (1 << 3) : 0;
            mask |= HasNeighbour(tilemap, position + Vector3Int.left) ? (1 << 1) : 0;
            mask |= HasNeighbour(tilemap, position + Vector3Int.right) ? (1 << 6) : 0;
            
            mask |= HasNeighbour(tilemap, position + Vector3Int.one) ? (1 << 7) : 0;
            mask |= HasNeighbour(tilemap, position - Vector3Int.one) ? (1 << 0) : 0;
            
            mask |= HasNeighbour(tilemap, position + new Vector3Int(-1, 1, 0)) ? (1 << 2) : 0;
            mask |= HasNeighbour(tilemap, position + new Vector3Int(1, -1, 0)) ? (1 << 5) : 0;
            
            return mask;
        }

        private int GetTileIndex(int mask)
        {
            int result;
            indiciesMap.TryGetValue((byte) mask, out result);
            return  result;
        }

        public override void RefreshTile(Vector3Int position, ITilemap tilemap)
        {
            tilemap.RefreshTile(position);
            
            for (int row = -1; row <= 1; row++) {
                for (int column = -1; column <= 1; column++) {
                    var neighbourPos = new Vector3Int(position.x + row, position.y + column, position.z);
                    if (HasNeighbour(tilemap, neighbourPos)) {
                        tilemap.RefreshTile(neighbourPos);
                    }
                }
            }
        }

        private bool HasNeighbour(ITilemap tilemap, Vector3Int position)
        {
            return tilemap.GetTile(position) == this;
        }

        private void CreateIndiciesMap()
        {
            indiciesMap = new Dictionary<byte, int>();

            for (var index = 0; index < tilePieces.Length; index++) {
                var tilePiece = tilePieces[index];
                indiciesMap.Add((byte) tilePiece.mask, index);
            }
        }
    
        private void OnEnable()
        {
            CreateIndiciesMap();
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            CreateIndiciesMap();
        }
    }
}
