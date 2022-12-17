using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace JimAIExecutionMod
{
    internal class JimAIExecutionBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<TaleWorlds.CampaignSystem.Party.PartyBase, Hero>(
                (captureParty, prisoner) =>
                {
                    try
                    {
                        if (!(prisoner == Hero.MainHero || captureParty.LeaderHero == Hero.MainHero))
                        {
                            KillCharacterAction.ApplyByExecution(prisoner, captureParty.LeaderHero, true);
                            //// The following line will create an execution scene, just like when player(the main hero) is executing a hero.
                            //MBInformationManager.ShowSceneNotification(HeroExecutionSceneNotificationData.CreateForInformingPlayer(captureParty.LeaderHero, prisoner, SceneNotificationData.RelevantContextType.Map));
                            MBInformationManager.AddQuickInformation(new CharacterKilledLogEntry(prisoner, captureParty.LeaderHero, KillCharacterAction.KillCharacterActionDetail.Executed).GetNotificationText(), 0, prisoner.CharacterObject, "event:/ui/notification/death");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        InformationManager.DisplayMessage(new InformationMessage(ex.Message));
                    }
                }
                ));
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}
