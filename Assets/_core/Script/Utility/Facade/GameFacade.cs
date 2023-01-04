using System;
using System.Collections.Generic;
using PlayerRegion;
using Script.Abstract;
using Script.AssetFactory;
using Script.Facade;
using Script.UI;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance;

    private IContainer _container;

    private IEventHolder _eventHolder;

    private GameObject _characterHolder;

    private string _playerAccount;

    //the player in current case
    private CurrentPlayer _player;


    // Start is called before the first frame update
    private void Awake()
    {
        #region GenerateInstance

        Debug.Log("GameFacedeAwake");

        var find = GameObject.Find("SceneBoot");
        if (find != null && find != gameObject)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        _container = new Container();
        //Instance register list
        RegisterList();
    }

    private void RegisterList()
    {
        //there is for registering mono instance

        #region RegisterResourceFactory

        //register common assetFactory
        var assetFactory = Register<IAssetFactory>(new ResourceFactory());

        //register GameObject pool by injecting from assetFactory internally 
        var gameObjectPool = Register<IGameObjectPool>(new ResourceFactoryProxy(assetFactory));

        #endregion

        #region UIkit

        //register UIkit by injecting from GameObject pool
        Register<IUIkit>(new UIkit(gameObjectPool));

        #endregion

        #region EventSystem

        _eventHolder = Register<IEventHolder>(new EventHolder());

        #endregion
    }


    #region FacadeForWholeGame

    //get the instance registered in RegisterList above
    public T GetInstance<T>() where T : class
    {
        return _container.GetInstance<T>();
    }

    //provide function of registering event directly
    public IUnRegister RegisterEvent<T>(Action<T> action) where T : new()
    {
        return _eventHolder.Register<T>(action);
    }

    public void UnRegisterEvent<T>(Action<T> action) where T : new()
    {
        _eventHolder.UnRegister<T>(action);
    }

    public void SendEvent<T>() where T : new()
    {
        _eventHolder.Send<T>();
    }

    public void SendEvent<T>(T t) where T : new()
    {
        _eventHolder.Send(t);
    }

    #endregion


    private T Register<T>(T obj, Mode mode = Mode.Singleton) where T : class
    {
        _container.Register<T>(obj, mode);
        return obj;
    }


    #region Player

    public void SetPlayer(string account)
    {
        //Set player 
        _player = new CurrentPlayer(account);
    }

    public void ClearBag()
    {
        _player.ClearBag();
    }

    //get bag
    public InventoryScrObj GetBag()
    {
        return _player?.GetBag();
    }

    //get account
    public string GetAccount()
    {
        return _player.GetAccount();
    }

    public void UpdateAccount(string account)
    {
        _player.UpdateAccount(account);
    }


    public void UpdateCharacterIndex(int index)
    {
        _player.UpdateCharacterIndex(index);
    }

    public int GetCharacterIndex()
    {
        return _player.GetCharacterIndex();
    }


    public void SetCharacterList(List<_core.AcountInfo.CharacterInfo> list)
    {
        _player.SetCharacterList(list);
    }

    public _core.AcountInfo.CharacterInfo GetSelectedCharacterInfo()
    {
        return _player.GetSelectedCharacterInfo();
    }

    public List<_core.AcountInfo.CharacterInfo> GetCharacterInfos()
    {
        return _player.GetCharacterInfos();
    }

    #endregion
}