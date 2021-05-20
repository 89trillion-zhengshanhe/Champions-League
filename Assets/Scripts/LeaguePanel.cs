using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LeaguePanel : MonoBehaviour
{
    [SerializeField] private RecyclingListView theList;
    [SerializeField] private GameObject leaguePanel;
    [SerializeField] private Text rankScoreText;
    [SerializeField] private Image rankIcon;
    [SerializeField] private Text leagueNameText;
    [SerializeField] private List<Sprite> rankIconImage;
    [SerializeField] private Text rankName;
    [SerializeField] private GameObject rewardItemPrefab;
    [SerializeField] private GameObject rewardScrollViewContent;
    [SerializeField] private Text totalCoinsText;
    [SerializeField] private GameObject rewardItemParent;
    private List<RewardItem.RewardChildData> data = new List<RewardItem.RewardChildData>();
    private List<string> rankNameList = new List<string>() {"青铜", "白银", "黄金"};
    private string leagueName = "联赛";
    private int leagueSeasonNumber = 1;
    private bool isActive = true;
    private int addScore = 100;
    private int MaxScore = 6000;
    private int StandardScore = 4000;
    public int rankScore = 3900;

    private void Start()
    {
        leagueNameText.text = leagueName + leagueSeasonNumber++.ToString();
        rankScoreText.text = rankScore.ToString();
        CreateRewardItemData();
        theList.ItemCallback = PopulateItem;
        theList.RowCount = data.Count;
    }

    private void PopulateItem(RecyclingListViewItem item, int rowIndex)
    {
        var child = item as RewardItem;
        child.RewardData = data[rowIndex];
    }

    private void CreateRewardItemData()
    {
        for (int rankScore = StandardScore; rankScore <= MaxScore; rankScore += 200)
        {
            if (rankScore % 1000 != 0 && rankScore % 200 == 0)
            {
                data.Add(new RewardItem.RewardChildData(rankScore));
            }
        }
    }

    private void ShowRankIcon(int score)
    {
        rankIcon.gameObject.SetActive(true);
        rankName.gameObject.SetActive(true);
        rankIcon.preserveAspect = true;
        if (score >= 4000 && score < 5000)
        {
            rankIcon.sprite = rankIconImage[0];
            rankName.text = rankNameList[0];
        }
        else if (score >= 5000 && score < MaxScore)
        {
            rankIcon.sprite = rankIconImage[1];
            rankName.text = rankNameList[1];
        }
        else if (score == MaxScore)
        {
            rankIcon.sprite = rankIconImage[2];
            rankName.text = rankNameList[2];
        }
        else
        {
            rankIcon.gameObject.SetActive(false);
            rankName.gameObject.SetActive(false);
        }
    }

    public void ShowRank()
    {
        leaguePanel.SetActive(!leaguePanel.activeSelf);
    }

    public void AddRankScore()
    {
        if (rankScore < MaxScore)
        {
            rankScore += addScore;
            if (rankScore > MaxScore)
            {
                rankScore = MaxScore;
            }
            ShowRankIcon(rankScore);
            rankScoreText.text = rankScore.ToString();
        }
    }

    public void RefeshLeague()
    {
        leagueNameText.text = leagueName + leagueSeasonNumber++.ToString();
        if (rankScore > 4000)
        {
            rankScore = StandardScore + (rankScore - StandardScore) / 2;
            ShowRankIcon(rankScore);
            rankScoreText.text = rankScore.ToString();
        }
        foreach (Transform child in rewardItemParent.transform)
        {
            Button getButton = child.GetComponent<Button>();
            if (!getButton.enabled)
            {
                child.Find("BackGround").GetComponent<Image>().color *= 2;
                getButton.enabled = true;
            }
        }
    }

    public void AddCoins(int number = 100)
    {
        int newCoins = Int32.Parse(totalCoinsText.text);
        totalCoinsText.text = (newCoins + number).ToString();
    }
}