using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Gears : MonoBehaviour
{
    public static Gears gears;

    public bool hideCursorOnStart;

    // public CanvasMain currentCanvas;
    
    [Header("Prefabs")]
    public Button button;
    public TextMeshProUGUI textPrefab;
    
    public static Dictionary<Type, int> typeDict = new Dictionary<Type, int>
    {
        {typeof(GameObject), 0},
        {typeof(RaycastHit), 1},
        {typeof(Transform), 2},
    };

    void Awake()
    {
        if (gears == null)
        {
            gears = this;
            DontDestroyOnLoad(this);
        }
    }
    
    void Start()
    {
        UnityEngine.Cursor.visible = !hideCursorOnStart;
    }
    
    void Update()
    {
        
    }

    #region Useful Function

        public static void IsClass<T>(object o, Action<T> onComplete)
        {
            if (o is T cast)
                onComplete?.Invoke(cast);
        }

        public static RectTransform GetRectTransform(GameObject obj)
        {
            if (obj.TryGetComponent(out RectTransform rectTransform))
            {
                return rectTransform;
            }

            return null;
        }

        #region MouseFunctions

            public static bool IsMouseOver_UiIgnore()
            {
                var pointerEventData = new PointerEventData(EventSystem.current) {position = Input.mousePosition};

                var raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                for (int i = 0; i < raycastResults.Count; i++)
                {
                    if (raycastResults[i].gameObject.TryGetComponent(out IgnoreMouseOver t))
                    {
                        raycastResults.RemoveAt(i);
                        i--;
                    }
                }

                return raycastResults.Count > 0;
            }

            public static bool IsMouseOver() //if the mouse is over any object
            {
                var pointerEventData = new PointerEventData(EventSystem.current) {position = Input.mousePosition};

                var raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                return raycastResults.Count > 0;
            }

            public static bool MouseOverGameObject(GameObject go) //Is the mouse over a specific object
            {
                var pointerEventData = new PointerEventData(EventSystem.current) {position = Input.mousePosition};

                var raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                return Array.Find(raycastResults.ToArray(), result => result.gameObject == go).gameObject != null;
            }

            public static GameObject ObjectUnderCursor() //Return the object under the mouse
            {
                var pointerEventData = new PointerEventData(EventSystem.current) {position = Input.mousePosition};

                var raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                return raycastResults.Capacity == 0 ? null : raycastResults[0].gameObject;
                //Debug.Log(raycastResults[0].gameObject);
            }

    #endregion

    #endregion

    #region Scenes

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
                Application.Quit();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static IEnumerator LoadAsyncScene(string sceneNameOrIndex, float secondsBeforeLoad, Action<AsyncOperation> loadingAction = null, Func<bool> conditionToActivateScene = null)
    {
        var asyncOperation = int.TryParse(sceneNameOrIndex, out var sceneIndex) ? SceneManager.LoadSceneAsync(sceneIndex) : 
            SceneManager.LoadSceneAsync(sceneNameOrIndex);

        if (asyncOperation == null)
            yield break;

        asyncOperation.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncOperation.isDone)
        {
            loadingAction?.Invoke(asyncOperation);

            // Check if the load has finished
            if (asyncOperation.progress > .8f)
            {
                if (conditionToActivateScene == null)
                    yield return LoadScene();
                else if (conditionToActivateScene())
                    yield return LoadScene();
            }

            yield return null;
        }

        IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(secondsBeforeLoad);
            asyncOperation.allowSceneActivation = true;
        }
    }

    #endregion
}

[Serializable]
public class Pair<T1, T2>
{
    public T1 value1;
    public T2 value2;

    public Pair(T1 value1, T2 value2)
    {
        this.value1 = value1;
        this.value2 = value2;
    }
}
