﻿using SysBot.Base;

namespace SysBot.Pokemon
{
    public class ProgramConfig : BotList<PokeBotState>
    {
        public ProgramMode Mode { get; set; } = ProgramMode.BDSP;
        public PokeTradeHubConfig Hub { get; set; } = new();
    }

    public enum ProgramMode
    {
        None, // invalid
        BDSP,
    }
}