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
    [SerializeField] private Text totalCoinsText;
    [SerializeField] private GameObject rewardItemParent;
    private List<RewardItem.RewardChildData> data = new List<RewardItem.RewardChildData>();
    private List<string> rankNameList = new List<string>() {"青铜", "白银", "黄金"};
    private string leagueName = "联赛";
    private int leagueSeasonNumber = 1;
    private int addScore = 100;
    private int MaxScore = 6000;
    private int StandardScore = 4000;
    public int rankScore = 3900;
    public int userCoins = 0;

    private void Start()
    {
        leagueNameText.text = leagueName + leagueSeasonNumber++;
        rankScoreText.text = rankScore.ToString();
        CreateRewardItemData();
        theList.ItemCallback = PopulateItem;
        theList.RowCount = data.Count;
    }

    /// <summary>
    /// 在ScrollView生成可复用的奖励Item
    /// </summary>
    private void PopulateItem(RecyclingListViewItem item, int rowIndex)
    {
        var child = item as RewardItem;
        child.RewardData = data[rowIndex];
    }

    /// <summary>
    /// 根据规则生成奖励Item数据
    /// </summary>
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

    /// <summary>
    /// 根据Rank分数显示对应的段位名字以及图标
    /// </summary>
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

    /// <summary>
    /// 点击增加分数按钮调用此方法，每点击一次增加100分并且根据分数检查段位图标
    /// </summary>
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

    /// <summary>
    /// 刷新赛季按钮调用此方法，每点击一次更新赛季名字以及重制Rank分数和可领取奖励
    /// </summary>
    public void RefeshLeague()
    {
        leagueNameText.text = leagueName + leagueSeasonNumber++;
        if (rankScore > 4000)
        {
            rankScore = StandardScore + (rankScore - StandardScore) / 2;
            ShowRankIcon(rankScore);
            rankScoreText.text = rankScore.ToString();
        }
        RewardItem[] childRewardItems = rewardItemParent.GetComponentsInChildren<RewardItem>();
        foreach (var child in childRewardItems)
        {
            if (!child.rewardItembutton.enabled)
            {
                child.rewardItemBackGroundImage.color *= 2;
                child.rewardItembutton.enabled = true;
            }
        }
        // action   func
    }

    /// <summary>
    /// 如果用户符合领取奖励所需要的Rank分数则增加相应的金币
    /// </summary>
    public void AddCoins(int number = 100)
    {
        userCoins += number;
        totalCoinsText.text = userCoins.ToString();
    }
}