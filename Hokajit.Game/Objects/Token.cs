using Ratelite;
using Ratelite.GO;
using Ratelite.Resources;
using Ratelite.Utils;

namespace Hokajit.Objects;

public record class TokenData(uint id, Texture2D texture, RectInt region, float footPosition)
{
	public Region uv = texture.GetUVRegion(region);
}

public class Token : RObject
{
	public readonly TokenData data; 
	
	public Token(TokenData data)
	{
		this.data = data;
		material = new MaterialObject().SetTexture(data.texture).SetUVRegion(data.uv);
		mesh = MeshFactory.CreateQuad(data.region.size, new Vector2(data.footPosition, 0));
	}
	
	protected override void Update()
	{
		drawDepth = position.y / (100 * Map.TILE_SIZE) + position.x * 0.0000001F;
	}
}