using System;
using System.Linq;
using System.Collections.Generic;
using Oathsworn.Entities;
using Oathsworn.Models;
using Oathsworn.Extensions;

namespace Oathsworn.Business.Bosses
{
    public class BossMapping
    {
        public int RelativeXPosition { get; set; }
        public int RelativeYPosition { get; set; }
        public Direction? Direction { get; set; }
        public Corner? Corner { get; set; }
        public (BossPart, int) BossPart { get; set; }
        public BossPart? Break { get; set; }
    }

    public class ActionComponent
    {
        public BossActionType Type { get; set; }
    }

    public class MoveActionComponent : ActionComponent
    {
        public string ExtraEffect { get; set; }
        public Direction? Direction { get; set; }
        public int Spaces { get; set; }
    }

    public class AttackActionComponent : ActionComponent
    {
        public BossPart BossPart { get; set; }
        public int Range { get; set; }
        public string ExtraEffect { get; set; }
    }

    public class SpecialActionComponent : ActionComponent
    {
        public int Parameter { get; set; }
    }

    public class CustomActionComponent : ActionComponent
    {
        public string ExtraEffect { get; set; }
    }

    public class Action
    {
        public int Number { get; set; }
        public int Stage { get; set; }
        public string Name { get; set; }
        public List<ActionComponent> Components { get; set; }
    }

    public abstract class AbstractBoss
    {
        public AbstractBoss(Boss bossEntity)
        {
            BossEntity = bossEntity;
        }

        protected Boss BossEntity { get; init; }
        protected abstract List<BossMapping> Mappings { get; }

        public List<BorderedPosition> GetBossPositions()
        {
            return Mappings.Select(
                m => new BorderedPosition() { XPosition = BossEntity.XPosition + m.RelativeXPosition, YPosition = BossEntity.YPosition + m.RelativeYPosition, Direction = m.Direction, Corner = m.Corner }
            ).ToList();
        }

        public (BossPart, int) GetBossPartFromPosition(IPosition position)
        {
            return Mappings.Where(m =>
                m.RelativeXPosition == BossEntity.XPosition - position.XPosition &&
                m.RelativeYPosition == BossEntity.YPosition - position.YPosition
            ).Single().BossPart;
        }

        public bool IsBroken(BossPart bossPart)
        {
            return Mappings.Where(m => m.Break == bossPart).Any(m => BossEntity.Health[m.BossPart.ConvertToString()] <= 0);
        }

        public Dictionary<Might, int> GetMight(BossPart bossPart)
        {
            if (!IsBroken(bossPart))
            {
                return BossEntity.Might;
            }

            var deductedFromHighest = false;
            var bossMight = new Dictionary<Might, int>();

            foreach (var might in new List<Might>() { Might.Black, Might.Red, Might.Yellow, Might.White })
            {
                if (!deductedFromHighest && BossEntity.Might[might] > 0)
                {
                    bossMight[might] = BossEntity.Might[might] - 1;
                    deductedFromHighest = true;
                }
                bossMight[might] = BossEntity.Might[might];
            }
            return bossMight;
        }

        public void PerformMove(Direction direction, int distance)
        {
            // Stop at edge of board 
            // Destroy obstacles 
            // Displace Players
        }

        public void PerformAttack(List<int> targets)
        {

        }

        public abstract (int, int) GetDefaultTarget();
        public abstract int GetStage();
        public abstract string GetActionText(int number);
        public abstract void PerformAction(int number);

        public static Boss GetBossEntityByNumber(int number)
        {
            return number switch
            {
                1 => BroodMother.GetBossEntity(),
                _ => throw new NotImplementedException()
            };
        }
    }
}