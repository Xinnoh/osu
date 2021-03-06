﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Mods;

namespace osu.Game.Rulesets.Catch.Mods
{
    public class CatchModNoFail : ModNoFail
    {

    }

    public class CatchModEasy : ModEasy
    {

    }

    public class CatchModHidden : ModHidden
    {
        public override string Description => @"Play with fading notes for a slight score advantage.";
        public override double ScoreMultiplier => 1.06;
    }

    public class CatchModHardRock : ModHardRock
    {
        public override double ScoreMultiplier => 1.12;
        public override bool Ranked => true;
    }

    public class CatchModSuddenDeath : ModSuddenDeath
    {

    }

    public class CatchModDaycore : ModDaycore
    {
        public override double ScoreMultiplier => 0.5;
    }

    public class CatchModDoubleTime : ModDoubleTime
    {
        public override double ScoreMultiplier => 1.06;
    }

    public class CatchModRelax : ModRelax
    {
        public override string Description => @"Use the mouse to control the catcher.";
    }

    public class CatchModHalfTime : ModHalfTime
    {
        public override double ScoreMultiplier => 0.5;
    }

    public class CatchModNightcore : ModNightcore
    {
        public override double ScoreMultiplier => 1.06;
    }

    public class CatchModFlashlight : ModFlashlight
    {
        public override double ScoreMultiplier => 1.12;
    }

    public class CatchModPerfect : ModPerfect
    {

    }
}
