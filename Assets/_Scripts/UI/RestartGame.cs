using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
#region Fields
    public Object reload_scene;
#endregion

#region Implementation
    public void ReloadScene()
    {
        SceneManager.LoadScene( $"{ reload_scene.name }" );
    }
#endregion
}
