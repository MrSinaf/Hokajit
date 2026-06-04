using Hokajit.Scenes;
using Ratelite;

namespace Hokajit.Plugins;

public class RolePlay : IPlugin
{
	private readonly Game game;
	
	public RolePlay()
	{
		if (Stage.current is Game scene)
			game = scene;
		else
			throw new Exception(
				"RolePlay plugin requires a Game scene to initialize ┻━┻ ︵ヽ(`Д´)ﾉ︵ ┻━┻"
			);
	}
	
	public void Init()
	{
		
	}
	
	public void Update()
	{
		
	}
	
	public void Render()
	{
		
	}
	
	public void Destroy()
	{
		
	}
}