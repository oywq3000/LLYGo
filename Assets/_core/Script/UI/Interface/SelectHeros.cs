using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using _core.Script.UI;
using _core.Script.UI.Interface;
using _core.Script.UI.Panel;
using _core.Script.UI.UIVariable;
using _core.Script.Utility.Extension;
using UnityEngine;
using DG.Tweening;
using MysqlUtility;
using Script.Abstract;
using Script.UI;
using UnityEngine.UI;
using CharacterInfo = _core.AcountInfo.CharacterInfo;

public class SelectHeros : MonoBehaviour
{
    //pos
    public RectTransform center;
    public RectTransform left1;
    public RectTransform left2;
    public RectTransform right1;
    public RectTransform right2;
    public Transform content;

    //config parameter 
    public float smaller = 0.8f;
    public float min = 0.4f;
    public float max = 1.4f;
    public float speed = 0.2f;
    public float alpha = 0.8f;

    public Button leftSelect;
    public Button rightSelect;

    private IGameObjectPool _gameObjectPool;
    private IAssetFactory _assetFactory;

    private List<GameObject> _items = new List<GameObject>();
    private List<CharacterInfo> _characterInfos;
    private int _currentIndex;

    private int CurrentIndex
    {
        get => _currentIndex;
        set
        {
            _currentIndex = value;
            
            //switch the the character index for current account
            GameFacade.Instance.UpdateCharacterIndex(CurrentIndex);
        }
    }


    void Start()
    {
        _gameObjectPool = GameFacade.Instance.GetInstance<IGameObjectPool>();
        _assetFactory = GameFacade.Instance.GetInstance<IAssetFactory>();

        //init items 
        LoadItems();
        //init the card item position
        InitRectTranform();

        //register event
        leftSelect.onClick.AddListener(() => { OnLeftButtonClick(); });

        rightSelect.onClick.AddListener(() => { OnRightButtonClick(); });
    }


    private void LoadItems()
    {
        _characterInfos = MysqlTool.GetCharactersByAccount<CharacterInfo>(GameFacade.Instance.GetAccount());
        
        GameFacade.Instance.SetCharacterList(_characterInfos);
        
        foreach (var info in _characterInfos)
        {
            var characterCard = _gameObjectPool.Dequeue("Slot_CharacterSelect", content);
            characterCard.transform.SetAsFirstSibling();
            var characterSlotController = characterCard.GetComponent<CharacterSlotController>();
            
            //set the img
            characterSlotController.SetCharacterImg(_assetFactory.LoadAsset<Sprite>(info.GetSpriteName()));
            characterSlotController.SetCharacterName(info.Name);
            characterSlotController.SetCharacterLevel(Int32.Parse(info.Exp));

            //add to list
            _items.Add(characterCard);
        }


        //add the empty slot for add option
        var emptySlot = _gameObjectPool.Dequeue("Slot_CharacterEmpty", content);
        _items.Add(emptySlot);
        
        //set the current character index
        GameFacade.Instance.UpdateCharacterIndex(CurrentIndex);
        
    }


    public bool IsSelected()
    {
        //if the current 
        
        return GameFacade.Instance.GetCharacterIndex() != _items.Count - 1;
    }
    

