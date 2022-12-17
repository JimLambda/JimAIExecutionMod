using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
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
                        if (!(prisoner == Hero.MainHero))
                        {
                            KillCharacterAction.ApplyByExecution(prisoner, captureParty.LeaderHero, true);
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
