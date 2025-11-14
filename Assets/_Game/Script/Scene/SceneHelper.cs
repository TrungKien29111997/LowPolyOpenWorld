using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;
using Core.UI;
using UnityEngine.SceneManagement;
using Core.Gameplay;
using UI;

namespace Core.Scene
{
    public class SceneHelper : Singleton<SceneHelper>
    {
        public IEnumerator IEChangeSceneLoading()
        {
            DebugCustom.Log("Change Scene Loading");
            SceneManager.LoadScene(Constant.SCENE_LOADING);
            yield return null;
            DebugCustom.Log("Change Scene Loading Done");
        }
        IEnumerator IEChangeSceneHome()
        {
            DebugCustom.Log("Change Scene Home");
            SceneManager.LoadScene(Constant.SCENE_HOME);
            yield return null;
            DebugCustom.Log("Change Scene Home Done");
        }

        public IEnumerator IEGoGameplay(Transform focusTransform = null)
        {
            yield return StartCoroutine(IEChangeSceneLoading());
            yield return StartCoroutine(IELoadSceneGameplay());
            yield return StartCoroutine(IELoadSceneUI());
            yield return StartCoroutine(IELoadGamePlay());
            GameManager.Instance.SetGameState(EGameState.Gameplay);
            //yield return StartCoroutine(UIManager.Instance.IEGameInit());
            //yield return StartCoroutine(LoadingPanel.Instance.IEEndTransition());
            //IAchievementController.Instance.UpdateAchievement(EAchievement.PlayGame, 1);
        }
        IEnumerator IELoadSceneGameplay()
        {
            DebugCustom.Log("Load Scene Gameplay");
            SceneManager.LoadScene(Constant.SCENE_GAME_PLAY);
            yield return null;
            DebugCustom.Log("Load Scene Gameplay Done");
        }
        IEnumerator IELoadSceneUI()
        {
            DebugCustom.Log("Load Scene UI");
            SceneManager.LoadScene(Constant.SCENE_UI, LoadSceneMode.Additive);
            yield return null;
            DebugCustom.Log("Load Scene UIDone");
        }
        public IEnumerator IELoadGamePlay()
        {
            yield return new WaitUntil(() => LevelManager.Instance);
            yield return new WaitUntil(() => UIManager.Instance);
            yield return UIManager.Instance.IEInit();
            UIManager.Instance.OpenUI<CanvasGameplay>();
            // yield return new WaitUntil(() => WorkshopIngame.Instance);
            // yield return new WaitUntil(() => EnemyManager.Instance);
            // UIManager.Instance.OnInit();
            // WorkshopIngame.Instance.InitLevel();
            // EnemyManager.Instance.OnInit();
            // GamePlayManager.Instance.SetUp(IChapterController.Instance.GetCurrentMode(), IChapterController.Instance.GetCurrentChapter());
            // UIManager.Instance.OpenUI<CanvasGamePlay>().SetCanvas(IChapterController.Instance.GetCurrentChapter());
        }
        public IEnumerator IEReturnHome(Transform focusTransform = null)
        {
            if (IsSceneLoaded(Constant.SCENE_UI))
            {
                SceneManager.UnloadSceneAsync(Constant.SCENE_UI);
            }
            if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName(Constant.SCENE_LOADING))
            {
                yield return StartCoroutine(IEChangeSceneLoading());
            }
            yield return StartCoroutine(IEChangeSceneHome());
            //yield return StartCoroutine(LoadingPanel.Instance.IEEndTransition());
            GameManager.Instance.SetGameState(EGameState.Home);
        }
        public bool IsSceneLoaded(string sceneName)
        {
            UnityEngine.SceneManagement.Scene s = SceneManager.GetSceneByName(sceneName);
            return s.IsValid() && s.isLoaded;
        }
    }
}