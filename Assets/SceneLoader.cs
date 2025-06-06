using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    private const string PersistentSceneName = "scn_index";
    private const string LoadingSceneName = "scn_loading";
    private const string FirstSceneName = "scn_menu";
    private static Scene _currentScene;
    
    public static async void LoadScene(string newSceneName)
    {
        await LoadSceneAsync(newSceneName);
    }
    
    public static void ExitGame()
    {
        Application.Quit();
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Main()
    {
        var activeScene = SceneManager.GetActiveScene();
        if (activeScene.name != PersistentSceneName)
        {
            SceneManager.LoadSceneAsync(PersistentSceneName, LoadSceneMode.Single);
            SceneManager.LoadSceneAsync(FirstSceneName, LoadSceneMode.Additive);
        }
    }
    
    private static async Task LoadSceneAsync(string newSceneName)
    {
        await SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);
        await Task.Delay(1000);
        if (_currentScene.IsValid())
        {
            await SceneManager.UnloadSceneAsync(_currentScene.name);
        }

        await SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);
        _currentScene = SceneManager.GetSceneByName(newSceneName);
        await Task.Delay(1000);
        await SceneManager.UnloadSceneAsync(LoadingSceneName);
    }
}
