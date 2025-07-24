// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Rulesets.Mods;
using osu.Framework.Localisation;
using osu.Game.Rulesets.Osu.UI;
using osuTK;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.UI;
using osu.Game.Configuration;
using osu.Framework.Bindables;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModCrosshair : Mod, IUpdatableByPlayfield, IApplicableToDrawableRuleset<OsuHitObject>
    {
        public override string Name => "Crosshair";
        public override string Acronym => "CH";
        public override LocalisableString Description => "Cursor? No. Crosshair!";
        public override double ScoreMultiplier => 1;
        public override ModType Type => ModType.Fun;

        private static readonly Vector2 playfield_center = OsuPlayfield.BASE_SIZE / 2;

        [SettingSource("Scope", "Move the camera away for a better view")]
        public Bindable<float> PlayfieldScale { get; } = new BindableFloat(1)
        {
            MinValue = 0.25f,
            MaxValue = 1,
            Precision = 0.05f
        };

        private PlayfieldAdjustmentContainer playfieldAdjustmentContainer = null!;

        public void Update(Playfield playfield)
        {
            // x2 for cursor movement compensation
            var ofset = (playfield.Cursor.ActiveCursor.Position - playfield_center) * PlayfieldScale.Value;

            playfieldAdjustmentContainer.Position = -ofset;
        }

        public void ApplyToDrawableRuleset(DrawableRuleset<OsuHitObject> drawableRuleset)
        {
            playfieldAdjustmentContainer = drawableRuleset.PlayfieldAdjustmentContainer;

            playfieldAdjustmentContainer.Scale = new Vector2(PlayfieldScale.Value);
        }
    }
}
