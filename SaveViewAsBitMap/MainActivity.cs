using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Views;
using Android.Provider;
using Android.Content;


using static Android.Graphics.Bitmap;
using Android.Net;
using Java.IO;
using System;
using Android.Graphics.Drawables;

namespace SaveViewAsBitMap
{
    [Activity(Label = "SaveViewAsBitMap", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            Button bt1 = FindViewById<Button>(Resource.Id.button1);
            bt1.Click += Bt1_Click;
          
        }

        private void Bt1_Click(object sender, System.EventArgs e)
        {
            View v = FindViewById<LinearLayout>(Resource.Id.myLinearLayout);
            Bitmap myBitMap = createViewBitmap(v);
            ImageView img = FindViewById<ImageView>(Resource.Id.imageView1);

            Drawable drawable = new BitmapDrawable(myBitMap);
            img.SetBackgroundDrawable(drawable);
           // MediaStore.Images.Media.InsertImage(ContentResolver, myBitMap, "title", "description");
            saveImage(myBitMap);
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Java.IO.File myFile = new Java.IO.File(Android.OS.Environment.ExternalStorageDirectory + "/DCIM/Camera", "MikeBitMap4.jpg");
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(myFile);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
        }

        public Bitmap createViewBitmap(View v)
        {
            Bitmap bitmap = Bitmap.CreateBitmap(v.Width, v.Height,
                    Bitmap.Config.Argb8888);
            Canvas canvas = new Canvas(bitmap);
            v.Draw(canvas);
            return bitmap;
        }


        public static void saveImage(Bitmap bmp)
        {
           

            try
            {
                using (var os = new System.IO.FileStream(Android.OS.Environment.ExternalStorageDirectory + "/DCIM/Camera/MikeBitMap4.jpg", System.IO.FileMode.CreateNew))
                {
                    bmp.Compress(Bitmap.CompressFormat.Jpeg, 95, os);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}

