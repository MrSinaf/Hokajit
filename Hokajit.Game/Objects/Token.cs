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
	}
}

public record class TokenData(string name, Vector2 size, Texture2D texture, Region uv);