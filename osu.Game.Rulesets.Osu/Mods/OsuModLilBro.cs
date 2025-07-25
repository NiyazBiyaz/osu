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
    public class OsuModLilBro : Mod, IUpdatableByPlayfield, IApplicableToDrawableRuleset<OsuHitObject>
    {
        public override string Name => "Lil Bro";
        public override string Acronym => "LB";
        public override LocalisableString Description => "Why i'm so small?!";
        public override double ScoreMultiplier => 1;
        public override ModType Type => ModType.Fun;

        private static readonly Vector2 playfield_center = OsuPlayfield.BASE_SIZE / 2;
        private static readonly float offset_factor_y = OsuPlayfield.BASE_SIZE.X / OsuPlayfield.BASE_SIZE.Y;

        [SettingSource("Scale")]
        public Bindable<float> PlayfieldScale { get; } = new BindableFloat(1.5f)
        {
            MinValue = 1.1f,
            MaxValue = 2f,
            Precision = 0.01f
        };

        private PlayfieldAdjustmentContainer playfieldAdjustmentContainer = null!;

        // TODO: как-то сделать так, чтобы при большом увеличении до краев игрового поля можно было достать (пересмотреть логику офсетов и тд)
        public void Update(Playfield playfield)
        {
            var offset = playfield.Cursor.ActiveCursor.Position - playfield_center;
            offset.Y *= offset_factor_y;
            playfieldAdjustmentContainer.Position = -(offset * PlayfieldScale.Value - offset);

            playfield.Cursor.ActiveCursor.Scale = new Vector2(1 / PlayfieldScale.Value);
        }

        public void ApplyToDrawableRuleset(DrawableRuleset<OsuHitObject> drawableRuleset)
        {
            playfieldAdjustmentContainer = drawableRuleset.PlayfieldAdjustmentContainer;

            playfieldAdjustmentContainer.Scale = new Vector2(PlayfieldScale.Value);
        }
    }
}
