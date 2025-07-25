// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Localisation;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.UI;
using osu.Game.Rulesets.UI;
using osu.Game.Utils;
using osuTK;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModLilBro : Mod, IUpdatableByPlayfield, IApplicableToDrawableRuleset<OsuHitObject>, IApplicableToBeatmap
    {
        public override string Name => "Lil Bro";
        public override string Acronym => "LB";
        public override LocalisableString Description => "Why i'm so small?!";
        public override double ScoreMultiplier => 1;
        public override ModType Type => ModType.Fun;

        private static readonly Vector2 playfield_center = OsuPlayfield.BASE_SIZE / 2;

        private static readonly Vector2 base_offset = new Vector2(0, -13);

        private static readonly float offset_factor_y = OsuPlayfield.BASE_SIZE.X / OsuPlayfield.BASE_SIZE.Y;

        // make playfield a little smaller so that you can reach the top and bottom edges at high scale
        private static readonly float base_scale = 0.9f;

        private static readonly float transition_duration = 100;

        [SettingSource("Scale")]
        public Bindable<float> PlayfieldScale { get; } = new BindableFloat(1.5f)
        {
            MinValue = 1.25f,
            MaxValue = 1.75f,
            Precision = 0.01f
        };

        private PeriodTracker spinnerPeriods = null!;

        private PlayfieldAdjustmentContainer playfieldAdjustmentContainer = null!;

        public void Update(Playfield playfield)
        {
            if (!spinnerPeriods.IsInAny(playfield.Clock.CurrentTime))
            {
                var offset = (playfield.Cursor.ActiveCursor.Position - playfield_center) * base_scale;
                offset.Y *= offset_factor_y;
                playfieldAdjustmentContainer.Position = -(offset * PlayfieldScale.Value - offset) + base_offset;
            }
            else
            {
                playfieldAdjustmentContainer.Position = new Vector2(0, 0);
            }


            playfield.Cursor.ActiveCursor.Scale = new Vector2(1 / PlayfieldScale.Value) * base_scale;
        }

        public void ApplyToDrawableRuleset(DrawableRuleset<OsuHitObject> drawableRuleset)
        {
            playfieldAdjustmentContainer = drawableRuleset.PlayfieldAdjustmentContainer;

            playfieldAdjustmentContainer.Scale = new Vector2(PlayfieldScale.Value) * base_scale;
        }

        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            spinnerPeriods = new PeriodTracker(beatmap.HitObjects.OfType<Spinner>().Select(b => new Period(b.StartTime - transition_duration, b.EndTime)));
        }
    }
}
