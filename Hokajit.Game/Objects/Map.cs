using Ratelite;
using Ratelite.GO;
using Ratelite.Resources;
using Ratelite.Utils;

namespace Hokajit.Objects;

public class Map
{
	public const int TILE_SIZE = 16;
	private readonly Tile[] tiles = new Tile[100 * 100];
	private readonly World world;
	
	private readonly RObject floor;
	private readonly RObject wall;
	
	public float wallsVisibility
	{
		get;
		set
		{
			field = value;
			wall.material.SetProperty("u_wallsVisibility", value);
		}
	} = 0.35F;
	
	public float cursorPosition
	{
		get;
		set
		{
			field = value;
			wall.material.SetProperty("u_cursorPosition", value);
		}
	} = 0;
	
	public Map(World world, Vector2Int size)
	{
		this.world = world;
		
		var floorTexture = Vault.GetAsset<Texture2D>("floors")!;
		var data1 = new TileData(
			1, floorTexture.GetUVRegion(new RectInt(4 * TILE_SIZE, 0, TILE_SIZE))
		);
		var data2 = new TileData(
			2, floorTexture.GetUVRegion(new RectInt(5 * TILE_SIZE, 0, TILE_SIZE))
		);
		
		var quads = new (Rect vertices, Region region)[size.x * size.y];
		for (var x = 0; x < size.x; x++)
		for (var y = 0; y < size.y; y++)
		{
			ref var tile = ref tiles[x + y * size.x];
			var data = y > 50 ? data2 : data1;
			tile.position = new Vector2Int(x, y);
			tile.data = data;
			
			quads[x + y * size.x] = (
				new Rect(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE),
				data.uv
			);
		}
		
		world.AddObject(floor = new RObject
		{
			mesh = MeshFactory.CreateQuads(quads),
			material = new MaterialObject().SetTexture(floorTexture)
		});
		
		
		var wallTexture = Vault.GetAsset<Texture2D>("walls")!;
		var data3 = new TileData(
			3, wallTexture.GetUVRegion(new RectInt(0, 0, TILE_SIZE))
		);
		quads = new (Rect vertices, Region region)[size.x * 2];
		for (var y = 0; y < 2; y++)
		for (var x = 0; x < size.x; x++)
		{
			quads[x + y * size.x] = (
				new Rect(x * TILE_SIZE, (y + 51) * TILE_SIZE, TILE_SIZE),
				data3.uv
			);
		}
		
		world.AddObject(wall = new RObject
		{
			mesh = MeshFactory.CreateQuads(quads),
			material = new MaterialObject(Vault.LoadResource<Shader>("shaders/wall.rshad"))
					.SetTexture(wallTexture)
					.SetProperty("u_wallsVisibility", wallsVisibility)
					.SetProperty("u_tileSize", TILE_SIZE)
					.SetProperty("u_depthRange", size.x * TILE_SIZE),
			drawOrder = int.MaxValue
		});
	}
}