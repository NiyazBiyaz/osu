// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Mods;
using osu.Framework.Localisation;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModAimPoint : Mod
    {
        public override string Name => "Crosshair";
        public override string Acronym => "CH";
        public override ModType Type => ModType.Fun;
        public override LocalisableString Description => "Cursor? No. Crosshair!";
        public override double ScoreMultiplier => 1;

        public void Update(Playfield playfield)
        {

        }
    }
}
