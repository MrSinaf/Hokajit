using Ratelite;
using Ratelite.GO;
using Ratelite.Resources;
using Ratelite.Utils;

namespace Hokajit.Objects;

public class Map : RObject
{
	public const byte CELL_SIZE = 16;
	private readonly Vector2Int size;
	
	public Map(Vector2Int size)
	{
		this.size = size;
		
		var meshes = new (Rect vertices, Region uvs)[size.x * size.y];
		var tiles = Vault.GetAsset<Texture2D>("tiles")!;
		var delta = tiles.texel.x * CELL_SIZE;
		for (var x = 0; x < size.x; x++)
		for (var y = 0; y < size.y; y++)
		{
			meshes[x + y * size.x] = (
				new Rect(
					new Vector2(x * CELL_SIZE, y * CELL_SIZE),
					new Vector2(CELL_SIZE)
				), new Region(
					new Vector2(delta * 3, 0),
					new Vector2(delta * 4, 1)
				));
		}
		mesh = MeshFactory.CreateQuads(meshes);
		material = new MaterialObject().SetTexture(tiles);
	}
}