    private void InitRectTranform()
    {
        if (CurrentIndex < 0 || CurrentIndex >= _items.Count) return;

        //location 
        _items[CurrentIndex].GetComponent<RectTransform>().anchoredPosition = center.anchoredPosition;
        _items[CurrentIndex].GetComponent<RectTransform>().localScale = new Vector3(max, max, max);
        _items[CurrentIndex].GetComponent<CanvasGroup>().alpha = 1;

        if (CurrentIndex == _items.Count - 1)
        {
            //the lasting item 
            _items[CurrentIndex].GetComponent<CharacterEmptySlotController>().addBtn.onClick
                .AddListener(OnAddOneCard);
        }
        else
        {
            _items[CurrentIndex].GetComponent<CharacterSlotController>()
                .deleteBtn.onClick.AddListener(OnDeleteOneCard);
        }


        if (CurrentIndex + 1 < _items.Count)
        {
            _items[CurrentIndex + 1].GetComponent<RectTransform>().anchoredPosition = right1.anchoredPosition;
            _items[CurrentIndex + 1].GetComponent<RectTransform>().localScale = new Vector3(smaller, smaller, smaller);
            _items[CurrentIndex + 1].GetComponent<CanvasGroup>().alpha = alpha;
            _items[CurrentIndex + 1].transform.SetAsFirstSibling();
        }

        if (CurrentIndex + 2 < _items.Count)
        {
            _items[CurrentIndex + 2].GetComponent<RectTransform>().anchoredPosition = right2.anchoredPosition;
            _items[CurrentIndex + 2].GetComponent<RectTransform>().localScale = new Vector3(min, min, min);
            _items[CurrentIndex + 2].GetComponent<CanvasGroup>().alpha = alpha;
            _items[CurrentIndex + 2].transform.SetAsFirstSibling();
        }

        if (CurrentIndex - 1 >= 0)
        {
            _items[CurrentIndex - 1].GetComponent<RectTransform>().anchoredPosition = right1.anchoredPosition;
            _items[CurrentIndex - 1].GetComponent<RectTransform>().localScale = new Vector3(smaller, smaller, smaller);
            _items[CurrentIndex - 1].GetComponent<CanvasGroup>().alpha = alpha;
            _items[CurrentIndex - 1].transform.SetAsFirstSibling();
        }

        if (CurrentIndex - 2 >= 0)
        {
            _items[CurrentIndex - 2].GetComponent<RectTransform>().anchoredPosition = right2.anchoredPosition;
            _items[CurrentIndex - 2].GetComponent<RectTransform>().localScale = new Vector3(min, min, min);
            _items[CurrentIndex - 2].GetComponent<CanvasGroup>().alpha = alpha;
            _items[CurrentIndex - 2].transform.SetAsFirstSibling();
        }

        for (int i = CurrentIndex + 3; i < _items.Count; i++)
        {
            _items[i].SetActive(false);
        }

        for (int i = CurrentIndex - 3; i >= 0; i--)
        {
            _items[i].SetActive(false);
        }
    }


    private void OnLeftButtonClick()
    {
        if (CurrentIndex < _items.Count - 1)
        {
            _items[CurrentIndex].GetComponent<RectTransform>().DOAnchorPos(
                left1.anchoredPosition, speed);
            _items[CurrentIndex].GetComponent<RectTransform>().DOScale(smaller, speed);
            _items[CurrentIndex].GetComponent<CanvasGroup>().alpha = alpha;
            _items[CurrentIndex].transform.SetAsFirstSibling();

            if (CurrentIndex == _items.Count - 1)
            {
                //the lasting item 
                _items[CurrentIndex].GetComponent<CharacterEmptySlotController>().addBtn.onClick.RemoveAllListeners();
            }
            else
            {
                _items[CurrentIndex].GetComponent<CharacterSlotController>()
                    .deleteBtn.onClick.RemoveAllListeners();
            }


            if (CurrentIndex - 1 >= 0)
            {
                _items[CurrentIndex - 1].GetComponent<RectTransform>().DOAnchorPos(
                    left2.anchoredPosition, speed);
                _items[CurrentIndex - 1].GetComponent<RectTransform>().DOScale(min, speed);
                _items[CurrentIndex - 1].GetComponent<CanvasGroup>().alpha = alpha;
                _items[CurrentIndex - 1].transform.SetAsFirstSibling();
            }

            if (CurrentIndex - 2 >= 0)
            {
                _items[CurrentIndex - 2].SetActive(false);
            }

            if (CurrentIndex + 1 < _items.Count)
            {
                _items[CurrentIndex + 1].GetComponent<RectTransform>().DOAnchorPos(
                    center.anchoredPosition, speed);
                _items[CurrentIndex + 1].GetComponent<RectTransform>().DOScale(max, speed);
                _items[CurrentIndex + 1].GetComponent<CanvasGroup>().alpha = 1;

                //this item is selected 

                if (CurrentIndex + 1 == _items.Count - 1)
                {
                    //the lasting item 
                    _items[CurrentIndex + 1].GetComponent<CharacterEmptySlotController>().addBtn.onClick
                        .AddListener(OnAddOneCard);
                }
                else
                {
                    _items[CurrentIndex + 1].GetComponent<CharacterSlotController>()
                        .deleteBtn.onClick.AddListener(OnDeleteOneCard);
                }
            }

            if (CurrentIndex + 2 < _items.Count)
            {
                _items[CurrentIndex + 2].GetComponent<RectTransform>().DOAnchorPos(
                    right1.anchoredPosition, speed);
                _items[CurrentIndex + 2].GetComponent<RectTransform>().DOScale(smaller, speed);
                _items[CurrentIndex + 2].GetComponent<CanvasGroup>().alpha = alpha;
                _items[CurrentIndex + 2].transform.SetAsFirstSibling();
            }

            //judge whether can appear new item
            if (CurrentIndex + 3 < _items.Count)
            {
                _items[CurrentIndex + 3].SetActive(true);
                _items[CurrentIndex + 3].GetComponent<RectTransform>().anchoredPosition =
                    right2.anchoredPosition;
                _items[CurrentIndex + 3].GetComponent<RectTransform>().DOScale(min, speed);
                _items[CurrentIndex + 3].GetComponent<CanvasGroup>().alpha = alpha;

                _items[CurrentIndex + 3].transform.SetAsFirstSibling();
            }

            CurrentIndex++;
        }
    }

