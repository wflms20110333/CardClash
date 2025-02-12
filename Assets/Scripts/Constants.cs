﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardClash
{
    public static class Constants
    {
        public const float  CARD_SCALE = 0.2f;
        public const int    NUM_NUMBER_CARDS = 4;
        public const int    NUM_BATTLE_CARDS = 12;
        public const int    TOTAL_CARDS = NUM_NUMBER_CARDS * 10 + NUM_BATTLE_CARDS;
        public const float  PLAYER_PUBLIC_CARD_POSITION_OFFSET = 1.2f;
        public const float  PLAYER_HIDDEN_CARD_POSITION_OFFSET = 2f;
        public const int    INITIAL_HIDDEN_CARDS = 3;
        public const int    INITIAL_PUBLIC_CARDS = 3;
        public const float  CARD_SNAP_DISTANCE = 0.01f;
        public const float  CARD_MOVEMENT_SPEED = 25.0f;
        public const float  CARD_ROTATION_SPEED = 8f;
    }

    public enum CardType
    {
        None,
        Card1,
        Card2,
        Card3,
        Card4,
        Card5,
        Card6,
        Card7,
        Card8,
        Card9,
        Card10,
        CardBattle,
        CardBack,
    }
}
