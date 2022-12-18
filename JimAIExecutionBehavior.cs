using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace JimAIExecutionMod
{
    internal class JimAIExecutionBehavior : CampaignBehaviorBase
    {
        Random random = new Random();

        public override void RegisterEvents()
        {
            CampaignEvents.HeroPrisonerTaken.AddNonSerializedListener(this, new Action<TaleWorlds.CampaignSystem.Party.PartyBase, Hero>(
                (captureParty, prisoner) =>
                {
                    try
                    {
                        ReadConfigurationFromOuterFile.SetConfigurationDataToDefault();
                        ReadConfigurationFromOuterFile.ReadDataFromConfigurationFile();

                        if (!(prisoner == Hero.MainHero || captureParty.LeaderHero == Hero.MainHero || captureParty.LeaderHero is null))
                        {
                            Double executionProbability = 100;

                            if (ReadConfigurationFromOuterFile.doYouWantGlobalExecutionProbability)
                            {
                                executionProbability = ReadConfigurationFromOuterFile.globalExecutionProbability;
                            }

                            if (ReadConfigurationFromOuterFile.doYouWantRelationFactor)
                            {
                                int leaderRelationTowardsPrisoner = captureParty.LeaderHero.GetRelation(prisoner);
                                executionProbability = executionProbability - leaderRelationTowardsPrisoner * ReadConfigurationFromOuterFile.relationFactor;
                            }

                            if (ReadConfigurationFromOuterFile.doYouWantTraitAffect)
                            {
                                int leaderMercyTraitLevel = captureParty.LeaderHero.GetTraitLevel(DefaultTraits.Mercy);
                                int leaderHonorTraitLevel = captureParty.LeaderHero.GetTraitLevel(DefaultTraits.Honor);
                                int leaderGenerosityTraitLevel = captureParty.LeaderHero.GetTraitLevel(DefaultTraits.Generosity);
                                int leaderCalculatingTraitLevel = captureParty.LeaderHero.GetTraitLevel(DefaultTraits.Calculating);

                                executionProbability = executionProbability - leaderMercyTraitLevel * ReadConfigurationFromOuterFile.traitLevelFactorMercy - leaderHonorTraitLevel * ReadConfigurationFromOuterFile.traitLevelFactorHonor - leaderGenerosityTraitLevel * ReadConfigurationFromOuterFile.traitLevelFactorGenerosity - leaderCalculatingTraitLevel * ReadConfigurationFromOuterFile.traitLevelFactorCalculating;
                            }

                            Double randomDoubleBetween0And100 = random.NextDouble() * 100;
                            if (randomDoubleBetween0And100 < executionProbability)
                            {
                                KillCharacterAction.ApplyByExecution(prisoner, captureParty.LeaderHero, true);
                                //// The following line will create an execution scene, just like when player(the main hero) is executing a hero.
                                //MBInformationManager.ShowSceneNotification(HeroExecutionSceneNotificationData.CreateForInformingPlayer(captureParty.LeaderHero, prisoner, SceneNotificationData.RelevantContextType.Map));
                                MBInformationManager.AddQuickInformation(new CharacterKilledLogEntry(prisoner, captureParty.LeaderHero, KillCharacterAction.KillCharacterActionDetail.Executed).GetNotificationText(), 0, prisoner.CharacterObject, "event:/ui/notification/death");
                            }

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
