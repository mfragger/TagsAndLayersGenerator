# Tags And Layers Generator
A unity script that generates constant fields of tags and layers in a unity project

## Install
Go to ``Package Manager`` -> ``Add Package from git URL``
Paste the following
```https://github.com/mfragger/TagsAndLayersGenerator.git```

## Sample Generated Code
```cs
 //This is a generated file. Changes will be removed on the next compilation
namespace GeneratedNamespace
{
	public struct Layers
	{
		public const int DEFAULT = 0;
		public const int TRANSPARENTFX = 1;
		public const int IGNORE_RAYCAST = 2;
		public const int WATER = 4;
		public const int UI = 5;
	}
	public struct Tags
	{
		public const string UNTAGGED = "Untagged";
		public const string RESPAWN = "Respawn";
		public const string FINISH = "Finish";
		public const string EDITORONLY = "EditorOnly";
		public const string MAINCAMERA = "MainCamera";
		public const string PLAYER = "Player";
		public const string GAMECONTROLLER = "GameController";
	}
}
```
