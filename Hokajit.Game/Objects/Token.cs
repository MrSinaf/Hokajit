using Ratelite;
using Ratelite.GO;
using Ratelite.Resources;

namespace Hokajit.Objects;

public class Token : RObject
{
	public readonly TokenData data;
	
	public Token(TokenData data)
	{
		this.data = data;
		name = data.name;
		mesh = Vault.GetAsset<Mesh>(data.size + ".mesh");
		material = new MaterialObject().SetTexture(data.texture).SetUVRegion(data.uv);
	}
}

public record class TokenData(string name, Vector2Int size, Texture2D texture, Region uv);