using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Graphics;
using Android.Graphics.Drawables;
using static Android.Views.ViewGroup;

namespace KProgressHUD
{
    public class KProgressHUD
    {
        public enum Style
        {
            Spin, Pie, Annular, Bar
        }

        // To avoid redundant APIs, make the HUD as a wrapper class around a Dialog
        private ProgressDialog mProgressDialog;
        private static float mDimAmount;
        private static int mWindowColor;
        private static float mCornerRadius;
        private Context mContext;

        private static int mAnimateSpeed;

        private static int mMaxProgress;
        private static bool mIsAutoDismiss;

        private static int mGraceTimeMs;
        private Handler mGraceTimer;
        private static bool mFinished;

        public KProgressHUD(Context context)
        {
            mContext = context;
            mProgressDialog = new ProgressDialog(context);
            mDimAmount = 0;
            //noinspection deprecation
            mWindowColor = context.Resources.GetColor(Resource.Color.kprogresshud_default_color);
            mAnimateSpeed = 1;
            mCornerRadius = 10;
            mIsAutoDismiss = true;
            mGraceTimeMs = 0;
            mFinished = false;

            SetStyle(Style.Spin);
        }

        public static KProgressHUD Create(Context context)
        {
            return new KProgressHUD(context);
        }

        public static KProgressHUD Create(Context context, Style style)
        {
            return new KProgressHUD(context).SetStyle(style);
        }
        private KProgressHUD SetStyle(Style style)
        {
            View view = null;
            switch (style)
            {
                case Style.Spin:
                    view = new SpinView(mContext);
                    break;
            }
            mProgressDialog.SetView(view);
            return this;
        }

        public KProgressHUD SetDimAmount(float dimAmount)
        {
            if (dimAmount >= 0 && dimAmount <= 1)
            {
                mDimAmount = dimAmount;
            }
            return this;
        }

        public KProgressHUD SetSize(int width, int height)
        {
            mProgressDialog.SetSize(width, height);
            return this;
        }

        public KProgressHUD SetWindowColor(int color)
        {
            mWindowColor = color;
            return this;
        }

        public KProgressHUD SetBackgroundColor(int color)
        {
            mWindowColor = color;
            return this;
        }

        public KProgressHUD SetCornerRadius(float radius)
        {
            mCornerRadius = radius;
            return this;
        }

        public KProgressHUD setAnimationSpeed(int scale)
        {
            mAnimateSpeed = scale;
            return this;
        }

        public KProgressHUD SetLabel(string label)
        {
            mProgressDialog.SetLabel(label);
            return this;
        }

        public KProgressHUD SetLabel(string label, Color color)
        {
            mProgressDialog.SetLabel(label, color);
            return this;
        }

        public KProgressHUD SetDetailsLabel(string detailsLabel)
        {
            mProgressDialog.SetDetailsLabel(detailsLabel);
            return this;
        }

        public KProgressHUD SetDetailsLabel(string detailsLabel, Color color)
        {
            mProgressDialog.SetDetailsLabel(detailsLabel, color);
            return this;
        }

        public KProgressHUD SetCustomView(View view)
        {
            if (view != null)
            {
                mProgressDialog.SetView(view);
            }
            else
            {
                throw new RuntimeException("Custom view must not be null!");
            }
            return this;
        }

        public KProgressHUD SetCancellable(bool isCancellable)
        {
            mProgressDialog.SetCancelable(isCancellable);
            mProgressDialog.SetOnCancelListener(null);
            return this;
        }

        public KProgressHUD SetCancellable(IDialogInterfaceOnCancelListener listener)
        {
            mProgressDialog.SetCancelable(null != listener);
            mProgressDialog.SetOnCancelListener(listener);
            return this;
        }

        public KProgressHUD SetAutoDismiss(bool isAutoDismiss)
        {
            mIsAutoDismiss = isAutoDismiss;
            return this;
        }

        public KProgressHUD SetGraceTime(int graceTimeMs)
        {
            mGraceTimeMs = graceTimeMs;
            return this;
        }

        public KProgressHUD Show()
        {
            if (!IsShowing())
            {
                mFinished = false;
                if (mGraceTimeMs == 0)
                {
                    mProgressDialog.Show();
                }
                else
                {
                    mGraceTimer = new Handler();
                    mGraceTimer.PostDelayed(new Runnable(ProgressHandler), mGraceTimeMs);
                }
            }
            return this;
        }

        private void ProgressHandler()
        {
            if (mProgressDialog != null && !mFinished)
            {
                mProgressDialog.Show();
            }
        }

        public bool IsShowing()
        {
            return mProgressDialog != null && mProgressDialog.IsShowing;
        }

        public void Dismiss()
        {
            mFinished = true;
            if (mProgressDialog != null && mProgressDialog.IsShowing)
            {
                mProgressDialog.Dismiss();
            }
            if (mGraceTimer != null)
            {
                mGraceTimer.RemoveCallbacksAndMessages(null);
                mGraceTimer = null;
            }
        }



        private class ProgressDialog : Dialog
        {

