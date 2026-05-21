using Hokajit.Animations;
using Ratelite;
using Ratelite.Resources;

namespace Hokajit.Objects;

public class Character : Token
{
	public readonly EntityAnimation animation;
	public new readonly CharacterData data;
	
	public Character(CharacterData data) : base(data)
	{
		this.data = data;
		animation = components.AddComponent<EntityAnimation>();
		
	}
}

public record class CharacterData(
	string name,
	Vector2Int size,
	Texture2D texture,
	Region uv,
	int maxLife = 0,
	int force= 0,
	int resistance = 0,
	int agility = 0,
	int luck = 0
) : TokenData(name, size, texture, uv);