using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Java.Lang;
using Android.Graphics;

namespace KProgressHUD
{
    public class SpinView : ImageView, Indeterminate
    {
        private float mRotateDegrees;
        private int mFrameTime;
        private bool mNeedToUpdateView;
        private Runnable mUpdateViewRunnable;

        public SpinView(Context context)
            : base(context)
        {
            init();
        }

        public SpinView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            init();
        }

        private void init()
        {
            SetImageResource(Resource.Drawable.kprogresshud_spinner);
            mFrameTime = 1000 / 12;
            mUpdateViewRunnable = new Runnable(handler);
        }

        private void handler()
        {
            mRotateDegrees += 30;
            mRotateDegrees = mRotateDegrees < 360 ? mRotateDegrees : mRotateDegrees - 360;
            Invalidate();
            if (mNeedToUpdateView)
            {
                PostDelayed(mUpdateViewRunnable, mFrameTime);
            }
        }

        public void SetAnimationSpeed(float scale)
        {
            mFrameTime = (int)(1000 / 12 / scale);
        }

        protected override void OnDraw(Canvas canvas)
        {
            canvas.Rotate(mRotateDegrees, Width / 2, Height / 2);
            base.OnDraw(canvas);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            mNeedToUpdateView = true;
            Post(mUpdateViewRunnable);
        }

        protected override void OnDetachedFromWindow()
        {
            mNeedToUpdateView = false;
            base.OnDetachedFromWindow();
        }

    }
}