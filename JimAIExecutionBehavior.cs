using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;

namespace JimAIExecutionMod
{
    internal class JimAIExecutionBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<TaleWorlds.CampaignSystem.Party.PartyBase, Hero>(
                (captureParty, prisoner) =>
                {
                    KillCharacterAction.ApplyByExecution(prisoner, captureParty.LeaderHero, true);
                }
                ));
        }

        public override void SyncData(IDataStore dataStore)
        {
        }
    }
}
