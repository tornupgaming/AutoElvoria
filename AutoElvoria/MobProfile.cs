using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoElvoria
{
    class MobProfile
    {
        public int level;
        public int id;
        public string name;

        public static MobProfile GetMobFromID(int id)
        {
            foreach (MobProfile mob in Mobs)
            {
                if (mob.id == id)
                    return mob;
            }
            return null;
        }

        /// <summary>
        /// Gets a mob of the specified level (or lower if none exists)
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static MobProfile GetMobByLevel(int level)
        {
            MobProfile nextBest = Mobs[0];
            for (int i = 1; i < Mobs.Length; i++)
            {
                if (Mobs[i].level < level)
                {
                    if (Mobs[i].level > nextBest.level)
                    {
                        nextBest = Mobs[i];
                    }
                }
                if (Mobs[i].level == level)
                    return Mobs[i];
            }
            return nextBest;
        }

        public static MobProfile[] Mobs = new MobProfile[] {
            new MobProfile() {level = 1,    id = 1,     name = "Rat"}, // 2500 exp p/h
            new MobProfile() {level = 4,    id = 2,     name = "Stray Dog"}, // 5000 exp p/h
            new MobProfile() {level = 6,    id = 3,     name = "Old Beggar"},
            new MobProfile() {level = 8,    id = 42,    name = "Janitor "},
            new MobProfile() {level = 10,   id = 62,    name = "Snake"},
            new MobProfile() {level = 12,   id = 4,     name = "Rogue Bandit"},
            new MobProfile() {level = 14,   id = 63,    name = "Praying Mantis"}, // 40k
            new MobProfile() {level = 16,   id = 5,     name = "Venomous Snake"}, // 40k
            new MobProfile() {level = 18,   id = 6,     name = "Evil Entling"}, // 40k
            new MobProfile() {level = 20,   id = 8,     name = "Evil Ent"}
        };
    }

}
