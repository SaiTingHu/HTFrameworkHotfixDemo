using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"HTFramework.RunTime.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// }}

	public void RefMethods()
	{
		// System.Void HT.Framework.ProcedureManager.SwitchProcedure<object>()
		// System.Void HT.Framework.UIManager.CloseUI<object>()
		// UnityEngine.Coroutine HT.Framework.UIManager.OpenUI<object>(object[])
		// object[] System.Array.Empty<object>()
	}
}