    private void OnRightButtonClick()
    {
        if (CurrentIndex > 0)
        {
            _items[CurrentIndex].GetComponent<RectTransform>().DOAnchorPos(
                right1.anchoredPosition, speed);
            _items[CurrentIndex].GetComponent<RectTransform>().DOScale(smaller, speed);
            _items[CurrentIndex].GetComponent<CanvasGroup>().alpha = alpha;
            _items[CurrentIndex].transform.SetAsFirstSibling();
            //Unregister button event
            if (CurrentIndex == _items.Count - 1)
            {
                //the lasting item 
                _items[CurrentIndex].GetComponent<CharacterEmptySlotController>().addBtn.onClick.RemoveAllListeners();
            }
            else
            {
                _items[CurrentIndex].GetComponent<CharacterSlotController>()
                    .deleteBtn.onClick.RemoveAllListeners();
            }


            if (CurrentIndex - 1 >= 0)
            {
                _items[CurrentIndex - 1].GetComponent<RectTransform>().DOAnchorPos(
                    center.anchoredPosition, speed);

                _items[CurrentIndex - 1].GetComponent<RectTransform>().DOScale(max, speed);
                _items[CurrentIndex - 1].GetComponent<CanvasGroup>().alpha = 1;


                if (CurrentIndex - 1 == _items.Count - 1)
                {
                    //the lasting item 
                    _items[CurrentIndex - 1].GetComponent<CharacterEmptySlotController>().addBtn.onClick
                        .AddListener(OnAddOneCard);
                }
                else
                {
                    _items[CurrentIndex - 1].GetComponent<CharacterSlotController>()
                        .deleteBtn.onClick.AddListener(OnDeleteOneCard);
                }
            }

            if (CurrentIndex - 2 >= 0)
            {
                _items[CurrentIndex - 2].GetComponent<RectTransform>().DOAnchorPos(
                    left1.anchoredPosition, speed);

                _items[CurrentIndex - 2].GetComponent<RectTransform>().DOScale(smaller, speed);
                _items[CurrentIndex - 2].GetComponent<CanvasGroup>().alpha = alpha;

                _items[CurrentIndex - 2].transform.SetAsFirstSibling();
            }

            if (CurrentIndex - 3 >= 0)
            {
                _items[CurrentIndex - 3].SetActive(true);
                _items[CurrentIndex - 3].GetComponent<RectTransform>().anchoredPosition =
                    left2.anchoredPosition;

                _items[CurrentIndex - 3].GetComponent<RectTransform>().DOScale(min, speed);
                _items[CurrentIndex - 3].GetComponent<CanvasGroup>().alpha = alpha;

                _items[CurrentIndex - 3].transform.SetAsFirstSibling();
            }

            if (CurrentIndex + 1 < _items.Count)
            {
                _items[CurrentIndex + 1].GetComponent<RectTransform>().DOAnchorPos(
                    right2.anchoredPosition, speed);

                _items[CurrentIndex + 1].GetComponent<RectTransform>().DOScale(min, speed);
                _items[CurrentIndex + 1].GetComponent<CanvasGroup>().alpha = alpha;

                _items[CurrentIndex + 1].transform.SetAsFirstSibling();
            }

            if (CurrentIndex + 2 < _items.Count)
            {
                _items[CurrentIndex + 2].SetActive(false);
            }

            CurrentIndex--;
        }
    }

    private void OnDeleteOneCard()
    {
        //delete 

        var operationPanel = GameFacade.Instance.GetInstance<IUIkit>().OpenPanel("ConfirmPanel");

        var operation = operationPanel.GetComponent<IOperationPanel>();
        operationPanel.GetComponent<ConfirmPanel>().SetContentText("是否确定要删除改角色");

        operation.Result = variable =>
        {
            //confirm to delete the current card
            DeleteCurrentCard();
        };
    }

