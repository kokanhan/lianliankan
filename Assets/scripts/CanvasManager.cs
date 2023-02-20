using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardCollectionSO cardCollection;

    public GameDataSO easyDatas;
    //unused
    public GameDataSO normalDatas;
    public GameDataSO hardDatas;

    GameDataSO gameDatas;
    List<CardController> cardControllers;
    CardGridGenerator cardGridGenerator;

    private void Awake()
    {
        cardControllers = new List<CardController>();  
        GetGameDatasDifficulty();

        cardGridGenerator = new CardGridGenerator(cardCollection, gameDatas);

        SetCardGridLayoutParams();
        GenerateCards();

        GameManager gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        gameManager.CardCount = gameDatas.rows * gameDatas.columns;

    }

    private void GenerateCards()
    {
        int cardCount = gameDatas.rows * gameDatas.columns;
        for(int i = 0; i < cardCount; i++)
        {
            GameObject card = Instantiate(cardPrefab, this.transform);
            card.transform.name = "Card (" + i.ToString() + ")";
            cardControllers.Add(card.GetComponent<CardController>());
        }

        //card乱序一下
        for (int i = 0; i < cardCount / 2; i++)
        {
            CardSO randomCard = cardGridGenerator.GetRandomAvailableCardSO();

            SetRandomCardToGrid(randomCard);

            CardSO randomCardPair = cardGridGenerator.GetCardPairSO(randomCard.cardName);
            SetRandomCardToGrid(randomCardPair);
        }
    }

    private void SetRandomCardToGrid(CardSO randomCard)
    {
        int index = cardGridGenerator.GetRandomCardPositionIndex();
        CardController cardObject = cardControllers[index];
        cardObject.SetCardDatas(gameDatas.background, randomCard);
    }

    private void SetCardGridLayoutParams()
    {
        CardGridLayout cardGridLayout = this.GetComponent<CardGridLayout>();

        cardGridLayout.preferredTopPadding = gameDatas.preferredTopBottomPadding;
        cardGridLayout.rows = gameDatas.rows;
        cardGridLayout.columns = gameDatas.columns;
        cardGridLayout.spacing = gameDatas.spacing;

        cardGridLayout.Invoke("CalculateLayoutInputVertical", 0.1f);//不知道为啥要invoke一下
    }

    private void GetGameDatasDifficulty()
    {
        Difficulty difficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty", (int)Difficulty.NORMAL);
        switch (difficulty)
        {
            case Difficulty.EASY:
                gameDatas = easyDatas;
                break;
            case Difficulty.NORMAL:
                gameDatas = normalDatas;
                break;
            case Difficulty.HARD:
                gameDatas = hardDatas;
                break;
        }
    }

}
