using Hokajit.Objects;
using Ratelite;

namespace Hokajit;

public struct Tile(Vector2Int position, TileData data)
{
	public readonly Vector2Int position = position;
	public Token? token;
	public TileData data = data;
}

public record class TileData(uint id, Region uv, bool isWalkable);