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
    private static async void Main()
    {
        var activeScene = SceneManager.GetActiveScene();
        if (activeScene.name != PersistentSceneName)
        { 
            SceneManager.LoadSceneAsync(PersistentSceneName, LoadSceneMode.Single);
            await LoadSceneAsync(FirstSceneName, LoadSceneMode.Additive);
        }
        else
        {
            await LoadSceneAsync(FirstSceneName, LoadSceneMode.Additive);
        }
    }
    
    private static async Task LoadSceneAsync(string newSceneName, LoadSceneMode mode = LoadSceneMode.Additive)
    {
        await SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);
        
        await Task.Delay(500);
        
        if (_currentScene.IsValid())
        {
            await SceneManager.UnloadSceneAsync(_currentScene.name);
        }

        await SceneManager.LoadSceneAsync(newSceneName, mode);
        _currentScene = SceneManager.GetSceneByName(newSceneName);
        
        await Task.Delay(500);
        
        await SceneManager.UnloadSceneAsync(LoadingSceneName);
    }
}
