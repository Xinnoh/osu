// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Objects.Drawables;
using OpenTK;
using osu.Framework.Configuration;

namespace osu.Game.Rulesets.Timing.Drawables
{
    /// <summary>
    /// A container for hit objects which applies applies the speed changes defined by the <see cref="Timing.SpeedAdjustment.BeatLength"/> and <see cref="Timing.SpeedAdjustment.SpeedMultiplier"/>
    /// properties to its <see cref="Container{T}.Content"/> to affect the <see cref="Drawables.DrawableTimingSection"/> scroll speed.
    /// </summary>
    public abstract class SpeedAdjustmentContainer : Container<DrawableHitObject>
    {
        private readonly Bindable<double> visibleTimeRange = new Bindable<double>();
        public Bindable<double> VisibleTimeRange
        {
            get { return visibleTimeRange; }
            set { visibleTimeRange.BindTo(value); }
        }

        public readonly SpeedAdjustment TimingSection;

        protected override Container<DrawableHitObject> Content => content;
        private Container<DrawableHitObject> content;

        private readonly Axes scrollingAxes;

        /// <summary>
        /// Creates a new <see cref="SpeedAdjustmentContainer"/>.
        /// </summary>
        /// <param name="timingSection">The encapsulated timing section that provides the speed changes.</param>
        /// <param name="scrollingAxes">The axes through which this drawable timing section scrolls through.</param>
        protected SpeedAdjustmentContainer(SpeedAdjustment timingSection, Axes scrollingAxes)
        {
            this.scrollingAxes = scrollingAxes;

            TimingSection = timingSection;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            DrawableTimingSection timingSection = CreateTimingSection();

            timingSection.VisibleTimeRange.BindTo(VisibleTimeRange);
            timingSection.RelativeChildOffset = new Vector2((scrollingAxes & Axes.X) > 0 ? (float)TimingSection.Time : 0, (scrollingAxes & Axes.Y) > 0 ? (float)TimingSection.Time : 0);

            AddInternal(content = timingSection);
        }

        public override Axes RelativeSizeAxes
        {
            get { return Axes.Both; }
            set { throw new InvalidOperationException($"{nameof(SpeedAdjustmentContainer)} must always be relatively-sized."); }
        }

        protected override void Update()
        {
            float speedAdjustedSize = (float)(1000 / TimingSection.BeatLength / TimingSection.SpeedMultiplier);

            // The speed adjustment happens by modifying our size while maintaining the visible time range as the relatve size for our children
            Size = new Vector2((scrollingAxes & Axes.X) > 0 ? speedAdjustedSize : 1, (scrollingAxes & Axes.Y) > 0 ? speedAdjustedSize : 1);
            RelativeChildSize = new Vector2((scrollingAxes & Axes.X) > 0 ? (float)VisibleTimeRange : 1, (scrollingAxes & Axes.Y) > 0 ? (float)VisibleTimeRange : 1);
        }

        /// <summary>
        /// Whether this speed adjustment can contain a hit object. This is true if the hit object occurs after this speed adjustment with respect to time.
        /// </summary>
        public bool CanContain(DrawableHitObject hitObject) => TimingSection.Time <= hitObject.HitObject.StartTime;

        /// <summary>
        /// Creates the container which handles the movement of a collection of hit objects.
        /// </summary>
        /// <returns>The drawable timing section.</returns>
        protected abstract DrawableTimingSection CreateTimingSection();
    }
}