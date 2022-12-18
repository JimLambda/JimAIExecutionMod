using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JimAIExecutionMod
{
    internal static class ReadConfigurationFromOuterFile
    {
        public static Boolean doYouWantGlobalExecutionProbability = false;
        public static Double globalExecutionProbability = 100;
        public static Boolean doYouWantRelationFactor = false;
        public static Double relationFactor = 0;
        public static Boolean doYouWantTraitAffect = false;
        public static Double traitLevelFactorMercy = 0;
        public static Double traitLevelFactorHonor = 0;
        public static Double traitLevelFactorGenerosity = 0;
        public static Double traitLevelFactorCalculating = 0;


        public static void ReadDataFromConfigurationFile()
        {
            string currentExecutingAssemblyLocation = Assembly.GetExecutingAssembly().Location;
            string configurationFileDirectoryPath = new DirectoryInfo(currentExecutingAssemblyLocation).Parent.Parent.Parent.FullName;
            string configurationFilePath = Path.Combine(configurationFileDirectoryPath, "JimAIExecutionModConfiguration.txt");
            if (File.Exists(configurationFilePath))
            {
                foreach (string readLine in File.ReadLines(configurationFilePath))
                {
                    if (!readLine.StartsWith("#") && readLine.Length > 1)
                    {
                        string[] stringArray = readLine.Split('=');
                        if (stringArray.Length == 2 && Double.TryParse(stringArray[1], out _))
                        {
                            int tempInteger = 0;
                            Double tempDouble = 0;
                            switch (stringArray[0])
                            {
                                case "DoYouWantGlobalExecutionProbability":
                                    if (int.TryParse(stringArray[1], out tempInteger))
                                    {
                                        if (tempInteger == 1)
                                        {
                                            doYouWantGlobalExecutionProbability = true;
                                        }
                                    }
                                    break;
                                case "GlobalExecutionProbability":
                                    if (Double.TryParse(stringArray[1], out tempDouble))
                                    {
                                        globalExecutionProbability = tempDouble;
                                    }
                                    break;
                                case "DoYouWantRelationFactor":
                                    if (int.TryParse(stringArray[1], out tempInteger))
                                    {
                                        if (tempInteger == 1)
                                        {
                                            doYouWantRelationFactor = true;
                                        }
                                    }
                                    break;
                                case "RelationFactor":
                                    if (Double.TryParse(stringArray[1], out tempDouble))
                                    {
                                        relationFactor = tempDouble;
                                    }
                                    break;
                                case "DoYouWantTraitAffect":
                                    if (int.TryParse(stringArray[1], out tempInteger))
                                    {
                                        if (tempInteger == 1)
                                        {
                                            doYouWantTraitAffect = true;
                                        }
                                    }
                                    break;
                                case "TraitLevelFactorMercy":
                                    if (Double.TryParse(stringArray[1], out tempDouble))
                                    {
                                        traitLevelFactorMercy = tempDouble;
                                    }
                                    break;
                                case "TraitLevelFactorHonor":
                                    if (Double.TryParse(stringArray[1], out tempDouble))
                                    {
                                        traitLevelFactorHonor = tempDouble;
                                    }
                                    break;
                                case "TraitLevelFactorGenerosity":
                                    if (Double.TryParse(stringArray[1], out tempDouble))
                                    {
                                        traitLevelFactorGenerosity = tempDouble;
                                    }
                                    break;
                                case "TraitLevelFactorCalculating":
                                    if (Double.TryParse(stringArray[1], out tempDouble))
                                    {
                                        traitLevelFactorCalculating = tempDouble;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }


        public static void SetConfigurationDataToDefault()
        {
            doYouWantGlobalExecutionProbability = false;
            globalExecutionProbability = 100;
            doYouWantRelationFactor = false;
            relationFactor = 0;
            doYouWantTraitAffect = false;
            traitLevelFactorMercy = 0;
            traitLevelFactorHonor = 0;
            traitLevelFactorGenerosity = 0;
            traitLevelFactorCalculating = 0;
        }
    }
}
