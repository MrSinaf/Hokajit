using Ratelite;
using Ratelite.GO;
using Ratelite.Resources;
using Ratelite.Utils;

namespace Hokajit.Objects;

public class Map : RObject
{
	private readonly Tile[,] grid;
	
	public Map(Vector2Int size)
	{
		grid = new Tile[size.x, size.y];
		var meshes = new (Rect vertices, Region uvs)[size.x * size.y];
		var tiles = Vault.GetAsset<Texture2D>("tiles")!;
		var tileData = DataManager.tiles[0];
		for (var x = 0; x < size.x; x++)
		for (var y = 0; y < size.y; y++)
		{
			grid[x, y] = new Tile(new Vector2Int(x, y), tileData);
			meshes[x + y * size.x] = (new Rect(
				new Vector2(x * Game.TILE_SIZE, y * Game.TILE_SIZE),
				new Vector2(Game.TILE_SIZE)
			), tileData.uv);
		}
		mesh = MeshFactory.CreateQuads(meshes);
		material = new MaterialObject().SetTexture(tiles);
		drawOrder = int.MinValue;
	}
	
	public void SetFloor(Vector2Int position, TileData data)
	{
		grid[position.x, position.y].data = data;
		var i = (position.x + position.y * grid.GetLength(0)) * 4;
		mesh.vertices[i].uv = data.uv.position00;
		mesh.vertices[i + 1].uv = new Vector2(data.uv.position11.x, data.uv.position00.y);
		mesh.vertices[i + 2].uv = data.uv.position11;
		mesh.vertices[i + 3].uv = new Vector2(data.uv.position00.x, data.uv.position11.y);
		mesh.ApplyVertex(i, 4);
	}
	
	public void SetToken(Vector2Int position, Token token)
	{
		grid[position.x, position.y].token = token;
	}
	
	public bool GetToken(Vector2Int position, out Token? token)
	{
		token = grid[position.x, position.y].token;
		return token != null;
	}
	
	public static Vector2Int WorldPositionToCell(Vector2 position)
		=> (position / Game.TILE_SIZE).ToVector2Int(RoundingMode.Floor);
}