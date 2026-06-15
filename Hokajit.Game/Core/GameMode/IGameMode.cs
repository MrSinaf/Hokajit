using Ratelite;
using Ratelite.GO;

namespace Hokajit.GameMode;

public interface IGameMode : IPlugin
{
	public World world { set; }
}