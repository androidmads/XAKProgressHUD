using System;
using Android.App;
using Android.OS;
using Java.Lang;
using Android.Widget;
using Android.Graphics.Drawables;
using KK = KProgressHUD.KProgressHUD;

namespace Sample
{
    [Activity(Label = "KProgress Hud Sample", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyCustomTheme")]
    public class MainActivity : Activity
    {
        KK hud;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            FindViewById(Resource.Id.btn1).Click += btn1Clicked;
            FindViewById(Resource.Id.btn2).Click += btn2Clicked;
            FindViewById(Resource.Id.btn3).Click += btn3Clicked;
            FindViewById(Resource.Id.btn4).Click += btn4Clicked;
            FindViewById(Resource.Id.btn5).Click += btn5Clicked;
            FindViewById(Resource.Id.btn6).Click += btn6Clicked;
            FindViewById(Resource.Id.btn7).Click += btn7Clicked;
        }

        private void btn1Clicked(object sender, EventArgs e)
        {
            hud = KK.Create(this);
            hud.Show();
            ScheduleDismiss();
        }

        private void btn2Clicked(object sender, EventArgs e)
        {
            hud = KK.Create(this).SetLabel("Loading...");
            hud.Show();
            ScheduleDismiss();
        }

        private void btn3Clicked(object sender, EventArgs e)
        {
            hud = KK.Create(this)

                .SetLabel("Please wait...")
                .SetDetailsLabel("File Downloading...");
            hud.Show();
            ScheduleDismiss();
        }

        private void btn4Clicked(object sender, EventArgs e)
        {
            hud = KK.Create(this)

              .SetGraceTime(1000);
            hud.Show();
            ScheduleDismiss();
        }

        private void btn5Clicked(object sender, EventArgs e)
        {
            ImageView imageView = new ImageView(this);
            imageView.SetBackgroundResource(Resource.Drawable.spin_animation);
            AnimationDrawable drawable = (AnimationDrawable)imageView.Background;
            drawable.Start();
            hud = KK.Create(this).SetLabel("Loading...").SetCustomView(imageView);
            hud.Show();
            ScheduleDismiss();
        }

        private void btn6Clicked(object sender, EventArgs e)
        {
            hud = KK.Create(this).SetDimAmount(0.5f);
            hud.Show();
            ScheduleDismiss();
        }

        private void btn7Clicked(object sender, EventArgs e)
        {
            hud = KK.Create(this)
                .SetWindowColor(Resources.GetColor(Resource.Color.my_gray))
                .setAnimationSpeed(2);
            hud.Show();
            ScheduleDismiss();
        }

        private void ScheduleDismiss()
        {
            Handler mHandler = new Handler();
            mHandler.PostDelayed(new Runnable(() => hud.Dismiss()), 2000);
        }

    }
}

