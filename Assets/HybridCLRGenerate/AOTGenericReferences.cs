using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"HTFramework.RunTime.dll",
		"UnityEngine.CoreModule.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// HT.Framework.SingletonBehaviourBase.ModuleBase<object>
	// HT.Framework.SingletonBehaviourBase<object>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// }}

	public void RefMethods()
	{
		// System.Void HT.Framework.ProcedureManager.SwitchProcedure<object>()
		// System.Void HT.Framework.UIManager.CloseUI<object>()
		// UnityEngine.Coroutine HT.Framework.UIManager.OpenUI<object>(object[])
		// object[] System.Array.Empty<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
	}
}