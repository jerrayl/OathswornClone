using System;
using System.Linq;
using System.Collections.Generic;
using Oathsworn.Entities;
using Oathsworn.Models;
using Oathsworn.Extensions;
using Oathsworn.Business.Helpers;

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
        public Template? Template { get; set; }
        public int Size { get; set; }
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
        public AbstractBoss(Boss bossEntity, IBossDependencies bossDependencies)
        {
            BossEntity = bossEntity;
            BossDependencies = bossDependencies;
        }

        protected IBossDependencies BossDependencies { get; init; }
        protected Boss BossEntity { get; init; }
        protected abstract List<BossMapping> Mappings { get; }
        protected abstract Size Size { get; }

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

        protected int GetNearestPlayer()
        {
            var players = BossDependencies.EncounterPlayers.Read(x => x.EncounterId == BossEntity.EncounterId);
            var nearestPlayer = GridHelper.GetNearest(BossEntity, players) ?? throw new Exception("No player could be found");
            return ((EncounterPlayer)nearestPlayer).Id;
        }

        public void BeginAction()
        {
            if (BossEntity.ActionComponentIndex is not null)
            {
                throw new Exception("Invalid Boss Action Component Index");
            }

            var encounter = BossDependencies.Encounters.ReadOne(x => x.Id == BossEntity.EncounterId);
            encounter.CharacterPerformingAction = CharacterType.Boss;

            if (BossEntity.TargetId is null) // Not a retailiation
            {
                BossEntity.TargetId = GetDefaultTarget();
            }
            BossEntity.ActionComponentIndex = 0;

            BossDependencies.Bosses.Update(BossEntity);
            BossDependencies.Encounters.Update(encounter);

            PerformAction();
        }

        public void PerformAction()
        {
            var action = GetAction(GetNextAction().Number);
            if (BossEntity.ActionComponentIndex < action.Components.Count)
            {
                EndAction();
            }
            else
            {
                PerformActionComponent(action.Components[BossEntity.ActionComponentIndex.Value]);
            }
        }

        private void EndAction()
        {
            var encounter = BossDependencies.Encounters.ReadOne(x => x.Id == BossEntity.EncounterId);

            if (encounter.CharacterPerformingAction != CharacterType.Boss)
            {
                throw new Exception("Invalid character performing action");
            }

            encounter.CharacterPerformingAction = null;

            BossEntity.TargetId = null;
            BossEntity.ActionComponentIndex = null;

            BossDependencies.Encounters.Update(encounter);
            BossDependencies.Bosses.Update(BossEntity);
            BossDependencies.BossActions.Delete(GetNextAction());
        }

        public void PerformActionComponent(ActionComponent actionComponent)
        {
            switch (actionComponent.Type)
            {
                case BossActionType.Move:
                    PerformMove((MoveActionComponent)actionComponent);
                    break;
                case BossActionType.Attack:
                    PerformAttack((AttackActionComponent)actionComponent);
                    break;
                case BossActionType.Special:
                    PerformSpecialAction((SpecialActionComponent)actionComponent);
                    break;
                case BossActionType.Custom:
                    PerformCustomAction((CustomActionComponent)actionComponent);
                    break;
            }
        }

        public void PerformMove(MoveActionComponent actionComponent)
        {
            //if target exists, move towards target, stopping at target
            //if no target, move to direction, stopping at edge of board 
            // Destroy obstacles 
            // Displace Players
        }

        public void PerformAttack(AttackActionComponent actionComponent)
        {
            // Assume single target
            var targets = new List<int> { BossEntity.TargetId.Value };

            var attack = new BossAttack() { BossId = BossEntity.Id };
            BossDependencies.BossAttacks.Add(attack);

            foreach (var target in targets)
            {
                BossDependencies.BossAttackPlayers.Add(new BossAttackPlayer() { PlayerId = target });
            }

            var playerMightDeck = BossDependencies.EncounterMightDecks
                .ReadOne(x => x.EncounterId == BossEntity.EncounterId && !x.IsFreeCompanyDeck);

            var cardsDrawn = new List<MightCard>();

            cardsDrawn.AddRange(BossDependencies.MightCardsService.DrawCards(playerMightDeck.Id, attack.Id, BossEntity.Might));

            var cardModels = cardsDrawn.Select(x => BossDependencies.Mapper.Map<MightCard, MightCardModel>(x)).ToList();

            var model = new DisplayAttackModel()
            {
                AttackId = attack.Id,
                CardsDrawn = cardModels,
                AttackerId = BossEntity.Id,
                CharacterType = CharacterType.Boss
            };

            //Call signalR
        }

        public void CompleteAttack(int attackId)
        {
            var attack = BossDependencies.BossAttacks
                .ReadOne(x => x.Id == attackId, x => x.MightCards, x => x.Players) ?? throw new Exception("Invalid attack id");
            var playerIds = attack.Players.Select(x => x.Id);
            var players = BossDependencies.EncounterPlayers
                .Read(x => x.EncounterId == BossEntity.EncounterId && playerIds.Contains(x.PlayerId));

            foreach (var player in players)
            {
                player.CurrentHealth -= attack.MightCards.Sum(x => x.Value) / attack.Boss.Defence;
                BossDependencies.EncounterPlayers.Update(player);
                // handle scenario where player health drops below 1
            }

            BossDependencies.BossAttacks.Delete(attack);
        }

        private BossAction GetNextAction()
        {
            //temporarily ignore action shuffling
            return BossDependencies.BossActions.Read(x => x.BossId == BossEntity.Id).OrderBy(x => x.Id).First();
        }

        public string GetNextActionText()
        {
            return GetActionText(GetNextAction().Number);
        }

        protected abstract int GetStage();
        protected abstract int GetDefaultTarget();
        protected abstract Action GetAction(int number);
        protected abstract string GetActionText(int number);
        protected abstract void PerformSpecialAction(SpecialActionComponent actionComponent);
        protected abstract void PerformCustomAction(CustomActionComponent actionComponent);
        protected abstract void PerformStartOfRoundActions();
        protected abstract void PerformEndOfRoundActions();
    }
}