            private Determinate mDeterminateView;
            private Indeterminate mIndeterminateView;
            private View mView;
            private TextView mLabelText;
            private TextView mDetailsText;
            private string mLabel;
            private string mDetailsLabel;
            private FrameLayout mCustomViewContainer;
            private BackgroundLayout mBackgroundLayout;
            private int mWidth, mHeight;
            private Color mLabelColor = Color.White;
            private Color mDetailColor = Color.White;

            public ProgressDialog(Context context)
                : base(context)
            {
            }

            protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);
                RequestWindowFeature((int)WindowFeatures.NoTitle);
                SetContentView(Resource.Layout.kprogresshud_hud);

                Window window = Window;
                window.SetBackgroundDrawable(new ColorDrawable());
                window.AddFlags(WindowManagerFlags.DimBehind);
                WindowManagerLayoutParams layoutParams = window.Attributes;
                layoutParams.DimAmount = mDimAmount;
                layoutParams.Gravity = GravityFlags.Center;
                window.Attributes = (layoutParams);

                SetCanceledOnTouchOutside(false);

                initViews();
            }

            private void initViews()
            {
                mBackgroundLayout = (BackgroundLayout)FindViewById(Resource.Id.background);
                mBackgroundLayout.SetBaseColor(mWindowColor);
                mBackgroundLayout.SetCornerRadius(mCornerRadius);
                if (mWidth != 0)
                {
                    UpdateBackgroundSize();
                }

                mCustomViewContainer = (FrameLayout)FindViewById(Resource.Id.container);
                AddViewToFrame(mView);

                if (mDeterminateView != null)
                {
                    mDeterminateView.SetMax(mMaxProgress);
                }
                if (mIndeterminateView != null)
                {
                    mIndeterminateView.SetAnimationSpeed(mAnimateSpeed);
                }

                mLabelText = (TextView)FindViewById(Resource.Id.label);
                SetLabel(mLabel, mLabelColor);
                mDetailsText = (TextView)FindViewById(Resource.Id.details_label);
                SetDetailsLabel(mDetailsLabel, mDetailColor);
            }

            private void AddViewToFrame(View view)
            {
                if (view == null) return;
                int wrapParam = ViewGroup.LayoutParams.WrapContent;
                LayoutParams param = new LayoutParams(wrapParam, wrapParam);
                mCustomViewContainer.AddView(view, param);
            }

            private void UpdateBackgroundSize()
            {
                LayoutParams param = mBackgroundLayout.LayoutParameters;
                param.Width = (int)Helper.DpToPixel(mWidth, Context);
                param.Height = (int)Helper.DpToPixel(mHeight, Context);
                mBackgroundLayout.LayoutParameters = (param);
            }

            public void SetProgress(int progress)
            {
                if (mDeterminateView != null)
                {
                    mDeterminateView.SetProgress(progress);
                    if (mIsAutoDismiss && progress >= mMaxProgress)
                    {
                        Dismiss();
                    }
                }
            }

            public void SetView(View view)
            {
                if (view != null)
                {
                    if (view.GetType() == typeof(Determinate))
                    {
                        mDeterminateView = (Determinate)view;
                    }
                    if (view.GetType() == typeof(Indeterminate))
                    {
                        mIndeterminateView = (Indeterminate)view;
                    }
                    mView = view;
                    if (IsShowing)
                    {
                        mCustomViewContainer.RemoveAllViews();
                        AddViewToFrame(view);
                    }
                }
            }

            public void SetLabel(string label)
            {
                mLabel = label;
                if (mLabelText != null)
                {
                    if (label != null)
                    {
                        mLabelText.Text = (label);
                        mLabelText.Visibility = (ViewStates.Visible);
                    }
                    else
                    {
                        mLabelText.Visibility = (ViewStates.Gone);
                    }
                }
            }

            public void SetDetailsLabel(string detailsLabel)
            {
                mDetailsLabel = detailsLabel;
                if (mDetailsText != null)
                {
                    if (detailsLabel != null)
                    {
                        mDetailsText.Text = (detailsLabel);
                        mDetailsText.Visibility = (ViewStates.Visible);
                    }
                    else
                    {
                        mDetailsText.Visibility = (ViewStates.Gone);
                    }
                }
            }

            public void SetLabel(string label, Color color)
            {
                mLabel = label;
                mLabelColor = color;
                if (mLabelText != null)
                {
                    if (label != null)
                    {
                        mLabelText.Text = (label);
                        mLabelText.SetTextColor(color);
                        mLabelText.Visibility = (ViewStates.Visible);
                    }
                    else
                    {
                        mLabelText.Visibility = (ViewStates.Gone);
                    }
                }
            }

            public void SetDetailsLabel(string detailsLabel, Color color)
            {
                mDetailsLabel = detailsLabel;
                mDetailColor = color;
                if (mDetailsText != null)
                {
                    if (detailsLabel != null)
                    {
                        mDetailsText.Text = (detailsLabel);
                        mDetailsText.SetTextColor(color);
                        mDetailsText.Visibility = (ViewStates.Visible);
                    }
                    else
                    {
                        mDetailsText.Visibility = (ViewStates.Gone);
                    }
                }
            }

            public void SetSize(int width, int height)
            {
                mWidth = width;
                mHeight = height;
                if (mBackgroundLayout != null)
                {
                    UpdateBackgroundSize();
                }
            }
        }


    }

}