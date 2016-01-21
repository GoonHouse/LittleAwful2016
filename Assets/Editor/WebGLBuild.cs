// from: http://answers.unity3d.com/questions/829349/command-line-flag-to-build-webgl.html
// from: https://gist.github.com/jagwire/0129d50778c8b4462b68
// from: http://jonathanpeppers.com/Blog/automating-unity3d-builds-with-fake
//place this script in the Editor folder within Assets.
using UnityEditor;
using System.Collections.Generic;

//to be used on the command line:
//$ Unity -quit -batchmode -executeMethod WebGLBuild.build outputFolder

class WebGLBuild {
	static string[] GetScenes()
    {
        var scenes = EditorBuildSettings.scenes;
        List<string> goodScenes = new List<string>();
        foreach( EditorBuildSettingsScene scene in scenes) {
            if( scene.enabled ) {
                goodScenes.Add(scene.path);
            }
        }
        return goodScenes.ToArray();
    }
	
	static void build(){
		string[] arguments = System.Environment.GetCommandLineArgs();
		int i = 0;
		string outPath = "";
		foreach( string s in arguments ){
			if( s == "WebGLBuild.build" && arguments.Length-1 >= i+1 ){
				outPath = arguments[i+1];
			}
			i++;
		}
		EditorUserBuildSettings.webGLOptimizationLevel = 2;
		BuildPipeline.BuildPlayer(GetScenes(), outPath, BuildTarget.WebGL, BuildOptions.None);
	}
}