using Hokajit.Data;
using Ratelite;
using Ratelite.GO;
using Ratelite.Resources;
using Ratelite.Utils;

namespace Hokajit.Objects;

public class Map : RObject
{
	public const int TILE_SIZE = 16;
	private readonly Tile[] tiles = new Tile[100 * 100];
	
	public Map()
	{
		var texture = Vault.GetAsset<Texture2D>("tiles")!;
		var data = new TileData(1, texture.GetUVRegion(new RectInt(4 * TILE_SIZE, 0, TILE_SIZE)));
		
		var quads = new (Rect vertices, Region region)[100 * 100];
		for (var x = 0; x < 100; x++)
		for (var y = 0; y < 100; y++)
		{
			ref var tile = ref tiles[x + y * 100];
			tile.position = new Vector2Int(x, y);
			tile.data = data;
			
			quads[x + y * 100] = (
				new Rect(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE),
				data.uv
			);
		}
		
		mesh = MeshFactory.CreateQuads(quads);
		material = new MaterialObject().SetTexture(texture);
	}
}