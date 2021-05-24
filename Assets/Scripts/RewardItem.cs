using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class RewardItem : RecyclingListViewItem
{
    [SerializeField] private Image rewardItemImage;
    [SerializeField] private Button getRewardButton;
    [SerializeField] private Text rankNumber;
    [SerializeField] private LeaguePanel leaguePanel;
    public Button rewardItembutton;
    public Image rewardItemBackGroundImage;
    public RewardItem rewardItem;

    public struct RewardChildData
    {
        public int Rank;
        
        public RewardChildData(int rank)
        {
            Rank = rank;
        }
    }

    private RewardChildData rewardData;

    public RewardChildData RewardData
    {
        get { return rewardData; }
        set
        {
            rewardData = value;
            rankNumber.text = rewardData.Rank.ToString();
        }
    }

    /// <summary>
    /// 点击奖励Item将调用此方法，根据用户排名分数判断是否可以领取奖励
    /// </summary>
    public void AddCoins()
    {
        if (rewardData.Rank <= leaguePanel.rankScore)
        {
            leaguePanel.AddCoins();
            rewardItemImage.color /= 2;
            getRewardButton.enabled = false;
        }
    }
}
