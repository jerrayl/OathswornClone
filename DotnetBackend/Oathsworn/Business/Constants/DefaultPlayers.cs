using System.Collections.Generic;
using Oathsworn.Entities;

namespace Oathsworn.Business.Constants
{
    public static class DefaultPlayers
    {
        public static readonly List<Player> Players = new()
        {
            new Player()
            {
                Class = Class.Witch,
                Health = 5,
                Defence = 2,
                MaxAnimus = 8,
                AnimusRegen = 6,
                Might = new Dictionary<Might, int>() { { Might.White, 0 }, { Might.Yellow, 0 }, { Might.Red, 1 }, { Might.Black, 0 } }
            },
            new Player()
            {
                Class = Class.Huntress,
                Health = 5,
                Defence = 2,
                MaxAnimus = 8,
                AnimusRegen = 6,
                Might = new Dictionary<Might, int>() { { Might.White, 0 }, { Might.Yellow, 2 }, { Might.Red, 0 }, { Might.Black, 0 } }
            },
            new Player()
            {
                Class = Class.Warden,
                Health = 5,
                Defence = 4,
                MaxAnimus = 8,
                AnimusRegen = 6,
                Might = new Dictionary<Might, int>() { { Might.White, 0 }, { Might.Yellow, 1 }, { Might.Red, 0 }, { Might.Black, 0 } }
            },
            new Player()
            {
                Class = Class.Penitent,
                Health = 5,
                Defence = 4,
                MaxAnimus = 8,
                AnimusRegen = 6,
                Might = new Dictionary<Might, int>() { { Might.White, 0 }, { Might.Yellow, 1 }, { Might.Red, 0 }, { Might.Black, 0 } }
            }
        };

        public static readonly List<EncounterPlayer> EncounterPlayers = new()
        {
            new EncounterPlayer()
            {
                XPosition = -4,
                YPosition = 6,
                CurrentHealth = 5,
                CurrentAnimus = 6,
                Tokens = new() { { Token.Animus, 2 }, { Token.Battleflow, 2 }, { Token.Defence, 2 }, { Token.Empower, 2 }, { Token.Redraw, 3 } }
            },
            new EncounterPlayer()
            {
                XPosition = -2,
                YPosition = 5,
                CurrentHealth = 5,
                CurrentAnimus = 6,
                Tokens = new() { { Token.Animus, 2 }, { Token.Battleflow, 2 }, { Token.Defence, 2 }, { Token.Empower, 2 }, { Token.Redraw, 3 } }
            },
            new EncounterPlayer()
            {
                XPosition = -1,
                YPosition = 5   ,
                CurrentHealth = 5,
                CurrentAnimus = 6,
                Tokens = new() { { Token.Animus, 2 }, { Token.Battleflow, 2 }, { Token.Defence, 2 }, { Token.Empower, 2 }, { Token.Redraw, 3 } }
            },
            new EncounterPlayer()
            {
                XPosition = 0,
                YPosition = 4,
                CurrentHealth = 5,
                CurrentAnimus = 6,
                Tokens = new() { { Token.Animus, 2 }, { Token.Battleflow, 2 }, { Token.Defence, 2 }, { Token.Empower, 2 }, { Token.Redraw, 3 } }
            }
        };
    }
}