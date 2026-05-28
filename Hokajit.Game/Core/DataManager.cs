using Hokajit.Objects;

namespace Hokajit;

public static class DataManager
{
	public static readonly List<CharacterData> characters = [];
	public static readonly Dictionary<uint, TileData> tiles = new();
}