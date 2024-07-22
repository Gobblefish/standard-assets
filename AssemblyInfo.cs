using System.Runtime.CompilerServices;

// Allow internal visibility for testing purposes.
[assembly: InternalsVisibleTo("Gobblefish.Runtime")]
// [assembly: InternalsVisibleTo("UnityEngine.Rendering.Universal")]

#if UNITY_EDITOR
// [assembly: InternalsVisibleTo("Unity.TextCore.Editor")]
// [assembly: InternalsVisibleTo("Unity.TextMeshPro.Editor")]
#endif