    private void DeleteCurrentCard()
    {
        _items[CurrentIndex].GetComponent<CharacterSlotController>()
            .deleteBtn.onClick.RemoveAllListeners();
        _gameObjectPool.Enqueue(_items[CurrentIndex]);

        _items.RemoveAt(CurrentIndex);

        if (CurrentIndex < _items.Count)
        {
            _items[CurrentIndex].GetComponent<RectTransform>().DOAnchorPos(
                center.anchoredPosition, speed);
            _items[CurrentIndex].GetComponent<RectTransform>().DOScale(max, speed);
            _items[CurrentIndex].GetComponent<CanvasGroup>().alpha = 1;
            _items[CurrentIndex].transform.SetAsLastSibling();
            //this item is selected 

            if (CurrentIndex == _items.Count - 1)
            {
                //the lasting item 
                _items[CurrentIndex].GetComponent<CharacterEmptySlotController>().addBtn.onClick
                    .AddListener(OnAddOneCard);
            }
            else
            {
                _items[CurrentIndex].GetComponent<CharacterSlotController>()
                    .deleteBtn.onClick.AddListener(OnDeleteOneCard);
            }
        }

        if (CurrentIndex + 1 < _items.Count)
        {
            _items[CurrentIndex + 1].GetComponent<RectTransform>().DOAnchorPos(
                right1.anchoredPosition, speed);
            _items[CurrentIndex + 1].GetComponent<RectTransform>().DOScale(smaller, speed);
            _items[CurrentIndex + 1].GetComponent<CanvasGroup>().alpha = alpha;
            _items[CurrentIndex + 1].transform.SetAsFirstSibling();
        }

        //judge whether can appear new item
        if (CurrentIndex + 2 < _items.Count)
        {
            _items[CurrentIndex + 2].SetActive(true);
            _items[CurrentIndex + 2].GetComponent<RectTransform>().anchoredPosition =
                right2.anchoredPosition;
            _items[CurrentIndex + 2].GetComponent<RectTransform>().DOScale(min, speed);
            _items[CurrentIndex + 2].GetComponent<CanvasGroup>().alpha = alpha;

            _items[CurrentIndex + 2].transform.SetAsFirstSibling();
        }
        
        //delete truly 
        MysqlTool.DeleteCharacterInfo(GameFacade.Instance.GetAccount(), _characterInfos[CurrentIndex].Id);
        
        _characterInfos.RemoveAt(CurrentIndex);
    }

    private void OnAddOneCard()
    {
        //open add panel
        var operationPanel = GameFacade.Instance.GetInstance<IUIkit>().OpenPanel("AddHeroPanel")
            .GetComponent<IOperationPanel>();


        operationPanel.Result = variable =>
        {
            //after the choice and create the new card for hero

            CreateNewCard(((SelectHeroName) variable).heroName);
        };
    }


    private void CreateNewCard(string heroName)
    {
        //create a instance for the new characterinfo
        CharacterInfo info = new CharacterInfo()
        {
            Name = heroName,
            Exp = "0",
            Account = GameFacade.Instance.GetAccount()
        };

        //add the new hero to database
        MysqlTool.AddCharacter(info.Name, info.Account);
        //update the current character info
        _characterInfos = MysqlTool.GetCharactersByAccount<CharacterInfo>(GameFacade.Instance.GetAccount());
        GameFacade.Instance.SetCharacterList(_characterInfos);
        //move the current card to right1 and before card to left
        _items[CurrentIndex].GetComponent<RectTransform>().DOAnchorPos(
            right1.anchoredPosition, speed);
        _items[CurrentIndex].GetComponent<RectTransform>().DOScale(smaller, speed);
        _items[CurrentIndex].GetComponent<CanvasGroup>().alpha = alpha;
        _items[CurrentIndex].transform.SetAsFirstSibling();

        //register the event 
        _items[CurrentIndex].GetComponent<CharacterEmptySlotController>()
            .addBtn.onClick.RemoveAllListeners();


        //create the new card
        var characterCard = _gameObjectPool.Dequeue("Slot_CharacterSelect", content);
        characterCard.GetComponent<RectTransform>().anchoredPosition = center.anchoredPosition;

        var characterSlotController = characterCard.GetComponent<CharacterSlotController>();


        //set the img
        characterSlotController.SetCharacterImg(_assetFactory.LoadAsset<Sprite>(info.GetSpriteName()));
        characterSlotController.SetCharacterName(info.Name);
        characterSlotController.SetCharacterLevel(Int32.Parse(info.Exp));

        //add to list
        _items.Insert(CurrentIndex, characterCard);

        //register event for this card
        _items[CurrentIndex].GetComponent<CharacterSlotController>()
            .deleteBtn.onClick.AddListener(OnDeleteOneCard);
    }

    private void OnEnable()
    {
    }
}