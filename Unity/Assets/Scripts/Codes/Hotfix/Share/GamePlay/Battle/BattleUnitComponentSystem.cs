﻿using System.Collections.Generic;
namespace ET
{
    [ObjectSystem]
    public class BattleUnitAwakeSystem : AwakeSystem<BattleUnitComponent,List<int>>
    {
        protected override void Awake(BattleUnitComponent self,List<int> skills)
        {
            
        }
    }
    [ObjectSystem]
    public class BattleUnitAwakeSystem1 : AwakeSystem<BattleUnitComponent>
    {
        protected override void Awake(BattleUnitComponent self)
        {
            
        }
    }
    [ObjectSystem]
    public class BattleUnitDestroySystem : DestroySystem<BattleUnitComponent>
    {
        protected override void Destroy(BattleUnitComponent self)
        {
            self.IdSkillMap.Clear();
        }
    }
    [FriendOf(typeof(BattleUnitComponent))]
    public static class BattleUnitComponentSystem
    {
        /// <summary>
        /// 添加技能
        /// </summary>
        /// <param name="self"></param>
        /// <param name="configId"></param>
        /// <returns></returns>
        public static Skill AddSkill(this BattleUnitComponent self,int configId, int skillLevel = 0)
        {
            if (!self.IdSkillMap.ContainsKey(configId))
            {
                var skill = self.AddChild<Skill, int, int>(configId, skillLevel);
                self.IdSkillMap.Add(configId, skill.Id);
            }
            return self.GetChild<Skill>(self.IdSkillMap[configId]);
        }

        public static bool TryGetSkill(this BattleUnitComponent self, int configId,out Skill skill)
        {
            if (self.IdSkillMap.ContainsKey(configId))
            {
                skill = self.GetChild<Skill>(self.IdSkillMap[configId]);
                return true;
            }
            skill = null;
            return false;
        }
    }
}