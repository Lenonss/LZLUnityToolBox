using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LZLUnityTool.Plugins.InvntoryPlugin.System.TaskSystem.Scripts.UI
{
[RequireComponent(typeof(Image), typeof(CanvasGroup))]
public class QuestFlag : MonoBehaviour
{
    private Image icon;
    private RectTransform iconRectTransform;
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Sprite notAccepted;
    [SerializeField]
    private Sprite accepted;
    [SerializeField]
    private Sprite complete;

    // private Talker questHolder;
    // private MapIcon mapIcon;

    private readonly HashSet<string> triggerNames = new HashSet<string>();

    public void Init(/*Talker questHolder*/)
    {
        // this.questHolder = questHolder;
        // if (MapManager.Instance)
        // {
        //     if (mapIcon) MapManager.Instance.RemoveMapIcon(mapIcon, true);
        //     mapIcon = MapManager.Instance.CreateMapIcon(notAccepted, Vector2.one * 48, questHolder.GetData<TalkerData>().currentPosition, false, MapIconType.Quest, false);
        //     mapIcon.iconImage.raycastTarget = false;
        //     mapIcon.Hide();
        // }
        // triggerNames.Clear();
        // foreach (var quest in questHolder.QuestInstances)
        // {
        //     Condition find = quest.Info.AcceptCondition.Conditions.Find(x => x.Type == ConditionType.TriggerSet || x.Type == ConditionType.TriggerReset);
        //     if (find) triggerNames.Add(find.TriggerName);
        // }
        // UpdateUI();
        // Update();
        // if (QuestManager.Instance) QuestManager.Instance.OnQuestStatusChange += UpdateUI;
        //
        // NotifyCenter.Instance.RemoveListener(this);
        // NotifyCenter.Instance.AddListener("TriggerChange", OnTriggerChange);
    }

    private bool conditionShow;
    public void UpdateUI()
    {
        // //Debug.Log(questHolder.TalkerName);
        // bool hasObjective = questHolder.GetData<TalkerData>().objectivesTalkToThis.FindAll(x => x.AllPrevObjCmplt && !x.IsComplete).Count > 0
        //     || questHolder.GetData<TalkerData>().objectivesSubmitToThis.FindAll(x => x.AllPrevObjCmplt && !x.IsComplete).Count > 0;
        // if (questHolder.QuestInstances.Count < 1 && !hasObjective)
        // {
        //     if (icon.enabled) icon.enabled = false;
        //     mapIcon.Hide();
        //     conditionShow = false;
        //     return;
        // }
        // //Debug.Log("enter");
        // if (hasObjective)//???NPC?????????????????????????????????
        // {
        //     icon.overrideSprite = accepted;
        //     mapIcon.iconImage.overrideSprite = accepted;
        //     conditionShow = true;
        //     return;
        // }
        // foreach (var quest in questHolder.QuestInstances)
        // {
        //     if (!quest.IsComplete && !quest.InProgress && MiscFuntion.CheckCondition(quest.Info.AcceptCondition))//????????????????????????
        //     {
        //         icon.overrideSprite = notAccepted;
        //         mapIcon.iconImage.overrideSprite = notAccepted;
        //         conditionShow = true;
        //         return;
        //     }
        //     else if (quest.IsComplete && quest.InProgress)//?????????????????????
        //     {
        //         icon.overrideSprite = complete;
        //         mapIcon.iconImage.overrideSprite = complete;
        //         conditionShow = true;
        //         return;
        //     }
        // }
        // conditionShow = false;
    }

    private void CheckDistance()
    {
        // Vector3 viewportPoint = Camera.main.WorldToViewportPoint(questHolder.transform.position + questHolder.questFlagOffset);
        // float sqrDistance = Vector3.SqrMagnitude(Camera.main.transform.position - questHolder.Position);
        // if (viewportPoint.z <= 0 || viewportPoint.x > 1 || viewportPoint.x < 0 || viewportPoint.y > 1 || viewportPoint.y < 0 || sqrDistance > 900f)
        // {
        //     if (icon.enabled) icon.enabled = false;
        // }
        // else if (questHolder.isActiveAndEnabled && conditionShow)
        // {
        //     if (!icon.enabled) icon.enabled = true;
        //     Vector2 position = new Vector2(Screen.width * viewportPoint.x, Screen.height * viewportPoint.y);
        //     iconRectTransform.position = position;
        //     if (sqrDistance > 625 && sqrDistance <= 900)
        //     {
        //         float percent = (900 - sqrDistance) / 275;
        //         canvasGroup.alpha = percent;
        //         iconRectTransform.localScale = new Vector3(percent, percent, 1);
        //     }
        //     else
        //     {
        //         canvasGroup.alpha = 1;
        //         iconRectTransform.localScale = Vector3.one;
        //     }
        // }
        // else
        // {
        //     if (icon.enabled) icon.enabled = false;
        // }
        // transform.position = new Vector3(viewportPoint.x * Screen.width, viewportPoint.y * Screen.height, 0);
    }

    public void Recycle()
    {
        // if (NotifyCenter.Instance) NotifyCenter.Instance.RemoveListener(this);
        // if (MapManager.Instance) MapManager.Instance.RemoveMapIcon(mapIcon, true);
        // questHolder = null;
        // mapIcon = null;
        // ObjectPool.Put(gameObject);
    }

    public void OnTriggerChange(params object[] args)
    {
        if (args.Length > 1)
        {
            if (triggerNames.Contains(args[0].ToString()))
                UpdateUI();
        }
    }

    void Awake()
    {
        icon = GetComponent<Image>();
        iconRectTransform = icon.rectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
        if (!canvasGroup) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
    }

    void Update()
    {
        // if (questHolder)
        // {
        //     CheckDistance();
        //     if (questHolder.isActiveAndEnabled && conditionShow)
        //     {
        //         mapIcon.UpdatePosition(questHolder.transform.position);
        //         mapIcon.Show();
        //     }
        //     else mapIcon.Hide();
        // }
    }

    private void OnDestroy()
    {
        // if (MapManager.Instance) MapManager.Instance.RemoveMapIcon(mapIcon);
        // if (QuestManager.Instance) QuestManager.Instance.OnQuestStatusChange -= UpdateUI;
        // if (NotifyCenter.Instance) NotifyCenter.Instance.RemoveListener(this);
    }
}
}