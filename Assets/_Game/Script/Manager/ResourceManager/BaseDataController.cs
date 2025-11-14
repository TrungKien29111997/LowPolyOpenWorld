using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Data
{
    public interface IController<T> where T : IController<T>
    {
        public IEnumerator IEInit();
        public IEnumerator IEClearData();
    }

    public abstract class BaseDataController<T> where T : BaseDataController<T>, new()
    {
        public abstract string KeyData { get; }
        public abstract string KeyEvent { get; }

        protected bool waitSaveData;
        protected bool firstTimeInited;
        public abstract IEnumerator IEInit();
        protected virtual void OnInitSuccess()
        {
            if (!firstTimeInited)
            {
                firstTimeInited = true;
                FirstTimeInit();
            }
            SaveData();
        }

        public abstract IEnumerator IEClearData();

        protected virtual void FirstTimeInit()
        {
            Ex.EventManager.StartListening(Constant.TIMER_TICK_EVENT, OnUpdate);
            Ex.EventManager.StartListening(Constant.EVENT_ON_NEW_DAY, OnNextDay);
            Ex.EventManager.StartListening(Constant.TIMER_TICK_EVENT, OnTick);
        }
        protected virtual void OnTick()
        {
        }
        protected virtual void OnNextDay()
        {
        }

        protected virtual void OnNextWeek()
        {
        }

        protected virtual void OnNextMonth()
        {
        }

        protected virtual void OnUpdate()
        {
            if (waitSaveData)
            {
                SaveData();
                waitSaveData = false;
            }
        }

        protected abstract void SaveData();

        public virtual void OnValueChange()
        {
            waitSaveData = true;
            Ex.EventManager.EmitEvent(KeyEvent);
        }
    }
    public abstract class BaseLocalController<T, D> : BaseDataController<T>
    where T : BaseLocalController<T, D>, new()
    where D : BaseControllerCacheData, new()
    {
        protected D cachedData;

        public override IEnumerator IEInit()
        {
            if (cachedData == null)
            {
                string data = GameManager.Instance.GetLocalData(KeyData);
                if (string.IsNullOrEmpty(data))
                {
                    cachedData = new D();
                    cachedData.OnNewData();
                }
                else
                {
                    cachedData = Newtonsoft.Json.JsonConvert.DeserializeObject<D>(data);
                }
            }
            cachedData.FirstTimeInit();
            yield return null;
            OnInitSuccess();
        }

        public override IEnumerator IEClearData()
        {
            cachedData = null;
            PlayerPrefs.SetString(KeyData, string.Empty);
            yield return null;
        }

        protected override void SaveData()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cachedData);
            GameManager.Instance.SaveLocalData(KeyData, json);
        }
    }
    public abstract class BaseControllerCacheData
    {
        // Dung de khoi tao khi bat dau game, vi du generate cac dictionary
        public abstract void FirstTimeInit();
        // Khoi tao data moi hoan toan khi khong co data truoc do
        public abstract void OnNewData();
    }
}