using Ratelite;

namespace Hokajit;

public record class TileData(uint id, Region uv);

public struct Tile
{
	public TileData data;
	public Vector2Int position;
	public uint life;
}